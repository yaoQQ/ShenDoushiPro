using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace YLib.CustomRender
{
    public class ShaderConstants
    {

        //暗屏
        public static readonly GlobalKeyword KW_DRAKSCREEN_ON = GlobalKeyword.Create("_DRAKSCREEN_ON");
        public static readonly int _DrakScreenStrength = Shader.PropertyToID("_DrakScreenStrength");


        //后处理
        public static readonly int _BlackWhiteThreshold = Shader.PropertyToID("_BlackWhiteThreshold");
        public static readonly int _BlackWhiteWidth = Shader.PropertyToID("_BlackWhiteWidth");
        public static readonly int _BlackWhiteWhiteColor = Shader.PropertyToID("_BlackWhiteWhiteColor");
        public static readonly int _BlackWhiteBlackColor = Shader.PropertyToID("_BlackWhiteBlackColor");
        public static readonly int _BlackWhiteFlip = Shader.PropertyToID("_BlackWhiteFlip");

        public static readonly int _RadialBlurHorizontalCenter = Shader.PropertyToID("_RadialBlurHorizontalCenter");
        public static readonly int _RadialBlurVerticalCenter = Shader.PropertyToID("_RadialBlurVerticalCenter");
        public static readonly int _RadialBlurWidth = Shader.PropertyToID("_RadialBlurWidth");
        public static readonly int _RadialBlurStrength = Shader.PropertyToID("_RadialBlurStrength");
        public static readonly int _RadialBlurIterTimes = Shader.PropertyToID("_RadialBlurIterTimes");

        public static readonly int _ColorDispersionU = Shader.PropertyToID("_ColorDispersionU");
        public static readonly int _ColorDispersionV = Shader.PropertyToID("_ColorDispersionV");
        public static readonly int _ColorDispersionStrength = Shader.PropertyToID("_ColorDispersionStrength");

        public static readonly int _FullscreenProjMat = Shader.PropertyToID("_FullscreenProjMat");
    }

}
