using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using YLib.PostProcess;
using static YLib.PostProcess.EffectPostCtrl;


namespace CustomTimeLine
{
    public class CameraRotateBehaviour : PlayableBehaviour
    {

        [LabelText("旋转中心")]
        public Vector2 rotatePivot = Vector2.zero;

        [LabelText("旋转角度")]
        [Range(-360, 360)]
        public float rotateAngle = 0;

        [LabelText("旋转曲线")]
        public AnimationCurve rotateCurve = new AnimationCurve();

        [LabelText("深度偏移")]
        public float depthOffset = 13; // 深度偏移，用于计算旋转中心的深度


        private bool isRecord = false;
        private Vector3 mStartPos_rotateGo = Vector3.zero;
        private Vector3 mStartPos_moveGo = Vector3.zero;
        private Quaternion mStartRotate = new Quaternion();
        private Camera effectCamara = null;
        private Transform moveGo = null;
        private Transform rotateGo = null;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            effectCamara = playerData as Camera;
            if (effectCamara != null)
            {
                moveGo = effectCamara.transform.parent;
                if (moveGo == null)
                {
                    Debug.LogError("Camera's parent is null, please check the camera setup.");
                    return;
                }
                rotateGo = moveGo.transform.parent;
                if (rotateGo == null)
                {
                    Debug.LogError("Camera's grandparent is null, please check the camera setup.");
                    return;
                }

                if (!isRecord)
                {
                    // 记录初始位置
                    mStartPos_rotateGo = rotateGo.transform.localPosition;
                    mStartRotate = rotateGo.transform.localRotation;
                    isRecord = true;

                    //修改锚点
                    mStartPos_moveGo = moveGo.transform.position;
                    Vector3 rotateGoWorldPos = effectCamara.ScreenToWorldPoint(new Vector3(rotatePivot.x * effectCamara.pixelWidth, rotatePivot.y * effectCamara.pixelHeight, effectCamara.nearClipPlane + depthOffset));

                    rotateGo.transform.position = rotateGoWorldPos;
                    moveGo.transform.position = mStartPos_moveGo;

                }

                // 获取当前Playable的时间
                double currentTime = playable.GetTime();
                // 获取Playable的总时长
                double duration = playable.GetDuration();

                // 计算归一化的进度（0到1之间）
                float rate = (float)(currentTime / duration);

                var r = rotateCurve.Evaluate(rate);

                // 计算旋转角度
                Vector3 e = mStartRotate.eulerAngles;
                e.z += rotateAngle * r;
                Quaternion newRotate = Quaternion.Euler(e);

                rotateGo.transform.localRotation = newRotate;
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
                if (effectCamara != null && moveGo != null && rotateGo != null)
                {
                    // 恢复到初始位置
                    rotateGo.transform.localPosition = mStartPos_rotateGo;
                    rotateGo.transform.localRotation = mStartRotate;
                    moveGo.transform.position = mStartPos_moveGo;
                    isRecord = false;
                }
            }
        }

        public override void OnGraphStop(Playable playable)
        {
        }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (effectCamara != null && moveGo != null && rotateGo != null)
            {
                // 恢复到初始位置
                rotateGo.transform.localPosition = mStartPos_rotateGo;
                rotateGo.transform.localRotation = mStartRotate;
                moveGo.transform.position = mStartPos_moveGo;
                isRecord = false;
            }
        }

    }
}