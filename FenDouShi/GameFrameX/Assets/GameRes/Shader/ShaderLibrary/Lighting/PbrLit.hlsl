#ifndef YA_PBR_LIT_INCLUDED
#define YA_PBR_LIT_INCLUDED

// #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"


#include "GI.hlsl"



uniform half3 _FrontLightColor;
uniform half3 _BackLightColor;
uniform half3 _FrontLightDirection;
uniform half3 _BackLightDirection;
uniform half _SpecularLightScale;
uniform half _ActorSpecularLightScale;


struct CustomInputData
{
    float3 positionWS;
    float3 normalWS;
    half3 viewDirectionWS;
    half3 bakedGI;
    float4  shadowCoord;
    half3x3 tangentToWorld;
};

struct CustomSurfaceData
{
    half3 albedo;
    half alpha;
    half3 emission;
    half occlusion;

    half3 specular;
    half  metallic;
    half  smoothness;
    half3 normalTS;

    half shadowSmooth;
    half shadowRange;
    half4 diffColorA;
    half4 diffColorB;
};

Light GetFrontLight()
{
    Light light;
    light.direction = _FrontLightDirection;
    light.color = _FrontLightColor;
    light.distanceAttenuation = 1.0;
    light.shadowAttenuation = 1.0;
    light.layerMask = 0;
    return light;
}

Light GetBackLight()
{
    Light light;
    light.direction = _BackLightDirection;
    light.color = _BackLightColor;
    light.distanceAttenuation = 1.0;
    light.shadowAttenuation = 1.0;
    light.layerMask = 0;
    return light;
}

half3 LightingDiffuse(half3 lightColor, half3 lightDir, half3 normal, CustomSurfaceData surfaceData)
{
    half NdotL = saturate(dot(normal, lightDir));

    // half ramp = smoothstep(0, surfaceData.shadowSmooth, NdotL - surfaceData.shadowRange);
    // half3 diffuse = lerp(surfaceData.diffColorB, surfaceData.diffColorA, ramp);

    return NdotL * lightColor;
}

half3 CalculateLight(Light light, CustomInputData inputData, CustomSurfaceData surfaceData, BRDFData brdfData,half specularScale)
{
    #if defined(USE_CUSTOM_LIGHT)
        half3 brdf = 0;
    #else
        half3 brdf = brdfData.diffuse;
    #endif

    half3 attenuatedLightColor = light.color * (light.distanceAttenuation * light.shadowAttenuation);
    half3 radiance = LightingDiffuse(attenuatedLightColor, light.direction, inputData.normalWS, surfaceData);

    // return brdf;

#ifndef _SPECULARHIGHLIGHTS_OFF
    brdf += brdfData.specular * DirectBRDFSpecular(brdfData, inputData.normalWS, light.direction, inputData.viewDirectionWS) * specularScale;
#endif


    // return brdf;
    return brdf * radiance;
    // return radiance;
}

half4 PbrLit(CustomInputData inputData, CustomSurfaceData surfaceData)
{
    half3 giColor = 0;
    half3 mainLightColor = 0;
    half3 additionalLightsColor = 0;
    half specularScale = _SpecularLightScale;

    //--------------------------------
    BRDFData brdfData;
    InitializeBRDFData(surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.alpha, brdfData);
    //--------------------------------

    //GI
    giColor = GlobalIllumination(brdfData, inputData.bakedGI, surfaceData.occlusion, inputData.normalWS, inputData.viewDirectionWS);


    #if defined(USE_CUSTOM_LIGHT)
        //主光源
        Light frontLight = GetFrontLight();
        Light backLight = GetBackLight();
        mainLightColor += CalculateLight(frontLight, inputData, surfaceData, brdfData,specularScale);
        mainLightColor += CalculateLight(backLight, inputData, surfaceData, brdfData,specularScale);
    #else
        //主光源
        Light mainLight = GetMainLight(inputData.shadowCoord,inputData.positionWS,half4(1, 1, 1, 1));
        mainLightColor += CalculateLight(mainLight, inputData, surfaceData, brdfData,specularScale);

        //其他光源
        #if defined(_ADDITIONAL_LIGHTS)
            uint pixelLightCount = GetAdditionalLightsCount();

            LIGHT_LOOP_BEGIN(pixelLightCount)
            Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
            additionalLightsColor += CalculateLight(light, inputData, surfaceData, brdfData,specularScale);
            LIGHT_LOOP_END
        #endif
 
    #endif

   
    half3 finalColor = (giColor + mainLightColor + additionalLightsColor);
    finalColor = finalColor * surfaceData.occlusion + surfaceData.emission;

    return half4(finalColor, surfaceData.alpha);
}

half4 PbrLit_Actor(CustomInputData inputData, CustomSurfaceData surfaceData,half3 reflectVector, half NoV,half3 reflectColor)
{
    half3 giColor = 0;
    half3 mainLightColor = 0;
    half3 additionalLightsColor = 0;
    half specularScale = _ActorSpecularLightScale;

    //--------------------------------
    BRDFData brdfData;
    InitializeBRDFData(surfaceData.albedo, surfaceData.metallic, surfaceData.specular, surfaceData.smoothness, surfaceData.alpha, brdfData);
    //--------------------------------

    //GI
    giColor = GlobalIllumination_Actor(brdfData, inputData.bakedGI, surfaceData.occlusion, reflectVector, NoV,reflectColor);

    //主光源
    Light mainLight = GetMainLight(inputData.shadowCoord,inputData.positionWS,half4(1, 1, 1, 1));
    mainLightColor += CalculateLight(mainLight, inputData, surfaceData, brdfData,specularScale);

    //其他光源
    #if defined(_ADDITIONAL_LIGHTS)
        uint pixelLightCount = GetAdditionalLightsCount();

        LIGHT_LOOP_BEGIN(pixelLightCount)
        Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
        additionalLightsColor += CalculateLight(light, inputData, surfaceData, brdfData,specularScale);
        LIGHT_LOOP_END
    #endif

   
    half3 finalColor = (giColor + mainLightColor + additionalLightsColor);
    finalColor = finalColor * surfaceData.occlusion + surfaceData.emission;

    return half4(finalColor, surfaceData.alpha);
}

half4 SampleAlbedoAlpha(float2 uv, TEXTURE2D_PARAM(albedoAlphaMap, sampler_albedoAlphaMap))
{
    return half4(SAMPLE_TEXTURE2D(albedoAlphaMap, sampler_albedoAlphaMap, uv));
}

half3 SampleEmission(float2 uv, half3 emissionColor, TEXTURE2D_PARAM(emissionMap, sampler_emissionMap))
{
    #ifndef _EMISSION
        return 0;
    #else
        return SAMPLE_TEXTURE2D(emissionMap, sampler_emissionMap, uv).rgb * emissionColor;
    #endif
}

half3 SampleNormal(float2 uv, TEXTURE2D_PARAM(bumpMap, sampler_bumpMap), half scale = half(1.0))
{
#ifdef _NORMALMAP
    half4 n = SAMPLE_TEXTURE2D(bumpMap, sampler_bumpMap, uv);
    // #if BUMP_SCALE_NOT_SUPPORTED
    //     return UnpackNormal(n);
    // #else
        return UnpackNormalScale(n, scale);
    // #endif
#else
    return half3(0.0h, 0.0h, 1.0h);
#endif
}


#endif

