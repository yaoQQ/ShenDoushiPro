using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Playables;
using static YLib.PostProcess.EffectPostCtrl;

namespace CustomTimeLine
{
    [DisplayName("径向模糊")]
    public class TL_RadialBlurAsset : PlayableAsset
    {

        [LabelText("中心初始化方式")]
        public RadialBlurInitCenterType RadialBlurCenterType;

        [LabelText("水平中心")]
        [Range(0, 1)]
        public float RadialBlurHorizontalCenter = 0.5f;

        [LabelText("垂直中心")]
        [Range(0, 1)]
        public float RadialBlurVerticalCenter = 0.5f;

        [LabelText("距离曲线")]
        public AnimationCurve RadialBlurDisCurve = new AnimationCurve();

        [LabelText("比例曲线")]
        public AnimationCurve RadialBlurStrengthCurve = new AnimationCurve();

        [LabelText("迭代次数")]
        [Range(10, 16)]
        public int RadialBlurIterTimes = 10;


        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            // 创建一个Playable
            var playable = ScriptPlayable<TL_RadialBlurBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            // 在这里可以设置behaviour的属性

            behaviour.RadialBlurCenterType = RadialBlurCenterType;
            behaviour.RadialBlurHorizontalCenter = RadialBlurHorizontalCenter;
            behaviour.RadialBlurVerticalCenter = RadialBlurVerticalCenter;
            behaviour.RadialBlurDisCurve = RadialBlurDisCurve;
            behaviour.RadialBlurStrengthCurve = RadialBlurStrengthCurve;
            behaviour.RadialBlurIterTimes = RadialBlurIterTimes;

            return playable;
        }
    }
}
