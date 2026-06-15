using UnityEngine;
using UnityEngine.Playables;
using YLib.PostProcess;
using static YLib.PostProcess.EffectPostCtrl;


namespace CustomTimeLine
{
    public class TL_ColorDispersionBehaviour : PlayableBehaviour
    {
        public AnimationCurve ColorDispersionCurve = new AnimationCurve();
        public AnimationCurve ColorDispersionUCurve = new AnimationCurve();
        public AnimationCurve ColorDispersionVCurve = new AnimationCurve();


        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // 获取当前Playable的时间
            double currentTime = playable.GetTime();
            // 获取Playable的总时长
            double duration = playable.GetDuration();

            // 计算归一化的进度（0到1之间）
            float rate = (float)(currentTime / duration);

            ColorDispersionData.ColorDispersionStrength = ColorDispersionCurve.Evaluate(rate);
            ColorDispersionData.ColorDispersionU = ColorDispersionUCurve.Evaluate(rate);
            ColorDispersionData.ColorDispersionV = ColorDispersionVCurve.Evaluate(rate);
        }


        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            ColorDispersionData.active = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                ColorDispersionData.active = false;
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (ColorDispersionData.active)
            {
                ColorDispersionData.active = false;
            }
        }

    }
}