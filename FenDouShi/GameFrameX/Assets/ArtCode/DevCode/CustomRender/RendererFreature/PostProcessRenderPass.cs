using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using YLib.CustomRender;
using YLib.PostProcess;

namespace YLib.PostProcess
{
    public class PostProcessRenderPass : ScriptableRenderPass
    {
        private static readonly ProfilingSampler m_ProfilingRenderFinalPostProcessing = new ProfilingSampler("CustomPostProcess");
        public Shader uber_shader = null;
        public Material uber_mat = null;


        public PostProcessRenderPass(RenderPassEvent evt,Shader uber)
        {
            renderPassEvent = evt;

            uber_shader = uber;
            if (uber_mat == null && uber_shader != null)
            {
                uber_mat = new Material(uber_shader);
            }

        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            
        }

        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
        }


        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cameraData = renderingData.cameraData;
            if (cameraData.isPreviewCamera) return;

            var cmd = CommandBufferPool.Get();
            using (new ProfilingScope(cmd, m_ProfilingRenderFinalPostProcessing))
            {
                Render(cmd, ref renderingData);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        private void Render(CommandBuffer cmd, ref RenderingData renderingData)
        {
            //if (uber_shader == null || !Application.isPlaying || (!BlackWhiteData.active && !RadialBlurData.active && !ColorDispersionData.active))
            if (uber_shader == null || (!BlackWhiteData.active && !RadialBlurData.active && !ColorDispersionData.active))
                return;

            if (uber_mat == null && uber_shader != null)
            {
                uber_mat = new Material(uber_shader);
            }

            if (BlackWhiteData.active)
            {
                uber_mat.SetFloat(ShaderConstants._BlackWhiteThreshold, BlackWhiteData.BlackWhiteThreshold);
                uber_mat.SetFloat(ShaderConstants._BlackWhiteWidth, BlackWhiteData.BlackWhiteWidth);
                uber_mat.SetColor(ShaderConstants._BlackWhiteWhiteColor, BlackWhiteData.BlackWhiteWhiteColor);
                uber_mat.SetColor(ShaderConstants._BlackWhiteBlackColor, BlackWhiteData.BlackWhiteBlackColor);
                uber_mat.SetFloat(ShaderConstants._BlackWhiteFlip, BlackWhiteData.BlackWhiteFlip ? 1f : 0f);
            }

            if (RadialBlurData.active)
            {
                uber_mat.SetFloat(ShaderConstants._RadialBlurHorizontalCenter, RadialBlurData.RadialBlurHorizontalCenter);
                uber_mat.SetFloat(ShaderConstants._RadialBlurVerticalCenter, RadialBlurData.RadialBlurVerticalCenter);
                uber_mat.SetFloat(ShaderConstants._RadialBlurWidth, RadialBlurData.RadialBlurWidth);
                uber_mat.SetFloat(ShaderConstants._RadialBlurStrength, RadialBlurData.RadialBlurStrength);
                uber_mat.SetInt(ShaderConstants._RadialBlurIterTimes, RadialBlurData.RadialBlurIterTimes);
            }

            if (ColorDispersionData.active)
            {
                uber_mat.SetFloat(ShaderConstants._ColorDispersionU, ColorDispersionData.ColorDispersionU);
                uber_mat.SetFloat(ShaderConstants._ColorDispersionV, ColorDispersionData.ColorDispersionV);
                uber_mat.SetFloat(ShaderConstants._ColorDispersionStrength, ColorDispersionData.ColorDispersionStrength);
            }

            CoreUtils.SetKeyword(uber_mat, "_BLACKWHITE", BlackWhiteData.active);
            CoreUtils.SetKeyword(uber_mat, "_RADIALBLUR", RadialBlurData.active);
            CoreUtils.SetKeyword(uber_mat, "_COLORDISPERSION", ColorDispersionData.active);

            ref CameraData cameraData = ref renderingData.cameraData;
            ref ScriptableRenderer renderer = ref cameraData.renderer;

            RTHandle source = renderer.cameraColorTargetHandle;
            renderer.EnableSwapBufferMSAA(true);
            RTHandle destination = renderer.GetCameraColorFrontBuffer(cmd);

            cmd.SetGlobalMatrix(ShaderConstants._FullscreenProjMat, GL.GetGPUProjectionMatrix(Matrix4x4.identity, true));

            Blitter.BlitCameraTexture(cmd, source, destination, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store, uber_mat, 0);

            renderer.SwapColorBuffer(cmd);

        }

        public override void OnCameraCleanup(CommandBuffer cmd)
        {

        }

    }

}