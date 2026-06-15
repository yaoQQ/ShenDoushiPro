#ifndef CUSTOM_ACTOR_SIMPLELIT_INPUT_INCLUDED
#define CUSTOM_ACTOR_SIMPLELIT_INPUT_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "../ShaderLibrary/CurvedWorldCore.hlsl"
#include "../ShaderLibrary/Common.hlsl"

CBUFFER_START(UnityPerMaterial)
    UPM_BASE_MAP
    UPM_CUTOFF
    
    half4 _EmissionColor;
    
    half4 _MatcapColor;
    half _MatcapLightContrast;
    half _MatcapDarkContrast;
    half _MatcapLitStrength;

    half _IsEdgeLight;
    half4 _EdgeColor;
    half _EdgeRange;
    half _EdgeTransparent;
    half _EdgeHardness;
    
    half _DissolutionOffset;
    half _DissolutionMask;
    half _DissolutionHdrStrength;
    half4 _DissolutionColor;


    float _HueShift;
    float _Saturation;
    float _Brightness;

    float _GradientObjectSpace;
    float4 _GradientStartColor;
    float4 _GradientEndColor;
    float _GradientHeight;
    float _GradientOffset;

    UPM_CARTOON_LIT
    UPM_OUTLINE
    UPM_PLANAR_SHADOW
CBUFFER_END


TEXTURE2D(_BaseMap);
SAMPLER(sampler_BaseMap);
TEXTURE2D(_EmissionMap);
SAMPLER(sampler_EmissionMap);
TEXTURE2D(_DissolutionMap);
SAMPLER(sampler_DissolutionMap);
TEXTURE2D(_IDMap);
SAMPLER(sampler_IDMap);
TEXTURE2D(_MatcapMap);
SAMPLER(sampler_MatcapMap);

void Dissolution(inout half4 color, float2 uv)
{
    #ifdef _DEADDISSOLUTION_ON
        half shouldApplyDissolution = step(_DissolutionMask, 0.99);
        half noise = SAMPLE_TEXTURE2D(_DissolutionMap, sampler_DissolutionMap, uv).r;
        half temp1 = step(noise, _DissolutionMask);
        half temp2 = step(noise + _DissolutionOffset, _DissolutionMask);
        half temp3 = temp1 - temp2;
        half alphaValue = (temp1 + temp3) * color.a;
        AlphaClip(alphaValue, shouldApplyDissolution * 0.5);
        color.rgb = lerp(color.rgb, _DissolutionColor.rgb * _DissolutionHdrStrength, temp3 * shouldApplyDissolution);
    #endif
}

#endif
