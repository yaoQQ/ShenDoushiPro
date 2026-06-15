#ifndef ACTOR_SKIN_PASS_INCLUDE
#define ACTOR_SKIN_PASS_INCLUDE

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
// #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/SurfaceInput.hlsl"
// #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "../ShaderLibrary/Common.hlsl"
#include "../ShaderLibrary/Lighting/PbrLit.hlsl"

// CBUFFER_START(UnityPerMaterial)
//     UPM_BASE_MAP
//     UPM_CUTOFF
// CBUFFER_END

struct Attributes
{
    float4 positionOS : POSITION;
    float3 normalOS : NORMAL;
    float4 tangentOS : TANGENT;
    float2 texcoord : TEXCOORD0;
    float2 staticLightmapUV   : TEXCOORD1;
};

struct Varyings
{
    float2 uv : TEXCOORD0;
    float3 posWS : TEXCOORD2;    // xyz: posWS
    float3 normalWS : TEXCOORD3;
    float3 viewDir : TEXCOORD4;
    float2 staticLightmapUV : TEXCOORD5;
    float4 shadowCoord              : TEXCOORD6;
    half4 tangentWS                : TEXCOORD7;  
    float4 positionCS : SV_POSITION;
};

void InitializeInputData(Varyings input, half3 normalTS, out CustomInputData inputData)
{
    inputData = (CustomInputData)0;

    inputData.positionWS = input.posWS;

#if defined(_NORMALMAP)
    float sgn = input.tangentWS.w;      // should be either +1 or -1
    float3 bitangent = sgn * cross(input.normalWS.xyz, input.tangentWS.xyz);
    half3x3 tangentToWorld = half3x3(input.tangentWS.xyz, bitangent.xyz, input.normalWS.xyz);

    inputData.tangentToWorld = tangentToWorld;
    inputData.normalWS = TransformTangentToWorld(normalTS, tangentToWorld);
#else
    inputData.normalWS = input.normalWS;
#endif
    inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);

    inputData.viewDirectionWS = SafeNormalize(input.viewDir);
    inputData.bakedGI =  SampleSimpleGI(inputData.normalWS);

    #if defined(LIGHTMAP_ON)
        half3 lightmapColor = SampleLightmap(input.staticLightmapUV, 0, inputData.normalWS);
        lightmapColor = ApplyHSVAdjustmentFast(lightmapColor, _LightmapBrightness, _LightmapSaturation, _LightmapContrast);
        inputData.bakedGI = lerp(inputData.bakedGI + lightmapColor, lightmapColor, _GIMixStrength);
    #endif

    #if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    inputData.shadowCoord = input.shadowCoord;
#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
    inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
#else
    inputData.shadowCoord = float4(0, 0, 0, 0);
#endif
}

void InitializeSurfaceData(float2 uv, out CustomSurfaceData outSurfaceData)
{
    outSurfaceData = (CustomSurfaceData)0;

    half4 diffuseAlpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv);
    // half3 diffuse = diffuseAlpha.rgb * _BaseColor.rgb;
    // half3 diffuse = ApplyHSVAdjustment(diffuseAlpha.rgb, _HueShift, _Saturation, _Brightness);
    half3 diffuse = ApplyHSVAdjustmentFast(diffuseAlpha.rgb, 1, _Saturation, 1);
    diffuse *= _BaseColor.rgb;
    #if defined(_LIGHT_DEBUG)
    diffuse.xyz = half3(0.5, 0.5, 0.5);
    #endif
    half alpha = diffuseAlpha.a * _BaseColor.a;

    AlphaClip(alpha, _Cutoff);
    outSurfaceData.occlusion = 1;
    outSurfaceData.albedo = diffuse;
    outSurfaceData.alpha = alpha;
    
    outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));
    
    outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _BumpScale);

    #if defined(_METALLICGLOSSMAP)
    half4 metallicGlossMap = SAMPLE_TEXTURE2D(_MetallicGlossMap, sampler_MetallicGlossMap, uv);


    half4 sdsColor = SAMPLE_TEXTURE2D(_SDS_Map, sampler_SDS_Map, uv);

    outSurfaceData.metallic = clamp(metallicGlossMap.r   + _MetallicOffset, 0, 1);
    outSurfaceData.smoothness = clamp(metallicGlossMap.g   + _SmoothnessOffset*sdsColor.g, 0, 1);
    outSurfaceData.occlusion =  lerp(1, metallicGlossMap.b, _OcclusionStrength);
    #else
    outSurfaceData.metallic = _Metallic;
    outSurfaceData.smoothness = _Smoothness;
    #endif

}

///////////////////////////////////////////////////////////////////////////////
//                  Vertex and Fragment functions                            //
///////////////////////////////////////////////////////////////////////////////

Varyings Vertex(Attributes input)
{
    Varyings output = (Varyings)0;


    VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
    VertexNormalInputs normalInput = GetVertexNormalInputs(input.normalOS, input.tangentOS);
    half3 viewDirWS = GetWorldSpaceViewDir(vertexInput.positionWS);

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
    output.posWS.xyz = vertexInput.positionWS;
    output.positionCS = vertexInput.positionCS;
    output.normalWS = NormalizeNormalPerVertex(normalInput.normalWS);
    
#if defined(_NORMALMAP)
    real sign = input.tangentOS.w * GetOddNegativeScale();
    half4 tangentWS = half4(normalInput.tangentWS.xyz, sign);
    output.tangentWS = tangentWS;
#endif

    output.viewDir = viewDirWS;
    OUTPUT_LIGHTMAP_UV(input.staticLightmapUV, unity_LightmapST, output.staticLightmapUV);
    
    
#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
    output.shadowCoord = GetShadowCoord(vertexInput);
    // output.shadowCoord = half4(1,0,0,0);
#endif

    return output;
}

half4 Fragment(Varyings input) : SV_Target
{
    half4 color = 0;
    float2 uv = input.uv;

    CustomSurfaceData surfaceData;
    InitializeSurfaceData(uv, surfaceData);

    CustomInputData inputData;
    InitializeInputData(input, surfaceData.normalTS, inputData);


    Light light = GetMainLight();

    #if defined(_FACE_ON)
        light.direction = _FaceLightDirection.xyz;
        light.color = _FaceLightColor.rgb;
    #endif

    float NdotL = dot(inputData.normalWS, light.direction);
    NdotL = saturate(NdotL * 0.5 + 0.5); // 转换为0-1范围

    half3 halfDir = normalize(input.viewDir + light.direction);
    float NdotH = dot(inputData.normalWS, halfDir);
    NdotH = saturate(NdotH);

    half ramp = smoothstep(0, _ShadowSmooth, NdotL - _ShadowRange);
    

    #if defined(_FACE_ON)
        ramp  = clamp(ramp + surfaceData.occlusion,0,1);
    #else
        ramp *= surfaceData.occlusion;
    #endif
    ramp *= surfaceData.alpha;

    half3 diffuse = lerp(_DiffuseColorB, _DiffuseColorA, ramp );

    color.rgb = surfaceData.albedo * diffuse * light.color;


    DrakScreen(color);

    AlphaClip(color.a,_CUTOFF);

    #if defined(_COLOR_DEBUG)
    color.rgb = surfaceData.albedo;
    #endif

    #if defined(_GRAY_DEBUG)
    color.rgb = Grayscale(color.rgb);
    #endif

    color.rgb = clamp(color.rgb,0,10);

    return color;
}

#endif

