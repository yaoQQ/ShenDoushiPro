Shader "R2/Actor/Actor-Lit"
{
    Properties
    {
         //-------------------------------------------------
        //               Render Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Render("渲染设置", Float) = 1

        [Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("SrcBlend", Float) = 1.0
        [Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("DstBlend", Float) = 0.0
        [Enum(Off, 0, On, 1)]_ZWrite("ZWrite", Float) = 1.0
        [Enum(UnityEngine.Rendering.CompareFunction)]_ZTest ("ZTest", Float) = 4
        [Enum(UnityEngine.Rendering.CullMode)]_Cull("Cull", Float) = 2.0

        [Toggle(_ALPHATEST_ON)]_AlphaClip("AlphaClip", Float) = 0.0
        [StyledIndentLevelAdd]
        [StyledIfShow(_AlphaClip)][StyledField]_Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.5
        [StyledIndentLevelSub]

        //-------------------------------------------------
        //               Color Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Color("颜色设置", Float) = 1

        [MainTexture] _BaseMap("基础贴图", 2D) = "white" {}
        [MainColor]   _BaseColor("基础颜色", Color) = (1, 1, 1, 1)
        _Saturation ("饱和度", Range(0, 2)) = 1

        [StyledKeywordTextureSingleLine(_METALLICGLOSSMAP)]_MetallicGlossMap("PBR贴图(R:金属,G:粗糙,B:AO)", 2D) = "white" { }
        _Metallic("金属度", Range(0, 1)) = 0.0
        _Smoothness("光泽度", Range(0, 1)) = 0.5
        _MetallicOffset("金属度偏移", Range(-1, 1)) = 0.0
        _SmoothnessOffset("光泽度偏移1", Range(-1, 1)) = 0.0
        _SmoothnessOffset2("光泽度偏移2", Range(-1, 1)) = 0.0
        _OcclusionStrength("遮挡强度", Range(0, 1)) = 1.0

        [StyledKeywordTextureSingleLine(_NORMALMAP, _BumpScale)]_BumpMap("法线贴图", 2D) = "bump" {}
        [HideInInspector]_BumpScale("法线强度", Range(0, 10)) = 1.0
      

        //-------------------------------------------------
        //               Outline Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Outline("描边设置", Float) = 1
        _OutlineColor ("描边颜色", Color) = (0, 0, 0, 1)
		[ToggleOff] _MixMainTexToOutline ("描边混合主贴图", Float ) = 1.0
        _OutlineWidth ("描边粗细", Range(0, 10)) = 0.05
        
        _MaxOutlineZOffset("描边最大粗细", Range(0, 10)) = 0.05
        _OutlineOffset ("描边偏移", Vector) = (0, 0, 0, 0)
        _InsideLineWidth ("内描边粗细", Range(0, 1)) = 0.05

        //-------------------------------------------------
        //               Sdscap Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Sdscap("SDSMAP设置(R:主副Matcap区间(0>0.3>0.6>1),G:主副Reflect区间,A:自发光区间)", Float) = 1
        _SDS_Map ("SDS贴图", 2D) = "white" { }
        _ReflectionMap1 ("反射贴图1", Cube) = "white" { }
        _ReflectionMap2 ("反射贴图2", Cube) = "white" { }
        
        [StyledKeywordTextureSingleLine(_MATCAP, _MatcapColor)]_MatcapMap ("MATCAP贴图(R:第一层,G:第二层,B:第三层)", 2D) = "white" { }
        [HideInInspector]_MatcapColor ("MATCAP颜色", Color) = (1, 1, 1, 1)
        _MatcapMultiOne("第一层亮度", Range(0, 10)) = 1.0
        _MatcapMultiTwo("第二层亮度", Range(0, 10)) = 1.0
        _MatcapMultiThree("第三层亮度", Range(0, 10)) = 1.0
        
        //-------------------------------------------------
        //               EdgeLight Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_EdgeLight("边缘光设置", Float) = 1
        [Toggle]_IsEdgeLight ("边缘光开关", Float) = 0.0
        [HDR][StyledAlignedLeft][StyledField]_EdgeColor ("边缘光颜色", Color) = (1, 0, 0, 1)
        _EdgeRange ("边缘光范围", Range(0,1)) = 0.95
        _EdgeTransparent ("边缘光通透度", Range(0, 1)) = 0.9
        _EdgeHardness("边缘光硬度", Range(0.01, 1)) = 0.98

        //-------------------------------------------------
        //               Laser Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Laser("镭射设置", Float) = 1
        _Laser_Map ("镭射贴图", 2D) = "white" { }
        _LaserRange ("镭射范围", Range(0,1)) = 0.95
        _LaserHardness("镭射硬度", Range(0.01, 1)) = 0.98
        _LaserSaturation ("镭射Saturation", Range(0, 2)) = 1

        //-------------------------------------------------
        //               Advanced Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Advanced("高级设置", Float) = 1

        //-------------------------------------------------
        //               ObsoleteProperties 
        //-------------------------------------------------
        [HideInInspector] _MainTex("BaseMap", 2D) = "white" {}
    }

    SubShader
    {
        Tags {"RenderType" = "Opaque" "IgnoreProjector" = "True" "RenderPipeline" = "UniversalPipeline" }

        Blend[_SrcBlend][_DstBlend]
        ZWrite[_ZWrite]
        ZTest [_ZTest]
        Cull [_Cull]
     
        // ForwardLit
        Pass
        {
            Name "BaseColor"

            HLSLPROGRAM
            #pragma target 3.5

            #pragma shader_feature_local_fragment _ALPHATEST_ON
            #pragma multi_compile _ _DRAKSCREEN_ON

            #pragma vertex Vertex
            #pragma fragment Fragment

            
            #pragma shader_feature_local _EMISSION
            #pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma shader_feature_local _METALLICGLOSSMAP

            #pragma multi_compile _ _LIGHT_DEBUG _COLOR_DEBUG
            #pragma multi_compile _ _GRAY_DEBUG

            
            #pragma shader_feature_local _RECEIVE_SHADOWS_OFF
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            #pragma shader_feature_local _NORMALMAP

            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            #pragma shader_feature_local _MATCAP

            //#define _REFLECTION_CUBE_OFF

            #include "Actor-Lit-Input.hlsl"
            #include "Actor-Lit-Pass.hlsl"

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
            #pragma shader_feature_local_fragment _DEADDISSOLUTION_ON

            #include "Actor-Lit-Input.hlsl"
            #include "Actor-Lit-Outline.hlsl"
            
            #pragma vertex vert
            #pragma fragment frag

            ENDHLSL
        }

        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ZTest LEqual
            ColorMask 0
            Cull[_Cull]

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex ShadowPassVertex
            #pragma fragment ShadowPassFragment

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local _ALPHATEST_ON
            #pragma shader_feature_local_fragment _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A


            // -------------------------------------
            // Universal Pipeline keywords

            // -------------------------------------
            // Unity defined keywords
            #pragma multi_compile_fragment _ LOD_FADE_CROSSFADE

            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            // -------------------------------------
            // Includes
            #include "Actor-Lit-Input.hlsl"
            #include "../ShaderLibrary/Lighting/ShadowCasterPass.hlsl"


            ENDHLSL
        }


    }
    
    
    CustomEditor "TA_Tools.StyledEditor.StyledMaterial.MaterialCoreGUI"
}
