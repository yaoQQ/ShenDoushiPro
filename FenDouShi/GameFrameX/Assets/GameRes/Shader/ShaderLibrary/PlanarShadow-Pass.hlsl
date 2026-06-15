#ifndef PLANAR_SHADOW_PASS_INCLUDED
#define PLANAR_SHADOW_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
#include "./CurvedWorldCore.hlsl"
#include "./Common.hlsl"

struct appdata
{
    float4 vertex : POSITION;
    float2 uv : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct v2f
{
    float4 vertex : SV_POSITION;
    float4 color : COLOR;
    float2 uv : TEXCOORD0;
    float4 worldPos : TEXCOORD1;

    UNITY_VERTEX_INPUT_INSTANCE_ID
};

float3 ShadowProjectPos(float4 vertPos)
{
    float3 shadowPos;

    //得到顶点的世界空间坐标
    float3 worldPos = TransformObjectToWorld(vertPos.rgb);

    float height = _HeightOffset ;

    //灯光方向
    Light mainLight = GetMainLight();
    float3 lightDir = normalize(mainLight.direction);

    //阴影的世界空间坐标（低于地面的部分不做改变）
    shadowPos.y = min(worldPos.y, height);
    shadowPos.xz = worldPos.xz - lightDir.xz * max(0, worldPos.y - height) / lightDir.y;

    return shadowPos;
}

v2f PlanarShadowVertex(appdata v)
{
    v2f o = (v2f)0;
    
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);

    //得到阴影的世界空间坐标
    float3 shadowPos = ShadowProjectPos(v.vertex);

    //曲面世界坐标
    #if !defined(CURVEDWORLD_DISABLED_ON)
        shadowPos = TransformWorldToObject(shadowPos);
        v.vertex = float4(shadowPos, 1);
        CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
        o.worldPos.xyz = TransformObjectToWorld(v.vertex.xyz);
        o.vertex = TransformWorldToHClip(o.worldPos);
    #else
        o.worldPos.xyz = shadowPos;
        o.vertex = TransformWorldToHClip(shadowPos);
    #endif

    o.uv = TRANSFORM_TEX(v.uv, _BaseMap);

    return o;
}

half4 PlanarShadowFragment(v2f i) : SV_Target
{
    UNITY_SETUP_INSTANCE_ID(i);

    half alpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, i.uv.xy).a;
    AlphaClip(alpha, _ShadowCutoff);

    //得到中心点世界坐标
    float2 center = float2(GetObjectToWorldMatrix()[0].w, GetObjectToWorldMatrix()[2].w);
    //计算阴影衰减
    float falloff = 1 - saturate(distance(i.worldPos.xz, center.xy) * _ShadowFalloff);

    //阴影颜色
    float4 color = _ShadowColor;
    color.a *= falloff;

    DrakScreen(color);

    return color;
}

#endif
