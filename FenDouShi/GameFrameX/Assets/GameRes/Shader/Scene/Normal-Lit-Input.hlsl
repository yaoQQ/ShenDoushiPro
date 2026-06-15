#ifndef NORMAL_LIT_INPUT_INCLUDED
#define NORMAL_LIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../ShaderLibrary/Common.hlsl"
#include "Scene-Common.hlsl"

// CBUFFER_START(UnityPerMaterial)
//     // UPM_BASE_MAP
//     // UPM_CUTOFF
   
//     // UPM_REFLECTION_CUBE
// CBUFFER_END

CBUFFER_START(UnityPerMaterial)
    UPM_BASE_MAP
    UPM_CUTOFF

    half _Metallic;
    half _Smoothness;
    half _OcclusionStrength;
    half _MetallicOffset;
    half _SmoothnessOffset;
    
    float _Saturation;
CBUFFER_END

half4 _EmissionColor;
half _BumpScale;

// TEXTURE2D(_EmissionMap);
// SAMPLER(sampler_EmissionMap);


// UPM_REFLECTION_CUBE

TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);
TEXTURE2D(_BumpMap);
SAMPLER(sampler_BumpMap);
TEXTURE2D(_EmissionMap);
SAMPLER(sampler_EmissionMap);
TEXTURE2D(_MetallicGlossMap);
SAMPLER(sampler_MetallicGlossMap);


// TEXTURE2D(_BaseMap);
// SAMPLER(sampler_BaseMap);


#endif
