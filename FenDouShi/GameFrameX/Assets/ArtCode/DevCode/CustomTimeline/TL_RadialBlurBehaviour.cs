using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using YLib.PostProcess;
using static YLib.PostProcess.EffectPostCtrl;


namespace CustomTimeLine
{
    public class TL_RadialBlurBehaviour : PlayableBehaviour
    {
        [LabelText("中心初始化方式")]
        public RadialBlurInitCenterType RadialBlurCenterType;

        [LabelText("水平中心")]
        [Range(0, 1)]
        public float RadialBlurHorizontalCenter;

        [LabelText("垂直中心")]
        [Range(0, 1)]
        public float RadialBlurVerticalCenter;

        [LabelText("距离曲线")]
        public AnimationCurve RadialBlurDisCurve = new AnimationCurve();

        [LabelText("比例曲线")]
        public AnimationCurve RadialBlurStrengthCurve = new AnimationCurve();

        [LabelText("迭代次数")]
        [Range(10, 16)]
        public int RadialBlurIterTimes = 10;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            // 获取当前Playable的时间
            double currentTime = playable.GetTime();
            // 获取Playable的总时长
            double duration = playable.GetDuration();

            // 计算归一化的进度（0到1之间）
            float rate = (float)(currentTime / duration);

            RadialBlurData.RadialBlurWidth = RadialBlurDisCurve.Evaluate(rate);
            RadialBlurData.RadialBlurStrength = RadialBlurStrengthCurve.Evaluate(rate);

            if (RadialBlurCenterType == RadialBlurInitCenterType.ScreenMiddle)
            {
                RadialBlurData.RadialBlurHorizontalCenter = 0.5f;
                RadialBlurData.RadialBlurVerticalCenter = 0.5f;
            }
            else if (RadialBlurCenterType == RadialBlurInitCenterType.ScreenPos)
            {
                RadialBlurData.RadialBlurHorizontalCenter = RadialBlurHorizontalCenter;
                RadialBlurData.RadialBlurVerticalCenter = RadialBlurVerticalCenter;
            }
            else
            {
                var targetTF = playerData as Transform;
                if (targetTF != null)
                {
                    Camera effectCamara = Camera.main;
                    Vector3 screenPos = effectCamara.WorldToScreenPoint(targetTF.position);
                    RadialBlurData.RadialBlurHorizontalCenter = screenPos.x / effectCamara.scaledPixelWidth;
                    RadialBlurData.RadialBlurVerticalCenter = screenPos.y / effectCamara.scaledPixelHeight;
                }
            }

            RadialBlurData.RadialBlurIterTimes = RadialBlurIterTimes;
        }


        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            RadialBlurData.active = true;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                RadialBlurData.active = false;
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (RadialBlurData.active)
            {
                RadialBlurData.active = false;
            }
        }

    }
}