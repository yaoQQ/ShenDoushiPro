#ifndef DIMIAN_PASS_INCLUDED
#define DIMIAN_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "../ShaderLibrary/Common.hlsl"
#include "../ShaderLibrary/CurvedWorldCore.hlsl"
#include "Scene-Common.hlsl"

CBUFFER_START(UnityPerMaterial)
    UPM_BASE_MAP
    UPM_CUTOFF
CBUFFER_END

struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float4 vertex : SV_POSITION;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

Varyings Vertex(Attributes input)
{
    Varyings output = (Varyings)0;

    UNITY_SETUP_INSTANCE_ID(input);
    UNITY_TRANSFER_INSTANCE_ID(input, output);

    #if !defined(CURVEDWORLD_DISABLED_ON)
        CURVEDWORLD_TRANSFORM_VERTEX(input.positionOS)
    #endif

    output.vertex = TransformObjectToHClip(input.positionOS);
    output.uv = TRANSFORM_TEX(input.uv, _BaseMap);

    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(input);

    float2 uv = input.uv;
    half4 diffuseAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
    
    half4 color = diffuseAlpha * _BaseColor;

    DrakScreen(color);

    AlphaClip(color.a,_CUTOFF);

    return color;
}

#endif

