Shader "R2/Actor/Actor-Cloth"
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
        //               NPR Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_NPR("NPR设置", Float) = 1

        [MainTexture] _BaseMap("基础贴图", 2D) = "white" {}
        _BackMap("背面贴图", 2D) = "white" {}
        [MainColor]   _BaseColor("基础颜色", Color) = (1, 1, 1, 1)
        _Saturation ("饱和度", Range(0, 2)) = 1
        
        [StyledKeywordTextureSingleLine(_METALLICGLOSSMAP)]_MetallicGlossMap("AO贴图(b:AO)", 2D) = "white" { }
        _OcclusionStrength("AO强度", Range(0, 1)) = 1.0


        _ShadowSmooth("漫反射平滑度", Range(0, 1)) = 0.0
        _ShadowRange("漫反射范围", Range(0, 1)) = 0.0
        _DiffuseColorA("亮部颜色", Color) = (0, 0, 0, 1)
        _DiffuseColorB("暗部颜色", Color) = (0, 0, 0, 1)


        //-------------------------------------------------
        //               Outline Settings 
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Outline("描边设置", Float) = 1
        _OutlineColor ("描边颜色", Color) = (0, 0, 0, 1)
		[ToggleOff] _MixMainTexToOutline ("描边混合主贴图", Float ) = 1.0
        _OutlineWidth ("描边粗细", Range(0, 10)) = 0.05
        
        _MaxOutlineZOffset("描边最大粗细", Range(0, 10)) = 0.05
        _OutlineOffset ("描边偏移", Vector) = (0, 0, 0, 0)

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

            #include "Actor-Cloth-Input.hlsl"
            #include "Actor-Cloth-Pass.hlsl"

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

            #include "Actor-Cloth-Input.hlsl"
            #include "Actor-Cloth-Outline.hlsl"
            
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

            // This is used during shadow map generation to differentiate between directional and punctual light shadows, as they use different formulas to apply Normal Bias
            #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

            // -------------------------------------
            // Includes
            // #include "Packages/com.unity.render-pipelines.universal/Shaders/LitInput.hlsl"
            // Includes
            #include "Actor-Cloth-Input.hlsl"
            #include "../ShaderLibrary/Lighting/ShadowCasterPass.hlsl"


            ENDHLSL
        }


    }
    
    
    CustomEditor "TA_Tools.StyledEditor.StyledMaterial.MaterialCoreGUI"
}
