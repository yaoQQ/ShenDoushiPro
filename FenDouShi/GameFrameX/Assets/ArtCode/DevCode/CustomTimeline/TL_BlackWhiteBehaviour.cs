using UnityEngine;
using UnityEngine.Playables;
using YLib.PostProcess;


namespace CustomTimeLine
{
    public class TL_BlackWhiteBehaviour : PlayableBehaviour
    {
        public AnimationCurve BlackWhiteThresholdCurve = new AnimationCurve();

        public AnimationCurve BlackWhiteWidthCurve = new AnimationCurve();

        public Color BlackWhiteWhiteColor = Color.white;
        public Color BlackWhiteBlackColor = Color.black;

        public AnimationCurve BlackWhiteFlipCurve = new AnimationCurve();

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // 获取当前Playable的时间
            double currentTime = playable.GetTime();
            // 获取Playable的总时长
            double duration = playable.GetDuration();

            // 计算归一化的进度（0到1之间）
            float rate = (float)(currentTime / duration);

            BlackWhiteData.BlackWhiteThreshold = BlackWhiteThresholdCurve.Evaluate(rate);
            BlackWhiteData.BlackWhiteWidth = BlackWhiteWidthCurve.Evaluate(rate);
            BlackWhiteData.BlackWhiteWhiteColor = BlackWhiteWhiteColor;
            BlackWhiteData.BlackWhiteBlackColor = BlackWhiteBlackColor;
            BlackWhiteData.BlackWhiteFlip = BlackWhiteFlipCurve.Evaluate(rate) > 0;
        }


        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            BlackWhiteData.active = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                BlackWhiteData.active = false;
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (BlackWhiteData.active)
                BlackWhiteData.active = false;
        }

    }
}