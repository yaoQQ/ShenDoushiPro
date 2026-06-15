using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using static YLib.PostProcess.EffectPostCtrl;

namespace CustomTimeLine
{
    [DisplayName("相机旋转")]
    public class CameraRotateAsset : PlayableAsset
    {

        [LabelText("旋转中心")]
        public Vector2 rotatePivot = Vector2.zero;

        [LabelText("旋转角度")]
        [Range(-360, 360)]
        public float rotateAngle = 0;

        [LabelText("旋转曲线")]
        public AnimationCurve rotateCurve = new AnimationCurve();

        [LabelText("深度偏移")]
        public float depthOffset = 13; // 深度偏移，用于计算旋转中心的深度 //13 是魔法数，斗罗相机近地面处


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<CameraRotateBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性

            behaviour.rotatePivot = rotatePivot;
            behaviour.rotateAngle = rotateAngle;
            behaviour.rotateCurve = rotateCurve;
            behaviour.depthOffset = depthOffset;

            return playable;
        }
    }
}
