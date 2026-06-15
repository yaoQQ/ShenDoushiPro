#ifndef CURVEDWORLD_CORE
#define CURVEDWORLD_CORE

#define CURVEDWORLD_TRANSFORM_VERTEX(v)                  CurvedWorld_CylindricalRolloff_Z_ID1(v);	

uniform float3 CurvedWorld_CylindricalRolloff_Z_ID1_PivotPoint;
uniform float CurvedWorld_CylindricalRolloff_Z_ID1_BendSize;    
uniform float CurvedWorld_CylindricalRolloff_Z_ID1_BendOffset;


float3 CurvedWorld_ObjectToWorld(float4 vertexOS)
{
	#ifdef SCRIPTABLE_RENDER_PIPELINE 
		return GetAbsolutePositionWS(TransformObjectToWorld(vertexOS.xyz));
	#else
		return mul(unity_ObjectToWorld, vertexOS).xyz;
	#endif
}

float3 CurvedWorld_WorldToObject(float4 vertexWS, float HDRPCoef)
{
	#ifdef SCRIPTABLE_RENDER_PIPELINE

		#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
			vertexWS.xyz -= _WorldSpaceCameraPos * HDRPCoef;	//HDRPCoef is always 1 for URP. In HDRP for Spiral bend types = 1, for Cylindricals = 0. 
		#endif

		return mul(GetWorldToObjectMatrix(), vertexWS).xyz;
	#else
		return mul(unity_WorldToObject, vertexWS).xyz;
	#endif
}

void CurvedWorld_CylindricalRolloff_Z(inout float4 inVertexOS, float3 pivotPoint, float3 bendSize, float3 bendOffset)
{
    float3 positionWS = CurvedWorld_ObjectToWorld(inVertexOS);
	positionWS -= pivotPoint;

	float2 offset = max(float2(0, 0), abs(positionWS.zx) - bendOffset.xx);
	offset *= step(float2(0, 0), positionWS.zx) * 2 - 1;
	offset *= offset;
	positionWS = float3(0, -bendSize.x * offset.x, 0) * 0.001;

	inVertexOS.xyz += CurvedWorld_WorldToObject(float4(positionWS, 0), 0);
}

void CurvedWorld_CylindricalRolloff_Z_ID1(inout float4 vertexOS)
{
    CurvedWorld_CylindricalRolloff_Z(vertexOS, 
	                        CurvedWorld_CylindricalRolloff_Z_ID1_PivotPoint,
							CurvedWorld_CylindricalRolloff_Z_ID1_BendSize,
							CurvedWorld_CylindricalRolloff_Z_ID1_BendOffset);
}

#endif

