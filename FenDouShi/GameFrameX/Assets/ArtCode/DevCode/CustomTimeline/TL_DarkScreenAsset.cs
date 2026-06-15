using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CustomTimeLine
{
    [DisplayName("暗屏")]
    public class TL_DarkScreenAsset : PlayableAsset
    {
        [LabelText("暗屏曲线")]
        public AnimationCurve DarkScreenCurve = new AnimationCurve();


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<TL_DarkScreenBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性

            behaviour.DarkScreenCurve = DarkScreenCurve;

            return playable;
        }
    }
}
