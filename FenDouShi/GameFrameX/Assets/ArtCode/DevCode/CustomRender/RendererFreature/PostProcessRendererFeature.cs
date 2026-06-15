using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Experimental.Rendering.Universal.RenderObjects;


namespace YLib.PostProcess
{
    public class PostProcessRendererFreature : ScriptableRendererFeature
    {
        [System.Serializable]
        public class Settings
        {
            public Shader uber = null;
        }

        public Settings settings = new Settings();

        public PostProcessRenderPass renderPass = null;
        public override void Create()
        {
            renderPass = new PostProcessRenderPass(RenderPassEvent.BeforeRenderingPostProcessing,settings.uber);
        }
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            renderer.EnqueuePass(renderPass);
        }
    }

}
