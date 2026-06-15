Shader "R2/Scene/DiMian"
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
        //               Env Settings
        //-------------------------------------------------
        [StyledCategory]_Category_Colapsable_Env ("环境设置", Float) = 1
        [Toggle(CURVEDWORLD_DISABLED_ON)]_CurvedWorldOn ("关闭曲面影响", Float) = 0.0

        //-------------------------------------------------
        //               Surface Settings 
        //------------------------------------------------- 
        [StyledCategory]_Category_Colapsable_Surface("着色设置", Float) = 1
        
        [StyledTextureSingleLine(_BaseColor)][MainTexture] _BaseMap ("颜色图", 2D) = "white" { }
        [HideInInspector][MainColor]_BaseColor ("Color", Color) = (1, 1, 1, 1)
        
        [Space(18)]
        [StyledTexST(_BaseMap)] _Temp_ST_1 ("_Temp_ST_1", Float) = 0

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
            #pragma shader_feature_local CURVEDWORLD_DISABLED_ON
            #pragma multi_compile _ _DRAKSCREEN_ON

            #pragma vertex Vertex
            #pragma fragment Fragment

            #include "DiMian-Pass.hlsl"

            ENDHLSL
        }
    }

    CustomEditor "TA_Tools.StyledEditor.StyledMaterial.MaterialCoreGUI"
}
