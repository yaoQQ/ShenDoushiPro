#ifndef ACTOR_CLOTH_INPUT_INCLUDED
#define ACTOR_CLOTH_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../ShaderLibrary/Common.hlsl"

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

    half4 _MatcapColor;
    half _MatcapMultiOne;
    half _MatcapMultiTwo;
    half _MatcapMultiThree;

    
    half _IsEdgeLight;
    half4 _EdgeColor;
    half _EdgeRange;
    half _EdgeTransparent;
    half _EdgeHardness;

    

    half _LaserRange;
    half _LaserHardness;
    float _LaserSaturation;

    float _HueShift;
    float _Saturation;
    float _Brightness;

    
    half _FresnelColorful;
    half _FresnelColorfulValue;
    half _ColorfulSmooth;
    half _Saturate;
    half _IsLaser;
    half _IsLaserLerp;
    
    float _MaxOutlineZOffset;
    float _OutlineZPostionInCamera;
    half _MixMainTexToOutline;
    half _VertexColorBlueAffectOutlineWitdh;
    half4 _OutlineOffset;
    half _OutlineWidth;
    half4 _OutlineColor;

    half _ShadowArea;
    half4 _StaticShadowColor;

    half _SpecularThreshod;
    half _SpecularSmooth;
    half _SpecularStrength;
    half4 _SpecularCol;

    half _ShadowSmooth;
    half _ShadowRange;
    half4 _DiffuseColorA;
    half4 _DiffuseColorB;

CBUFFER_END

half4 _EmissionColor;
half _BumpScale;

TEXTURE2D(_MatcapMap);
SAMPLER(sampler_MatcapMap);

// TEXTURE2D(_EmissionMap);
// SAMPLER(sampler_EmissionMap);


// UPM_REFLECTION_CUBE

TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);
TEXTURE2D(_BackMap);
SAMPLER(sampler_BackMap);
TEXTURE2D(_BumpMap);
SAMPLER(sampler_BumpMap);
TEXTURE2D(_EmissionMap);
SAMPLER(sampler_EmissionMap);
TEXTURE2D(_MetallicGlossMap);
SAMPLER(sampler_MetallicGlossMap);

TEXTURE2D(_SDS_Map);
SAMPLER(sampler_SDS_Map);

TEXTURE2D(_Laser_Map);
SAMPLER(sampler_Laser_Map);

TEXTURECUBE(_ReflectionMap1);
SAMPLER(sampler_ReflectionMap1);

TEXTURECUBE(_ReflectionMap2);
SAMPLER(sampler_ReflectionMap2);




// TEXTURE2D(_BaseMap);
// SAMPLER(sampler_BaseMap);


#endif
