using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using static YLib.PostProcess.EffectPostCtrl;

namespace CustomTimeLine
{
    [DisplayName("相机移动")]
    public class CameraMoveAsset : PlayableAsset
    {

        [LabelText("抖动x偏移曲线")]
        public AnimationCurve VibrationX = new AnimationCurve();

        [LabelText("抖动y偏移曲线")]
        public AnimationCurve VibrationY = new AnimationCurve();

        [LabelText("抖动z偏移曲线")]
        public AnimationCurve VibrationZ = new AnimationCurve();

        [LabelText("震动幅度缩放")]
        public float Magnitude = 1;


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<CameraMoveBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性

            behaviour.VibrationX = VibrationX;
            behaviour.VibrationY = VibrationY;
            behaviour.VibrationZ = VibrationZ;
            behaviour.Magnitude = Magnitude;

            return playable;
        }
    }
}
