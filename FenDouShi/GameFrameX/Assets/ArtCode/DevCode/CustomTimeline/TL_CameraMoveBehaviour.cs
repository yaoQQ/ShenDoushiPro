using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using YLib.PostProcess;
using static YLib.PostProcess.EffectPostCtrl;


namespace CustomTimeLine
{
    public class CameraMoveBehaviour : PlayableBehaviour
    {

        [LabelText("抖动x偏移曲线")]
        public AnimationCurve VibrationX = new AnimationCurve();

        [LabelText("抖动y偏移曲线")]
        public AnimationCurve VibrationY = new AnimationCurve();

        [LabelText("抖动z偏移曲线")]
        public AnimationCurve VibrationZ = new AnimationCurve();

        [LabelText("震动幅度缩放")]
        public float Magnitude = 1;

        private bool isRecord = false;
        private Vector3 mVibrationStartPos = Vector3.zero;
        private Camera effectCamara = null;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            effectCamara = playerData as Camera;
            if (effectCamara != null)
            {
                if(!isRecord)
                {
                    // 记录初始位置
                    mVibrationStartPos = effectCamara.transform.localPosition;
                    isRecord = true;
                }

                // 获取当前Playable的时间
                double currentTime = playable.GetTime();
                // 获取Playable的总时长
                double duration = playable.GetDuration();

                // 计算归一化的进度（0到1之间）
                float rate = (float)(currentTime / duration);

                var x = VibrationX.Evaluate(rate);
                var y = VibrationY.Evaluate(rate);
                var z = VibrationZ.Evaluate(rate);

                Vector3 newPos = Vector3.zero;
                newPos = mVibrationStartPos + new Vector3(
                    x * Magnitude,
                    y * Magnitude,
                    z * Magnitude);
                effectCamara.transform.localPosition = newPos;
            }
        }


        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            isRecord = false;
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            if (info.effectivePlayState == PlayState.Paused)
            {
                if (effectCamara != null)
                {
                    // 恢复到初始位置
                    effectCamara.transform.localPosition = mVibrationStartPos;
                    isRecord = false;
                }
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (effectCamara != null)
            {
                // 恢复到初始位置
                effectCamara.transform.localPosition = mVibrationStartPos;
                isRecord = false;
            }
        }

    }
}