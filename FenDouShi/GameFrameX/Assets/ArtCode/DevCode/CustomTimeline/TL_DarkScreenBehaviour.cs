using UnityEngine;
using UnityEngine.Playables;
using YLib.CustomRender;


namespace CustomTimeLine
{
    public class TL_DarkScreenBehaviour : PlayableBehaviour
    {
        public AnimationCurve DarkScreenCurve = new AnimationCurve();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // 获取当前Playable的时间
            double currentTime = playable.GetTime();
            // 获取Playable的总时长
            double duration = playable.GetDuration();

            // 计算归一化的进度（0到1之间）
            float progress = (float)(currentTime / duration);

            // 使用曲线计算当前值
            float curveValue = DarkScreenCurve.Evaluate(progress);

            Shader.SetGlobalFloat(ShaderConstants._DrakScreenStrength, curveValue);
        }

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            SetDrakScreen(true);
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                SetDrakScreen(false);
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            SetDrakScreen(false);
        }

        public void SetDrakScreen(bool isEnabled)
        {
            if (isEnabled != Shader.IsKeywordEnabled(ShaderConstants.KW_DRAKSCREEN_ON))
            {
                Shader.SetKeyword(ShaderConstants.KW_DRAKSCREEN_ON, isEnabled);
            }
        }

    }
}