using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CustomTimeLine
{
    [DisplayName("色散")]
    public class TL_ColorDispersionAsset : PlayableAsset
    {
        [LabelText("色散比例曲线")]
        public AnimationCurve ColorDispersionCurve = new AnimationCurve();

        [LabelText("U偏移曲线")]
        public AnimationCurve ColorDispersionUCurve = new AnimationCurve();

        [LabelText("V偏移曲线")]
        public AnimationCurve ColorDispersionVCurve = new AnimationCurve();


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<TL_ColorDispersionBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性


            behaviour.ColorDispersionCurve = ColorDispersionCurve;
            behaviour.ColorDispersionUCurve = ColorDispersionUCurve;
            behaviour.ColorDispersionVCurve = ColorDispersionVCurve;
   


            return playable;
        }
    }
}
