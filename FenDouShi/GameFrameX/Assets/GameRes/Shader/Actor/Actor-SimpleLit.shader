Shader "R2/Actor/Actor-SimpleLit"
{
    Properties
    {
        //-------------------------------------------------
        //               Render Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Render ("渲染设置", Float) = 1

        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend ("SrcBlend", Float) = 1.0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend ("DstBlend", Float) = 0.0
        [Enum(Off, 0, On, 1)]_ZWrite ("ZWrite", Float) = 1.0
        [Enum(UnityEngine.Rendering.CompareFunction)]_ZTest ("ZTest", Float) = 4
        [Enum(UnityEngine.Rendering.CullMode)]_Cull ("Cull", Float) = 2.0

        [Toggle(_ALPHATEST_ON)]_AlphaClip ("AlphaClip", Float) = 1.0
        [StyledIndentLevelAdd]
        [StyledIfShow(_AlphaClip)][StyledField]_Cutoff ("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        [StyledIndentLevelSub]
        
        //-------------------------------------------------
        //               Env Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Env ("环境设置", Float) = 1
        [Toggle(CURVEDWORLD_DISABLED_ON)]_CurvedWorldOn ("关闭曲面影响", Float) = 0.0

        //-------------------------------------------------
        //               Surface Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Surface ("着色设置", Float) = 1

        [StyledTextureSingleLine(_BaseColor)][MainTexture] _BaseMap ("颜色图", 2D) = "white" { }
        [HideInInspector][MainColor]_BaseColor ("Color", Color) = (1, 1, 1, 1)

        _Outline ("描边粗细", Range(0, 1)) = 0.05
        _OutlineColor ("描边颜色", Color) = (0, 0, 0, 1)

        [Space(18)]
        _ShadowSmooth ("明暗交界线平滑度", Range(0, 1)) = 0.5
        _ShadowRange ("明暗范围控制", Range(0, 1)) = 0.5
        _DiffColorA ("亮部颜色", Color) = (1, 1, 1, 1)
        _DiffColorB ("暗部颜色", Color) = (0, 0, 0, 1)

        [Space(18)]
        [StyledKeywordTextureSingleLine(_EMISSION, _EmissionColor)]_EmissionMap ("自发光贴图", 2D) = "white" { }
        [HideInInspector][HDR] _EmissionColor ("Color", Color) = (0, 0, 0)

        [Space(18)]
        [StyledTexST(_BaseMap)] _Temp_ST_1 ("_Temp_ST_1", Float) = 0

        // HSV控制参数
        _HueShift ("Hue Shift", Range(0, 1)) = 0
        _Saturation ("Saturation", Range(0, 2)) = 1
        _Brightness ("Brightness", Range(0, 2)) = 1

        [Toggle]_GradientObjectSpace ("Gradient Object Space", Float) = 0
        _GradientStartColor ("Gradient Start Color", Color) = (1,1,1,1)
        _GradientEndColor ("Gradient End Color", Color) = (1,1,1,1)
        _GradientHeight ("Gradient Height", Float) = 10
        _GradientOffset ("Gradient Offset", Float) = 0

        //-------------------------------------------------
        //               Matcap Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Matcap ("Matcap", Float) = 1
        [StyledTextureSingleLine]_IDMap ("ID贴图", 2D) = "white" { }
        [StyledKeywordTextureSingleLine(_MATCAP, _MatcapColor)]_MatcapMap ("MATCAP贴图", 2D) = "white" { }
        [HideInInspector]_MatcapColor ("MATCAP颜色", Color) = (1, 1, 1, 1)
        _MatcapDarkContrast ("MATCAP亮部对比度", Range(-2, 0.5)) = -1.525
        _MatcapLightContrast ("MATCAP暗部对比度", Range(0.5, 2)) = 1.855
        _MatcapLitStrength ("MATCAP补充整体亮度", Range(0, 5)) = 2.6

        //-------------------------------------------------
        //               Edge Color Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Edge_Color ("边缘光", Float) = 1
        [Toggle]_IsEdgeLight ("边缘光开关", Float) = 0.0
        [HDR][StyledAlignedLeft][StyledField]_EdgeColor ("边缘光颜色", Color) = (1, 0, 0, 1)
        _EdgeRange ("边缘光范围", Range(0,1)) = 0.95
        _EdgeTransparent ("边缘光通透度", Range(0, 1)) = 0.9
        _EdgeHardness("边缘光硬度", Range(0.01, 1)) = 0.98

        //-------------------------------------------------
        //               Dead Dissolution Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Dead_Dissolution ("死亡消融", Float) = 1
        [Toggle]_DeadDissolution ("死亡消融开关", Float) = 1.0
        [StyledTextureSingleLine(_DissolutionColor)]_DissolutionMap ("消融贴图", 2D) = "white" { }
        [HideInInspector]_DissolutionColor ("消融颜色", Color) = (0.3018867, 0.333, 0.316, 1)
        _DissolutionHdrStrength ("消融发光强度", Range(1, 20)) = 1
        _DissolutionMask ("消融进度", Range(0, 1)) = 1
        _DissolutionOffset ("消融范围", Range(0, 1)) = 0.075

        //-------------------------------------------------
        //               Planar Shadow Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Planar_Shadow ("平面阴影", Float) = 1
        [StyledPassOff(PlanarShadowPass)]_DisablePlanarShadowPass ("关闭平面阴影", Float) = 0.0
        // [Toggle]_3DMax ("Z-to-Y", Int) = 1
        [StyledAlignedLeft][StyledField]_ShadowColor ("阴影颜色", Color) = (0.1294117, 0.1999999, 0.2392156, 0.572549)
        _HeightOffset ("阴影高度", Range(-5, 5)) = 0
        _ShadowFalloff ("阴影透明度", Range(0, 10)) = 0
        _ShadowCutoff ("阴影透明裁剪", Range(0.0, 1.0)) = 0.85

        //-------------------------------------------------
        //               Advanced Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Advanced ("高级设置", Float) = 1
        
        //-------------------------------------------------
        //               Obsolete Properties
        //-------------------------------------------------
        [HideInInspector] _MainTex ("BaseMap", 2D) = "white" { }
    }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" "UniversalMaterialType" = "SimpleLit" "IgnoreProjector" = "True" }

        // ForwardLit
        Pass
        {
            Name "BaseColor"

            Blend[_SrcBlend][_DstBlend]
            ZWrite[_ZWrite]
            ZTest [_ZTest]
            Cull[_Cull]

            HLSLPROGRAM
            #pragma target 3.5

            // -------------------------------------
            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local_fragment _EMISSION
            
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON
            #pragma shader_feature_local_fragment _DEADDISSOLUTION_ON
            #pragma shader_feature_local_fragment _MATCAP
            
            // #define _ADDITIONAL_LIGHTS

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "Actor-SimpleLitInput.hlsl"
            #include "Actor-SimpleLitPass.hlsl"
            ENDHLSL
        }

        
        Pass
        {
            Name "Outline"

            Tags { "LightMode" = "UniversalForward" }

            Cull Front
            ZWrite On
            ZTest LEqual
            HLSLPROGRAM

            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON
            #pragma shader_feature_local_fragment _DEADDISSOLUTION_ON

            #include "Actor-SimpleLitInput.hlsl"
            #include "Actor-SimpleLitPass-Outline.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag

            ENDHLSL
        }


        // PlanarShadow
        Pass
        {
            Name "PlanarShadow"
            Tags { "LightMode" = "PlanarShadowPass" }

            //用使用模板测试以保证alpha显示正确
            Stencil
            {
                Ref 0
                Comp equal
                Pass incrWrap
                Fail keep
                ZFail keep
            }

            Cull Back
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite off
            //深度稍微偏移防止阴影与地面穿插
            Offset -1, 0
            
            HLSLPROGRAM
            #pragma target 3.5

            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON

            #pragma vertex PlanarShadowVertex
            #pragma fragment PlanarShadowFragment

            #include "Actor-SimpleLitInput.hlsl"
            #include "../ShaderLibrary/PlanarShadow-Pass.hlsl"

            ENDHLSL
        }
    }
    CustomEditor "TA_Tools.StyledEditor.StyledMaterial.MaterialCoreGUI"
}
