#pragma once

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

CBUFFER_START(UnityPerMaterial)
    //色散
    half _ColorDispersionU;
    half _ColorDispersionV;
    half _ColorDispersionStrength;

    //径向模糊
    half _RadialBlurHorizontalCenter;
    half _RadialBlurVerticalCenter;
    half _RadialBlurWidth;
    half _RadialBlurStrength;
    half _RadialBlurIterTimes;

    //黑白屏
    half _BlackWhiteThreshold;
    half _BlackWhiteWidth;
    half4 _BlackWhiteWhiteColor;
    half4 _BlackWhiteBlackColor;
    half _BlackWhiteFlip;
CBUFFER_END

TEXTURE2D_X(_BlitTexture);
SAMPLER(sampler_BlitTexture);


#if SHADER_API_GLES
struct Attributes
{
    float3 positionOS : POSITION;
    float2 uv : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};
#else
struct Attributes
{
    uint vertexID : SV_VertexID;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};
#endif

struct Varyings
{
    float2 uv : TEXCOORD0;
    float4 screenPos : TEXCOORD2;
    float4 positionCS : SV_POSITION;
};

Varyings UberEffectPostVert(Attributes v)
{
    Varyings o = (Varyings) 0;

#if SHADER_API_GLES
    float4 pos = v.positionOS;
    float2 uv  = v.uv;
#else
    float4 pos = GetFullScreenTriangleVertexPosition(v.vertexID);
    float2 uv  = GetFullScreenTriangleTexCoord(v.vertexID);
#endif

    o.uv = uv;
    o.positionCS = pos;
    o.screenPos = ComputeScreenPos(o.positionCS);
    return o;
}

float4 SampleScreenColor(float2 uv)
{
    return SAMPLE_TEXTURE2D_X(
        _BlitTexture,
        sampler_BlitTexture,
        UnityStereoTransformScreenSpaceTex(uv)
    );
}


half4 BlackWhite(half4 screenColor)
{
    half4 result = screenColor;

    #if _BLACKWHITE
        half luminosity = dot(screenColor.rgb, half3(0.299, 0.587, 0.114));
        float smoothstepResult = smoothstep(_BlackWhiteThreshold, _BlackWhiteThreshold + _BlackWhiteWidth, luminosity.x);
        smoothstepResult = _BlackWhiteFlip > 0.5 ? 1 - smoothstepResult : smoothstepResult;
        result = lerp(_BlackWhiteWhiteColor, _BlackWhiteBlackColor, smoothstepResult);
    #endif

    return result;
}

half4 ColorDispersion(float2 screenUV, half4 screenColor)
{
    half4 result = screenColor;

    #if _COLORDISPERSION
        #if _BLACKWHITE
            screenColor = BlackWhite(screenColor);
        #endif

        half2 deltaUv = half2(_ColorDispersionStrength * _ColorDispersionU, _ColorDispersionStrength * _ColorDispersionV);

        half4 tempScreenColor = SampleScreenColor(screenUV + deltaUv);
        #if _BLACKWHITE
            tempScreenColor = BlackWhite(tempScreenColor);
        #endif
        result.r = tempScreenColor.r;

        result.g = screenColor.g;

        tempScreenColor = SampleScreenColor(screenUV - deltaUv);
        #if _BLACKWHITE
            tempScreenColor = BlackWhite(tempScreenColor);
        #endif
        result.b = tempScreenColor.b;
    #endif

    return result;
}

half4 RadialBlur(half2 screenUV, half4 screenColor)
{
    half4 result = screenColor;

    #if _RADIALBLUR
        half2 dir = screenUV - half2(_RadialBlurHorizontalCenter, _RadialBlurVerticalCenter);

        half4 blur = 0;
        for (int i = 1; i < _RadialBlurIterTimes; ++i)
        {
            half2 uv = screenUV + _RadialBlurWidth * dir * i;
            blur += SampleScreenColor(uv);
        }
        blur /= 6;

        result = lerp(result, blur, saturate(_RadialBlurStrength));
    #endif

    return result;
}

half4 RadialBlurV2(float2 screenUV, half4 screenColor)
{
    half4 result = screenColor;

    #if _RADIALBLUR
        #if _BLACKWHITE
            screenColor = BlackWhite(screenColor);
        #endif

        half2 dir = screenUV - half2(_RadialBlurHorizontalCenter, _RadialBlurVerticalCenter);
        half dist = length(dir);
        dir *= rcp(dist);

        const half weights[16] = {
            - 0.18, -0.14, -0.11, -0.08, -0.05, -0.03, -0.02, -0.01, 0.01, 0.02, 0.03, 0.05, 0.08, 0.11, 0.14, 0.18
        };

        half4 sum = screenColor;
        int iterStart = (16 - _RadialBlurIterTimes) / 2;
        int iterEnd = 16 - iterStart;
        for (int i = iterStart; i < iterEnd; ++i)
        {
            half2 uv = screenUV + _RadialBlurWidth * dir * weights[i];
            half4 tempScreenColor = SampleScreenColor(uv);
            #if _BLACKWHITE
                tempScreenColor = BlackWhite(tempScreenColor);
            #endif
            sum += tempScreenColor;
        }
        sum *= rcp(_RadialBlurIterTimes + 1);

        result = lerp(screenColor, sum, saturate(dist * _RadialBlurStrength));
    #endif

    return result;
}

half4 UberEffectPostFrag(Varyings input) : SV_Target
{
    half2 screenUV = input.screenPos.xy / input.screenPos.w;

    half4 screenColor = SampleScreenColor(screenUV);
    half4 result = screenColor;

    #if _RADIALBLUR && _COLORDISPERSION
        half2 deltaUv = half2(_ColorDispersionStrength * _ColorDispersionU, _ColorDispersionStrength * _ColorDispersionV);
        result.r = RadialBlurV2(screenUV + deltaUv, result).r;
        result.g = RadialBlurV2(screenUV, result).g;
        result.b = RadialBlurV2(screenUV - deltaUv, result).b;
    #elif _RADIALBLUR
        result = RadialBlurV2(screenUV, result);
    #elif _COLORDISPERSION
        result = ColorDispersion(screenUV, result);
    #elif _BLACKWHITE
        result = BlackWhite(result);
    #endif

    return half4(result.rgb, screenColor.a);
}
