#ifndef CUSTOM_ACTOR_SIMPLELIT_PASS_OUTLINE_INCLUDED
#define CUSTOM_ACTOR_SIMPLELIT_PASS_OUTLINE_INCLUDED

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
    float4 pos : SV_POSITION;
};

v2f vert(a2v v)
{
    v2f o;
    
    #if !defined(CURVEDWORLD_DISABLED_ON)
        CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
    #endif

    //o.pos = TransformObjectToHClip(v.vertex + v.normal * 0.1 * (_Outline * v.color.a));
    o.pos = TransformObjectToHClip(v.vertex + v.normal * 0.1 * (_Outline ));
    o.uv = TRANSFORM_TEX(v.texcoord, _BaseMap);

    return o;
}

float4 frag(v2f input) : SV_Target
{
    float4 color = _OutlineColor;

    Dissolution(color, input.uv);

    return color;
}

#endif
