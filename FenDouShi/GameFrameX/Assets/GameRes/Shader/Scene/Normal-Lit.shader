Shader "R2/Scene/Normal-Lit"
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

        [StyledKeywordTextureSingleLine(_METALLICGLOSSMAP)]_MetallicGlossMap("金属光泽贴图", 2D) = "white" { }
        _Saturation ("饱和度", Range(0, 2)) = 1
        _Metallic("金属度", Range(0, 1)) = 0.0
        _Smoothness("光泽度", Range(0, 1)) = 0.5
        _MetallicOffset("金属度偏移", Range(-1, 1)) = 0.0
        _SmoothnessOffset("光泽度偏移", Range(-1, 1)) = 0.0

        [StyledKeywordTextureSingleLine(_NORMALMAP, _BumpScale)]_BumpMap("法线贴图", 2D) = "bump" {}
        [HideInInspector]_BumpScale("法线强度", Range(0, 10)) = 1.0
        _OcclusionStrength("遮挡强度", Range(0, 1)) = 1.0
        
        // [Toggle(_REFLECTION_CUBE_ON)]_ReflectionCube("反射贴图", Float) = 0.0
        // [StyledIndentLevelAdd]
        // [StyledIfShow(_ReflectionCube)][StyledField]_ComRefCubeMip("反射Mip", Range(0, 5)) = 0.0
        // [StyledIndentLevelSub]

        [Space(18)]
        [StyledKeywordTextureSingleLine(_EMISSION, _EmissionColor)]_EmissionMap ("自发光贴图", 2D) = "white" { }
        [HideInInspector][HDR] _EmissionColor ("Color", Color) = (0, 0, 0)

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
            Name "ForwardLit"
            Tags{"LightMode" = "UniversalForward"}

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

            #if defined(LIGHTMAP_ON)
                #define USE_CUSTOM_LIGHT
            #endif

            //#define _REFLECTION_CUBE_OFF

            #include "Normal-Lit-Input.hlsl"
            #include "Normal-Lit-Pass.hlsl"

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
            #include "Normal-Lit-Input.hlsl"
            #include "../ShaderLibrary/Lighting/ShadowCasterPass.hlsl" 


            ENDHLSL
        }


        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }

            // -------------------------------------
            // Render State Commands
            Cull Off

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex UniversalVertexMeta
            #pragma fragment UniversalFragmentMetaSimple

            // -------------------------------------
            // Material Keywords
            #pragma shader_feature_local_fragment _EMISSION
            #pragma shader_feature EDITOR_VISUALIZATION

            

            
            #pragma multi_compile _ _LIGHT_LAYERS
            #pragma multi_compile _ _FORWARD_PLUS

            // -------------------------------------
            // Includes
            #include "Normal-Lit-Input.hlsl"
            #include "Normal-Lit-MetaPass.hlsl"

            ENDHLSL
        }
    }
    
    
    CustomEditor "TA_Tools.StyledEditor.StyledMaterial.MaterialCoreGUI"
}
