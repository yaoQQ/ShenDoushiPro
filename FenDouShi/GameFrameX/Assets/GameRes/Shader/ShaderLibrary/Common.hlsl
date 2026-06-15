#ifndef CUSTOM_COMMON_INCLUDED
#define CUSTOM_COMMON_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

// 属性宏----------------------------------------
#define UPM_BASE_MAP \
    float4 _BaseMap_ST; \
    half4 _BaseColor; 

#define UPM_CARTOON_LIT \
    half _ShadowSmooth; \
    half _ShadowRange; \
    half4 _DiffColorA; \
    half4 _DiffColorB;

#define UPM_OUTLINE \
    half _Outline; \
    half4 _OutlineColor; 

#define UPM_PLANAR_SHADOW \
    half _HeightOffset; \
    half4 _ShadowColor; \
    half _ShadowFalloff; \
    half _ShadowCutoff;

// 暗屏----------------------------------------
uniform float _DrakScreenStrength;

void DrakScreen(inout half4 color)
{
    #ifdef _DRAKSCREEN_ON
        color.rgb *= _DrakScreenStrength;
    #endif
}

// 透明裁剪----------------------------------------
#define UPM_CUTOFF \
    half _Cutoff;

#define _CUTOFF _Cutoff

void AlphaClip(half4 alpha, half cutoff)
{
    #if defined(_ALPHATEST_ON)
        clip(alpha - cutoff);
    #endif
}

// 通用----------------------------------------
float2 Rotate2D(float2 UV, float Rotation = 0, float2 Center = float2(0.5, 0.5))
{
    Rotation = Rotation / 360 * 2 * PI;
    UV -= Center;
    float s = sin(Rotation);
    float c = cos(Rotation);
    float2x2 rMatrix = float2x2(c, -s, s, c);
    rMatrix *= 0.5;
    rMatrix += 0.5;
    rMatrix = rMatrix * 2 - 1;
    UV.xy = mul(UV.xy, rMatrix);
    UV += Center;
    return UV;
}

half remap(half x, half t1, half t2)
{
    return (x - t1) / (t2 - t1) ;
}

half remap(half x, half t1, half t2, half s1, half s2)
{
    return (x - t1) / (t2 - t1) * (s2 - s1) + s1;
}

// RGB转HSV
float3 RGBtoHSV(float3 c)
{
    float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
    float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

// HSV转RGB
float3 HSVtoRGB(float3 c)
{
    float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * lerp(K.xxx, saturate(p - K.xxx), c.y);
}

// 调整HSV值
float3 ApplyHSVAdjustment(float3 color, float hueShift, float saturation, float brightness)
{
    float3 hsv = RGBtoHSV(color);
    
    // 调整色相
    hsv.x = frac(hsv.x + hueShift);
    
    // 调整饱和度
    hsv.y = saturate(hsv.y * saturation);
    
    // 调整明度
    hsv.z = saturate(hsv.z * brightness);
    
    return HSVtoRGB(hsv);
}

// 调整HSV值
float3 ApplyHSVAdjustmentFast(float3 color, float brightness, float saturation, float contrast)
{
    // 调整明暗
    float3 adjustedColor = color.rgb * brightness;

    // 调整饱和度
    float luminance = dot(adjustedColor, float3(0.2126, 0.7152, 0.0722));
    adjustedColor = lerp(luminance, adjustedColor, saturation);

    // 调整对比度
    float midpoint = 0.5;
    adjustedColor = (adjustedColor - midpoint) * contrast + midpoint;
    
    return adjustedColor;
}

//置灰
float3 Grayscale(float3 color)
{
    // float gray = dot(color.rgb, float3(0.299, 0.587, 0.114));
    float gray = dot(color.rgb, float3(0.2126, 0.7152, 0.0722));
    return float3(gray, gray, gray);
}





#endif