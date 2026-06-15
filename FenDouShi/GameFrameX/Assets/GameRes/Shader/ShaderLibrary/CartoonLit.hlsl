#ifndef CARTOON_LIT_INCLUDED
#define CARTOON_LIT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

uniform half4 _SkyColor;
uniform half4 _EquatorColor;
uniform half4 _GroundColor;

struct CustomInputData
{
    float3 positionWS;
    float3 normalWS;
    half3 viewDirectionWS;
    half3 bakedGI;
};

struct CustomSurfaceData
{
    half3 albedo;
    half alpha;
    half3 emission;
    half occlusion;

    half shadowSmooth;
    half shadowRange;
    half4 diffColorA;
    half4 diffColorB;
};

half3 LightingCartoonDiffuse(half3 lightColor, half3 lightDir, half3 normal, CustomSurfaceData surfaceData)
{
    half NdotL = saturate(dot(normal, lightDir));

    half ramp = smoothstep(0, surfaceData.shadowSmooth, NdotL - surfaceData.shadowRange);
    half3 diffuse = lerp(surfaceData.diffColorB, surfaceData.diffColorA, ramp);

    return diffuse * lightColor;
}

half3 CalculateLight(Light light, CustomInputData inputData, CustomSurfaceData surfaceData)
{
    half3 attenuatedLightColor = light.color * (light.distanceAttenuation);
    half3 lightColor = LightingCartoonDiffuse(attenuatedLightColor, light.direction, inputData.normalWS, surfaceData);

    lightColor *= surfaceData.albedo;

    // #if defined(_SPECGLOSSMAP) || defined(_SPECULAR_COLOR)
    //     half smoothness = exp2(10 * surfaceData.smoothness + 1);
    //     lightColor += LightingSpecular(attenuatedLightColor, light.direction, inputData.normalWS, inputData.viewDirectionWS, half4(surfaceData.specular, 1), smoothness);
    // #endif

    return lightColor;
}

half4 CartoonLit(CustomInputData inputData, CustomSurfaceData surfaceData)
{
    half3 giColor = 0;
    half3 mainLightColor = 0;
    half3 additionalLightsColor = 0;

    //GI
    giColor = inputData.bakedGI * surfaceData.albedo;

    //主光源
    Light mainLight = GetMainLight();
    mainLightColor += CalculateLight(mainLight, inputData, surfaceData);

    //其他光源
    #if defined(_ADDITIONAL_LIGHTS) && !WX_PERFORMANCE_MODE
        uint pixelLightCount = GetAdditionalLightsCount();

        LIGHT_LOOP_BEGIN(pixelLightCount)
        Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
        additionalLightsColor += CalculateLight(light, inputData, surfaceData);
        LIGHT_LOOP_END
    #endif

    half3 finalColor = (giColor + mainLightColor + additionalLightsColor);
    finalColor = finalColor * surfaceData.occlusion + surfaceData.emission;

    return half4(finalColor, surfaceData.alpha);
}

half3 SampleEmission(float2 uv, half3 emissionColor, TEXTURE2D_PARAM(emissionMap, sampler_emissionMap))
{
    #ifndef _EMISSION
        return 0;
    #else
        return SAMPLE_TEXTURE2D(emissionMap, sampler_emissionMap, uv).rgb * emissionColor;
    #endif
}

half3 SampleSimpleGI(float3 normalWS)
{
    float y = normalWS.y; // 获取法线向量的y分量
    float x = (y + 1) * 0.5;       // 将 y 从 [-1, 1] 映射到 [0, 1]

    // 使用 saturate 和 lerp 替代 if 语句
    half isUpperHemisphere = step(0.5, x); // 如果 x > 0.5，返回 1；否则返回 0
    half3 upperColor = lerp(_EquatorColor.rgb, _SkyColor.rgb, x * 2 - 1);
    half3 lowerColor = lerp(_GroundColor.rgb, _EquatorColor.rgb, x * 2);

    // 根据 isUpperHemisphere 选择最终颜色
    half3 finalColor = lerp(lowerColor, upperColor, isUpperHemisphere);

    return finalColor;
}


#endif

