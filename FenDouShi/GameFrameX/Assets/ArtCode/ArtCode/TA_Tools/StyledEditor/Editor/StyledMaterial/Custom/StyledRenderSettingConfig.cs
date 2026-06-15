using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;


namespace TA_Tools.StyledEditor.StyledMaterial
{

    [CreateAssetMenu(fileName = "StyledRenderSettingConfig", menuName = "我的资源/StyledEditor/StyledRenderSettingConfig")]
    public class StyledRenderSettingConfig : ScriptableObject
    {
        public enum State { 
            OFF = 0,
            ON = 1,
        }

        public enum RendererQueue
        {
            FromShader = -1,
            Geometry = 2000,
            AlphaTest = 2450,
            Transparent = 3000
        }


        //[EnumToggleButtons]
        public UnityEngine.Rendering.BlendMode _SrcBlend = UnityEngine.Rendering.BlendMode.SrcAlpha;

        //[EnumToggleButtons]
        public UnityEngine.Rendering.BlendMode _DstBlend = UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha;

        //[EnumToggleButtons]
        public UnityEngine.Rendering.CompareFunction _ZTest = UnityEngine.Rendering.CompareFunction.LessEqual;

        //[EnumToggleButtons]
        public UnityEngine.Rendering.CullMode _Cull = UnityEngine.Rendering.CullMode.Back;

        //[EnumToggleButtons]
        public State _ZWrite = State.ON;

        //[ToggleLeft]
        public bool _AlphaClip = false;

        // 当 _AlphaClip 为 true 时显示 Alpha Cutoff
        [ShowIf("_AlphaClip")]
        [Range(0f, 1f)]
        [LabelText("Alpha Cutoff")]
        public float _Cutoff = 0.5f;

        public RendererQueue _RenderQueue = RendererQueue.FromShader;
        public int _RenderQueueOffset = 0;

    }
}