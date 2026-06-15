#ifndef ACTOR_LIT_PASS_INCLUDE
#define ACTOR_LIT_PASS_INCLUDE

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
    half3 diffuse = ApplyHSVAdjustmentFast(diffuseAlpha.rgb, 1, _Saturation, 1);
    diffuse *= _BaseColor.rgb;
    diffuse =  lerp(lerp(diffuse,diffuse * _OutlineColor.rgb,_InsideLineWidth) ,diffuse,diffuseAlpha.a  );
    
    #if defined(_LIGHT_DEBUG)
    diffuse.xyz = half3(0.5, 0.5, 0.5);
    #endif
    half alpha =  _BaseColor.a;

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
    outSurfaceData.smoothness = clamp(metallicGlossMap.g   + (sdsColor.g > 0.5 ? _SmoothnessOffset : _SmoothnessOffset2), 0, 1);
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

    half4 sdsColor = SAMPLE_TEXTURE2D(_SDS_Map, sampler_SDS_Map, uv);

    float3 reflection = normalize(reflect(-input.viewDir, inputData.normalWS));
    half NoV = saturate(dot(inputData.normalWS, inputData.viewDirectionWS));

    half reflectFlag = sdsColor.g;
    half3 reflectColor = 0;

    half4 reflection1 = SAMPLE_TEXTURECUBE(_ReflectionMap1, sampler_ReflectionMap1, reflection);
    half4 reflection2 = SAMPLE_TEXTURECUBE(_ReflectionMap2, sampler_ReflectionMap2, reflection);
    if(reflectFlag > 0.5){
        reflectColor = reflection1.rgb ;
    }else{
        reflectColor = reflection2.rgb ;
    }

    // inputData.bakedGI = SampleSimpleGI(normalWS);


    color = PbrLit_Actor(inputData, surfaceData, reflection, NoV,reflectColor);


    half3 viewNormal = TransformWorldToViewNormal(inputData.normalWS);

    #ifdef _MATCAP
    half matCapFlag = sdsColor.r;

    half2 matcapUV = viewNormal.xy * 0.5 + 0.5;

    half4 matcapColor = 0;
    half4 matcapTexColor = SAMPLE_TEXTURE2D(_MatcapMap, sampler_MatcapMap, matcapUV);

    half matcapIntensity = matcapTexColor.r * _MatcapMultiOne;
    
    // 多MatCap混合
    if (matCapFlag >= 0.3)
    {
        half secondMatCap = matcapTexColor.g * _MatcapMultiTwo - matcapIntensity;
        matcapIntensity += secondMatCap;
    }
    
    if (matCapFlag >= 0.6)
    {
        half thirdMatCap = matcapTexColor.b * _MatcapMultiThree - matcapIntensity;
        matcapIntensity += thirdMatCap;
    }
    
    color.rgb *= matcapIntensity;
    // color.rgb = matcapColor;
    #endif

    // 边缘光
    {
        half insideLine = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, uv).a;
        half fresnel = _EdgeRange - max(0, dot(inputData.normalWS, inputData.viewDirectionWS));
        fresnel = remap(clamp(fresnel, 0, _EdgeHardness), 0, _EdgeHardness);
        fresnel = fresnel * _EdgeTransparent;
        fresnel = fresnel * _IsEdgeLight;
        color.rgb = lerp(color, _EdgeColor, fresnel*insideLine).rgb;
    }
    

    Light mainLight = GetMainLight();
    
    float ndl = dot(inputData.normalWS, mainLight.direction);
    half ndv  =  max(0, dot(inputData.normalWS, inputData.viewDirectionWS));
    half fresnel2 = _LaserRange - ndv;
    fresnel2 = remap(clamp(fresnel2, 0, _LaserHardness), 0, _LaserHardness);

    float2 l_uv = float2(fresnel2,lerp(ndl,fresnel2,0.25));

    half4 laserColor2 = SAMPLE_TEXTURE2D(_Laser_Map, sampler_Laser_Map, l_uv);
    // color.rgb =  laserColor2.rgb* _LaserSaturation;
    // return color;
    color.rgb = lerp(color.rgb ,color.rgb * laserColor2.rgb,_LaserSaturation);
 

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

