#ifndef ACTOR_SKIN_OUTLINE_INCLUDED
#define ACTOR_SKIN_OUTLINE_INCLUDED



struct a2v
{
    float4 vertex : POSITION;
    float3 normal : NORMAL;
    float2 texcoord : TEXCOORD0;
    half4 color : COLOR;
};

struct v2f
{
    float2 uv : TEXCOORD0;
    float4 color :TEXCOORD1;
    float4 pos : SV_POSITION;

};

v2f vert(a2v v)
{
    v2f o;
    
    float3 offset =  v.normal * _OutlineWidth * v.color.a;
    offset = clamp(offset, -_MaxOutlineZOffset, _MaxOutlineZOffset);
    offset *=  0.0005;

    v.vertex.xyz += offset ;
    
    // 计算世界空间位置
    float4 worldPos = mul(unity_ObjectToWorld, v.vertex);

    // 转换到裁剪空间
    float4 vpos = mul(UNITY_MATRIX_V, worldPos);

    #if defined(SHADER_API_GLCORE) || defined(SHADER_API_GLES) || defined(SHADER_API_GLES3)
        vpos.x += _OutlineOffset.x * 0.0005;
        vpos.y -= _OutlineOffset.y * 0.0005;
        vpos.z += _OutlineOffset.z * 0.0005;
    #else
        vpos.x += _OutlineOffset.x * 0.0005;
        vpos.y -= _OutlineOffset.y * 0.0005;
        vpos.z -= _OutlineOffset.z * 0.0005;
    #endif


    o.pos = mul(UNITY_MATRIX_P, vpos);
    o.uv = TRANSFORM_TEX(v.texcoord, _BaseMap);
    o.color = v.color;

    return o;
}

float4 frag(v2f input) : SV_Target
{
    float4 color = _OutlineColor;
    
    half4 diffuseAlpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv);
    color.rgb = _MixMainTexToOutline == 1 ? diffuseAlpha.rgb * color.rgb : color.rgb;
    
    //Dissolution(color, input.uv);

    return color;
}


#endif
