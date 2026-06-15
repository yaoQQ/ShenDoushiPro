#ifndef YA_GI_INCLUDED
#define YA_GI_INCLUDED

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/EntityLighting.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/ImageBasedLighting.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/RealtimeLights.hlsl"
#include "BRDF.hlsl"

#if defined(LIGHTMAP_ON)
    #define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT) OUT.xy = lightmapUV.xy * lightmapScaleOffset.xy + lightmapScaleOffset.zw;
#else
    #define OUTPUT_LIGHTMAP_UV(lightmapUV, lightmapScaleOffset, OUT)
#endif

// 天空

uniform half4 _SkyColor;
uniform half4 _EquatorColor;
uniform half4 _GroundColor;

uniform half _LightmapBrightness;
uniform half _LightmapSaturation;
uniform half _LightmapContrast;

uniform half _GIMixStrength;



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


TEXTURECUBE(_ComRefCube);
SAMPLER(sampler_ComRefCube);

half3 SampleReflection(float3 reflectVector,half mip)
{
    half4 encodedIrradiance = half4(SAMPLE_TEXTURECUBE_LOD(_ComRefCube, sampler_ComRefCube, reflectVector, mip));
    return  encodedIrradiance.xyz;
}

half3 GlossyEnvironmentReflection(half3 reflectVector, half perceptualRoughness, half occlusion)
{
#if !defined(_REFLECTION_CUBE_OFF)
    half3 irradiance;
    half mip = PerceptualRoughnessToMipmapLevel(perceptualRoughness);

    irradiance = half4(SAMPLE_TEXTURECUBE_LOD(_ComRefCube, sampler_ComRefCube, reflectVector, mip)).xyz;

    // half4 encodedIrradiance = half4(SAMPLE_TEXTURECUBE_LOD(_ComRefCube, sampler_ComRefCube, reflectVector, mip));
    // irradiance = DecodeHDREnvironment(encodedIrradiance, unity_SpecCube0_HDR);

    return irradiance * occlusion;
#else
    return half3(0.5, 0.5, 0.5) * occlusion;
#endif 
}

half3 GlobalIllumination(BRDFData brdfData,half3 bakedGI, half occlusion,half3 normalWS, half3 viewDirectionWS)
{
    half3 reflectVector = reflect(-viewDirectionWS, normalWS);
    half NoV = saturate(dot(normalWS, viewDirectionWS));
    half fresnelTerm = Pow4(1.0 - NoV);

    half3 indirectDiffuse = bakedGI;
    half3 indirectSpecular = GlossyEnvironmentReflection(reflectVector, brdfData.perceptualRoughness, half(1.0));

    half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);

    // return brdfData.albedo;

    return color * occlusion;
}

half3 GlobalIllumination_Actor(BRDFData brdfData,half3 bakedGI, half occlusion,half3 reflectVector, half NoV,half3 reflectColor)
{
    half fresnelTerm = Pow4(1.0 - NoV);

    half3 indirectDiffuse = bakedGI;
    half3 indirectSpecular =  reflectColor;

    half3 color = EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);

    return color * occlusion;
}

// Sample baked and/or realtime lightmap. Non-Direction and Directional if available.
half3 SampleLightmap(float2 staticLightmapUV, float2 dynamicLightmapUV, half3 normalWS)
{
#ifdef UNITY_LIGHTMAP_FULL_HDR
    bool encodedLightmap = false;
#else
    bool encodedLightmap = true;
#endif

    half4 decodeInstructions = half4(LIGHTMAP_HDR_MULTIPLIER, LIGHTMAP_HDR_EXPONENT, 0.0h, 0.0h);

    // The shader library sample lightmap functions transform the lightmap uv coords to apply bias and scale.
    // However, universal pipeline already transformed those coords in vertex. We pass half4(1, 1, 0, 0) and
    // the compiler will optimize the transform away.
    half4 transformCoords = half4(1, 1, 0, 0);
    float3 diffuseLighting = 0;
    diffuseLighting = SampleSingleLightmap(TEXTURE2D_LIGHTMAP_ARGS(unity_Lightmap, samplerunity_Lightmap), staticLightmapUV, transformCoords, encodedLightmap, decodeInstructions);

    return diffuseLighting;
}



#endif

