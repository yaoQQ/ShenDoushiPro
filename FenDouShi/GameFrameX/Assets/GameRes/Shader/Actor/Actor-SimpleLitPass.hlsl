#ifndef CUSTOM_ACTOR_SIMPLELIT_PASS_INCLUDED
#define CUSTOM_ACTOR_SIMPLELIT_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "../ShaderLibrary/CartoonLit.hlsl"


struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
    float4 tangentOS : TANGENT;
    float2 texcoord : TEXCOORD0;
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float3 posWS : TEXCOORD2;    // xyz: posWS
    float3 normal : TEXCOORD3;
    float3 viewDir : TEXCOORD4;
    float3 posOS : TEXCOORD5;
    float4 positionCS : SV_POSITION;
};


void InitializeInputData(Varyings input, out CustomInputData inputData)
{
    inputData = (CustomInputData)0;

    inputData.positionWS = input.posWS;
    inputData.normalWS = NormalizeNormalPerPixel(input.normal);
    inputData.viewDirectionWS = SafeNormalize(input.viewDir);
    inputData.bakedGI = SampleSimpleGI(inputData.normalWS);
}

void InitializeSurfaceData(float2 uv, out CustomSurfaceData outSurfaceData)
{
    outSurfaceData = (CustomSurfaceData)0;

    half4 diffuseAlpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
    // half3 diffuse = diffuseAlpha.rgb * _BaseColor.rgb;
    half3 diffuse = ApplyHSVAdjustment(diffuseAlpha.rgb, _HueShift, _Saturation, _Brightness);
    diffuse *= _BaseColor.rgb;
    half alpha = diffuseAlpha.a * _BaseColor.a;

    AlphaClip(alpha, _Cutoff);

    outSurfaceData.albedo = diffuse;
    outSurfaceData.alpha = alpha;
    outSurfaceData.occlusion = 1;
    outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));

    outSurfaceData.shadowSmooth = _ShadowSmooth;
    outSurfaceData.shadowRange = _ShadowRange;
    outSurfaceData.diffColorA = _DiffColorA;
    outSurfaceData.diffColorB = _DiffColorB;
}

///////////////////////////////////////////////////////////////////////////////
//                  Vertex and Fragment functions                            //
///////////////////////////////////////////////////////////////////////////////

Varyings Vertex(Attributes input)
{
    Varyings output = (Varyings)0;

    #if !defined(CURVEDWORLD_DISABLED_ON)
        CURVEDWORLD_TRANSFORM_VERTEX(input.positionOS)
    #endif

    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
    half3 viewDirWS = GetWorldSpaceViewDir(vertexInput.positionWS);

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
    output.posWS.xyz = vertexInput.positionWS;
    output.positionCS = vertexInput.positionCS;
    output.normal = NormalizeNormalPerVertex(normalInput.normalWS);
    output.viewDir = viewDirWS;
    output.posOS = input.positionOS.xyz;
    
    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
    float2 uv = input.uv;

    CustomInputData inputData;
    InitializeInputData(input, inputData);
    CustomSurfaceData surfaceData;
    InitializeSurfaceData(uv, surfaceData);

    half4 color = CartoonLit(inputData, surfaceData);
    
    #ifdef _MATCAP
        // 采样 IDMap 和 MatcapMap，并计算反射向量的 UV
        half4 idColor = SAMPLE_TEXTURE2D(_IDMap, sampler_IDMap, uv);
        float3 reflection = normalize(reflect(-input.viewDir, inputData.normalWS));
        float2 matcapUV = reflection.xy * 0.5 + 0.5;

        // 采样 MatcapMap 和计算亮度
        half4 matcapColor = SAMPLE_TEXTURE2D(_MatcapMap, sampler_MatcapMap, matcapUV);
        half intensity = dot(matcapColor.rgb, float3(0.299, 0.587, 0.114)); // 计算亮度

        intensity = remap(intensity, _MatcapDarkContrast, _MatcapLightContrast) * _MatcapLitStrength;
        color.rgb = lerp(color, color * intensity * _MatcapColor, idColor.r).rgb;
    #endif

    // 边缘光
    {
        half fresnel = _EdgeRange - max(0, dot(inputData.normalWS, inputData.viewDirectionWS));
        fresnel = remap(clamp(fresnel, 0, _EdgeHardness), 0, _EdgeHardness);
        fresnel = fresnel * _EdgeTransparent;
        fresnel = fresnel * _IsEdgeLight;
        color.rgb = lerp(color, _EdgeColor, fresnel).rgb;
    }

    Dissolution(color, uv);

    // 计算世界Y坐标渐变
    float posY = _GradientObjectSpace == 0 ? input.posWS.y : (input.posWS.x>0? input.posOS.x : -input.posOS.x);
    float gradientFactor = saturate((posY - _GradientOffset) / _GradientHeight);
    half4 gradientColor = lerp(_GradientStartColor, _GradientEndColor, gradientFactor);
    
    // 混合主纹理和渐变颜色
    color.rgb = color.rgb * gradientColor.rgb;

    return color;
}

#endif
