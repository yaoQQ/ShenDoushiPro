Shader "R2/PostProcess/UberPost"
{
    Properties
    {
        [MainTexture]_BlitTexture ("Source Texture", 2D) = "white" { }

        [Main(_ColorDispersionGroup, _COLORDISPERSION)]_ColorDispersion ("色散", Float) = 0
        [Sub(_ColorDispersionGroup)] _ColorDispersionU ("色散U偏移", Range(-1, 1)) = 0
        [Sub(_ColorDispersionGroup)] _ColorDispersionV ("色散V偏移", Range(-1, 1)) = 0
        [Sub(_ColorDispersionGroup)] _ColorDispersionStrength ("色散比例", Range(0, 0.2)) = 0

        [Main(_BlackWhiteGroup, _BLACKWHITE)]_BlackWhite ("黑白屏", Float) = 0
        [Sub(_BlackWhiteGroup)] _BlackWhiteThreshold ("黑白屏阈值", Range(0, 1)) = 0.5
        [Sub(_BlackWhiteGroup)] _BlackWhiteWidth ("黑白屏过渡（数值越小越卡通）", Range(0, 0.1)) = 0.001
        [Sub(_BlackWhiteGroup)] _BlackWhiteWhiteColor ("白色区域着色", Color) = (1, 1, 1, 0)
        [Sub(_BlackWhiteGroup)] _BlackWhiteBlackColor ("黑色区域着色", Color) = (0, 0, 0, 0)
        [SubToggle(_BlackWhiteGroup, _)] _BlackWhiteFlip ("翻转黑白效果", Float) = 0

        [Main(_RadialBlurGroup, _RADIALBLUR)]_RadialBlur ("径向模糊", Float) = 0
        [Sub(_RadialBlurGroup)] _RadialBlurHorizontalCenter ("径向模糊水平中心", Range(0, 1)) = 0.5
        [Sub(_RadialBlurGroup)] _RadialBlurVerticalCenter ("径向模糊垂直中心", Range(0, 1)) = 0.5
        [Sub(_RadialBlurGroup)] _RadialBlurWidth ("径向模糊距离", Range(0, 1)) = 0.2
        [Sub(_RadialBlurGroup)] _RadialBlurStrength ("径向模糊比例", Range(0, 5)) = 1
        [Sub(_RadialBlurGroup)] _RadialBlurIterTimes ("径向模糊迭代次数", Range(10, 16)) = 10
    }


    SubShader
    {
        Tags { "IgnoreProjector" = "True" "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline" }

        Pass
        {
            Cull Back
            ZWrite Off
            ZTest Off
            Lighting Off
            Blend Off

            Tags { "LightMode" = "UberPost" }
            
            HLSLPROGRAM
            
            // Required to compile gles 2.0 with standard SRP library
            // All shaders must be compiled with HLSLcc and currently only gles is not using HLSLcc by default
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            // -------------------------------------
            // Material Keywords
            #pragma multi_compile_local_fragment _  _COLORDISPERSION
            #pragma multi_compile_local_fragment _  _BLACKWHITE
            #pragma multi_compile_local_fragment _  _RADIALBLUR
            
            #include "UberPost.hlsl"
            
            #pragma vertex UberEffectPostVert
            #pragma fragment UberEffectPostFrag

            ENDHLSL
        }
    }
}