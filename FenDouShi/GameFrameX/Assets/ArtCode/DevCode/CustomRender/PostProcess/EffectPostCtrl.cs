using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using YLib.CustomRender;
using YLib.PostProcess;

namespace YLib.PostProcess
{
    [ExecuteInEditMode]
    public class EffectPostCtrl : MonoBehaviour
    {
        #region 暗屏
        [Serializable]
        public class DarkScreenParameters
        {
            [LabelText("暗屏曲线")]
            public AnimationCurve DarkScreenCurve;
        }

        [FoldoutGroup("暗屏")]
        [LabelText("暗屏")]
        public bool EnableDarkScreen;

        [EnableIf("EnableDarkScreen"), FoldoutGroup("暗屏")]
        [LabelText("开始时间")]
        public float DarkScreenStartTime = 0;

        [EnableIf("EnableDarkScreen"), FoldoutGroup("暗屏")]
        [LabelText("结束时间")]
        [MinValue("DarkScreenStartTime")]
        public float DarkScreenEndTime = 3;

        [EnableIf("EnableDarkScreen"), FoldoutGroup("暗屏")]
        [HideLabel]
        public DarkScreenParameters mDarkScreenParameters;
        #endregion

        #region 黑白屏
        [Serializable]
        public class BlackWhiteParameters
        {
            [LabelText("阈值曲线")]
            public AnimationCurve BlackWhiteThresholdCurve;

            [LabelText("过渡范围曲线")]
            public AnimationCurve BlackWhiteWidthCurve;

            [LabelText("白色区域颜色")]
            public Color BlackWhiteWhiteColor = Color.white;

            [LabelText("黑色区域颜色")]
            public Color BlackWhiteBlackColor = Color.black;

            [LabelText("翻转曲线")]
            public AnimationCurve BlackWhiteFlipCurve;
        }

        [FoldoutGroup("黑白屏")]
        [LabelText("黑白屏")]
        public bool EnableBlackWhite;

        [EnableIf("EnableBlackWhite"), FoldoutGroup("黑白屏")]
        [LabelText("开始时间")]
        public float BlackWhiteStartTime = 0;

        [EnableIf("EnableBlackWhite"), FoldoutGroup("黑白屏")]
        [LabelText("结束时间")]
        [MinValue("BlackWhiteStartTime")]
        public float BlackWhiteEndTime = 3;

        [EnableIf("EnableBlackWhite"), FoldoutGroup("黑白屏")]
        [HideLabel]
        public BlackWhiteParameters mBlackWhiteParameters;
        #endregion

        #region 径向模糊
        public enum RadialBlurInitCenterType
        {
            [LabelText("自定义")]
            ScreenCustom,
            [LabelText("屏幕坐标")]
            ScreenPos,
            [LabelText("屏幕中心")]
            ScreenMiddle,
        }

        [Serializable]
        public class RadialBlurParameters
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
            public AnimationCurve RadialBlurDisCurve;

            [LabelText("比例曲线")]
            public AnimationCurve RadialBlurStrengthCurve;

            [LabelText("迭代次数")]
            [Range(10, 16)]
            public int RadialBlurIterTimes = 10;
        }

        [FoldoutGroup("径向模糊")]
        [LabelText("径向模糊")]
        public bool EnableRadialBlur;

        [EnableIf("EnableRadialBlur"), FoldoutGroup("径向模糊")]
        [LabelText("开始时间")]
        public float RadialBlurStartTime = 0;

        [EnableIf("EnableRadialBlur"), FoldoutGroup("径向模糊")]
        [LabelText("结束时间")]
        [MinValue("RadialBlurStartTime")]
        public float RadialBlurEndTime = 3;

        [EnableIf("EnableRadialBlur"), FoldoutGroup("径向模糊")]
        [HideLabel]
        public RadialBlurParameters mRadialBlurParameters;
        #endregion

        #region 色散
        [Serializable]
        public class ColorDispersionParameters
        {
            [LabelText("色散比例曲线")]
            public AnimationCurve ColorDispersionCurve;

            [LabelText("U偏移曲线")]
            public AnimationCurve ColorDispersionUCurve;

            [LabelText("V偏移曲线")]
            public AnimationCurve ColorDispersionVCurve;
        }

        [FoldoutGroup("色散")]
        [LabelText("色散")]
        public bool EnableColorDispersion;

        [EnableIf("EnableColorDispersion"), FoldoutGroup("色散")]
        [LabelText("开始时间")]
        public float ColorDispersionStartTime = 0;

        [EnableIf("EnableColorDispersion"), FoldoutGroup("色散")]

        [LabelText("结束时间")]
        [MinValue("ColorDispersionStartTime")]
        public float ColorDispersionEndTime = 3;

        [EnableIf("EnableColorDispersion"), FoldoutGroup("色散")]
        [HideLabel]
        public ColorDispersionParameters mColorDispersionParameters;
        #endregion

        #region 相机抖动
        [Serializable]
        public class VibrationParameters
        {
            [LabelText("抖动x偏移曲线")]
            public AnimationCurve VibrationX;

            [LabelText("抖动y偏移曲线")]
            public AnimationCurve VibrationY;

            [LabelText("抖动z偏移曲线")]
            public AnimationCurve VibrationZ;

            [LabelText("震动幅度缩放")]
            public float Magnitude = 1;
        }

        [FoldoutGroup("相机抖动")]
        [LabelText("相机抖动")]
        public bool EnableVibration;

        [EnableIf("EnableVibration"), FoldoutGroup("相机抖动")]
        [LabelText("开始时间")]
        public float VibrationStartTime = 0;

        [EnableIf("EnableVibration"), FoldoutGroup("相机抖动")]
        [LabelText("结束时间")]
        [MinValue("VibrationStartTime")]
        public float VibrationEndTime = 3;

        [HideInInspector]
        public Vector3 mVibrationStartPos = Vector3.zero;

        [EnableIf("EnableVibration"), FoldoutGroup("相机抖动")]
        [HideLabel]
        public VibrationParameters mVibrationParameters;
        #endregion

        #region 慢动作
        [Serializable]
        public class SlowMotionParameters
        {
            [LabelText("速度")]
            public AnimationCurve Speed;

            [LabelText("缩放")]
            public float Scale = 1;
        }

        [FoldoutGroup("慢动作")]
        [LabelText("慢动作")]
        public bool EnableSlowMotion;

        [EnableIf("EnableSlowMotion"), FoldoutGroup("慢动作")]
        [LabelText("开始时间")]
        public float SlowMotionStartTime = 0;

        [EnableIf("EnableSlowMotion"), FoldoutGroup("慢动作")]
        [LabelText("结束时间")]
        [MinValue("VibrationStartTime")]
        public float SlowMotionEndTime = 3;

        [HideInInspector]
        public bool mSlowMotionState = false;

        [EnableIf("EnableSlowMotion"), FoldoutGroup("慢动作")]
        [HideLabel]
        public SlowMotionParameters mSlowMotionParameters;
        #endregion

        private float mCurrentTime = 0.0f;
        private float mCurrentFixedTime = 0.0f;

        private Camera mCamera = null;


        private void Start()
        {
            InitData();
        }

        private void OnEnable()
        {
            InitData();
        }

        private void OnDestroy()
        {
            ClearData();
        }

        private void Update()
        {
            if (Application.isPlaying)
            {
                mCurrentTime += Time.deltaTime;
                mCurrentFixedTime += Time.fixedDeltaTime;

                UpdateDrakScreen(mCurrentTime);
                UpdateBlackWhite(mCurrentTime);
                UpdateRadialBlur(mCurrentTime);
                UpdateColorDispersion(mCurrentTime);
                UpdateVibration(mCurrentTime);
                UpdateSlowMotion(mCurrentFixedTime);
            }
        }

        private void InitData()
        {
            mCurrentTime = 0.0f;
            mCurrentFixedTime = 0.0f;
            mVibrationStartPos = GetCamera().transform.position;
            mSlowMotionState = false;
        }

        private void ClearData()
        {
            Shader.SetKeyword(ShaderConstants.KW_DRAKSCREEN_ON, false);
        }

        private Camera GetCamera()
        {
            if (mCamera != null)
            {
                return mCamera;
            }

            mCamera = Camera.main;

            return mCamera;
        }

        #region Updateunction
        private void UpdateDrakScreen(float currentTime)
        {
            bool isEnabled = true;
            if (!EnableDarkScreen || currentTime < DarkScreenStartTime || currentTime > DarkScreenEndTime)
            {
                isEnabled = false;
            }

            if (isEnabled != Shader.IsKeywordEnabled(ShaderConstants.KW_DRAKSCREEN_ON))
            {
                Shader.SetKeyword(ShaderConstants.KW_DRAKSCREEN_ON, isEnabled);
            }

            if (!isEnabled) return;

            float rate = (currentTime - DarkScreenStartTime) / (DarkScreenEndTime - DarkScreenStartTime);
            float strength = mDarkScreenParameters.DarkScreenCurve.Evaluate(rate);

            Shader.SetGlobalFloat(ShaderConstants._DrakScreenStrength, strength);
        }


        private void UpdateBlackWhite(float currentTime)
        {
            if (!EnableBlackWhite || currentTime < BlackWhiteStartTime || currentTime > BlackWhiteEndTime)
            {
                BlackWhiteData.active = false;
                return;
            }

            float rate = (currentTime - BlackWhiteStartTime) / (BlackWhiteEndTime - BlackWhiteStartTime);
            BlackWhiteData.active = true;
            BlackWhiteData.BlackWhiteThreshold = mBlackWhiteParameters.BlackWhiteThresholdCurve.Evaluate(rate);
            BlackWhiteData.BlackWhiteWidth = mBlackWhiteParameters.BlackWhiteWidthCurve.Evaluate(rate);
            BlackWhiteData.BlackWhiteWhiteColor = mBlackWhiteParameters.BlackWhiteWhiteColor;
            BlackWhiteData.BlackWhiteBlackColor = mBlackWhiteParameters.BlackWhiteBlackColor;
            BlackWhiteData.BlackWhiteFlip = mBlackWhiteParameters.BlackWhiteFlipCurve.Evaluate(rate) > 0;
        }

        private void UpdateRadialBlur(float currentTime)
        {
            if (!EnableRadialBlur || currentTime < RadialBlurStartTime || currentTime > RadialBlurEndTime)
            {
                RadialBlurData.active = false;
                return;
            }

            float rate = (currentTime - RadialBlurStartTime) / (RadialBlurEndTime - RadialBlurStartTime);
            RadialBlurData.active = true;
            UpdateRadialBlurCenter();
            RadialBlurData.RadialBlurWidth = mRadialBlurParameters.RadialBlurDisCurve.Evaluate(rate);
            RadialBlurData.RadialBlurStrength = mRadialBlurParameters.RadialBlurStrengthCurve.Evaluate(rate);
            RadialBlurData.RadialBlurIterTimes = mRadialBlurParameters.RadialBlurIterTimes;
        }

        public void UpdateRadialBlurCenter()
        {
            if (mRadialBlurParameters.RadialBlurCenterType == RadialBlurInitCenterType.ScreenMiddle)
            {
                RadialBlurData.RadialBlurHorizontalCenter = 0.5f;
                RadialBlurData.RadialBlurVerticalCenter = 0.5f;
            }
            else if (mRadialBlurParameters.RadialBlurCenterType == RadialBlurInitCenterType.ScreenPos)
            {
                RadialBlurData.RadialBlurHorizontalCenter = mRadialBlurParameters.RadialBlurHorizontalCenter;
                RadialBlurData.RadialBlurVerticalCenter = mRadialBlurParameters.RadialBlurVerticalCenter;
            }
            else
            {
                Camera effectCamara = GetCamera();
#if UNITY_EDITOR
                if (SceneView.lastActiveSceneView != null && SceneView.lastActiveSceneView.hasFocus)
                {
                    effectCamara = SceneView.lastActiveSceneView.camera;

                }
#endif

                Vector3 screenPos = effectCamara.WorldToScreenPoint(transform.position);
                RadialBlurData.RadialBlurHorizontalCenter = screenPos.x / effectCamara.scaledPixelWidth;
                RadialBlurData.RadialBlurVerticalCenter = screenPos.y / effectCamara.scaledPixelHeight;

            }
        }


        private void UpdateColorDispersion(float currentTime)
        {
            if (!EnableColorDispersion || currentTime < ColorDispersionStartTime || currentTime > ColorDispersionEndTime)
            {
                ColorDispersionData.active = false;
                return;
            }

            float rate = (currentTime - ColorDispersionStartTime) / (ColorDispersionEndTime - ColorDispersionStartTime);
            ColorDispersionData.active = true;
            ColorDispersionData.ColorDispersionStrength = mColorDispersionParameters.ColorDispersionCurve.Evaluate(rate);
            ColorDispersionData.ColorDispersionU = mColorDispersionParameters.ColorDispersionUCurve.Evaluate(rate);
            ColorDispersionData.ColorDispersionV = mColorDispersionParameters.ColorDispersionVCurve.Evaluate(rate);
        }

        private void UpdateVibration(float currentTime)
        {
            if (!EnableVibration || currentTime < VibrationStartTime || currentTime > VibrationEndTime)
            {
                return;
            }

            float rate = (currentTime - VibrationStartTime) / (VibrationEndTime - VibrationStartTime);

            Camera effectCamara = GetCamera();
            Vector3 newPos = Vector3.zero;
            newPos = mVibrationStartPos + new Vector3(
                mVibrationParameters.VibrationX.Evaluate(rate) * mVibrationParameters.Magnitude,
                mVibrationParameters.VibrationY.Evaluate(rate) * mVibrationParameters.Magnitude,
                mVibrationParameters.VibrationZ.Evaluate(rate) * mVibrationParameters.Magnitude);
            effectCamara.transform.position = newPos;
        }

        private void UpdateSlowMotion(float currentTime)
        {
            if (!EnableSlowMotion)
            {
                return;
            }

            if (currentTime < SlowMotionStartTime || currentTime > SlowMotionEndTime)
            {
                if (mSlowMotionState)
                {
                    Time.timeScale = 1;
                    mSlowMotionState = false;
                }
            }
            else
            {
                mSlowMotionState = true;

                float rate = (currentTime - SlowMotionStartTime) / (SlowMotionEndTime - SlowMotionStartTime);
                Time.timeScale = mSlowMotionParameters.Speed.Evaluate(rate) * mSlowMotionParameters.Scale;
            }

        }
        #endregion

    }
}