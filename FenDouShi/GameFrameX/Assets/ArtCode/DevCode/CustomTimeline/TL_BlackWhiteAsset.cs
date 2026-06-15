using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;

namespace CustomTimeLine
{
    [DisplayName("黑白屏")]
    public class TL_BlackWhiteAsset : PlayableAsset
    {
        [LabelText("阈值曲线")]
        public AnimationCurve BlackWhiteThresholdCurve = new AnimationCurve();

        [LabelText("过渡范围曲线")]
        public AnimationCurve BlackWhiteWidthCurve = new AnimationCurve();

        [LabelText("白色区域颜色")]
        public Color BlackWhiteWhiteColor = Color.white;

        [LabelText("黑色区域颜色")]
        public Color BlackWhiteBlackColor = Color.black;

        [LabelText("翻转曲线")]
        public AnimationCurve BlackWhiteFlipCurve = new AnimationCurve();


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<TL_BlackWhiteBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性


            behaviour.BlackWhiteThresholdCurve = BlackWhiteThresholdCurve;
            behaviour.BlackWhiteWidthCurve = BlackWhiteWidthCurve;
            behaviour.BlackWhiteWhiteColor = BlackWhiteWhiteColor;
            behaviour.BlackWhiteBlackColor = BlackWhiteBlackColor;
            behaviour.BlackWhiteFlipCurve = BlackWhiteFlipCurve;


            return playable;
        }
    }
}
