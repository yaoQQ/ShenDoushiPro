using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace DevCode.Scene
{
    [ExecuteInEditMode]
    public class SceneEnvController : MonoBehaviour
    {
        public enum DebugMode
        {
            Default,
            LightDebug,
            ColorDebug
        }

        public DebugMode debugMode = DebugMode.Default;

        public bool isGray = false; // 是否开启灰度调试模式


        [LabelText("主光源")]
        public Light mainLight;
        [LabelText("后处理体积")]
        public Volume mainVolume;

        //[Header("主光源设置")]
        //[LabelText("旋转X")]
        //[Range(0f, 360f)]
        //public float rotationX; // Progress bar for X-axis
        //[LabelText("旋转Y")]
        //[Range(0f, 360f)]
        //public float rotationY; // Progress bar for Y-axis
        //[LabelText("旋转Z")]
        //[Range(0f, 360f)]
        //public float rotationZ; // Progress bar for Z-axis
        //[LabelText("主光源颜色")]
        //public Color mainLightColor = Color.white;
        //[LabelText("主光源强度")]
        //public float mainLightIntensity = 1f;

        [Header("高光设置")]
        [LabelText("主光源")]
        public Light frontLight;
        [LabelText("次光源")]
        public Light backLight;
        [LabelText("高光强度"),Range(0f, 5f)]
        public float specularLightScale = 1f;

        
        



        [Header("后处理")]
        [LabelText("泛光阈值")]
        public float bloomThreshold = 1f;
        [LabelText("泛光强度")]
        public float bloomIntensity = 1f;
        [LabelText("泛光散射")]
        public float bloomScatter = 1f;
        [LabelText("泛光颜色")]
        public Color bloomTint = Color.white;

        [Header("反射球")]
        public Cubemap cubemap;

        [Header("环境光")]
        [LabelText("天空颜色")]
        [ColorUsage(true, true)]
        public Color ambientSkyColor = Color.white;
        [LabelText("赤道颜色")]
        [ColorUsage(true, true)]
        public Color ambientEquatorColor = Color.white;
        [LabelText("地面颜色")]
        [ColorUsage(true, true)]
        public Color ambientGroundColor = Color.white;
        [Range(0f, 1)]
        [LabelText("环境光与光照贴图的混合强度")]
        public float giMixStrength = 1f;


        [Header("全局照明")]
        [Range(0f, 10)]
        [LabelText("光照贴图亮度")]
        public float lightmapBrightness = 1f;
        [Range(0f, 2)]
        [LabelText("光照贴图饱和度")]
        public float lightmapSaturation = 1f;
        [Range(0f, 2)]
        [LabelText("光照贴图对比度")]
        public float lightmapContrast = 1f;






        [Header("角色环境光")]
        [LabelText("天空颜色")]
        [ColorUsage(true, true)]
        public Color actorAmbientSkyColor = Color.white;
        [LabelText("赤道颜色")]
        [ColorUsage(true, true)]
        public Color actorAmbientEquatorColor = Color.white;
        [LabelText("地面颜色")]
        [ColorUsage(true, true)]
        public Color actorAmbientGroundColor = Color.white;
        [Range(0f, 10)]
        [LabelText("角色环境光强度")]
        public float actorAmbientStrength = 1f;

        [Header("角色灯光")]
        [LabelText("角色脸光源")]
        public Light faceLight;
        [LabelText("角色高光强度"), Range(0f, 5f)]
        public float actorSpecularLightScale = 1f;

        [Header("角色平面阴影")]
        [LabelText("阴影光源")]
        public Light shadowLight = null;
        [LabelText("阴影颜色")]
        public Color global_shadowColor = Color.black;
        [Range(-5, 5)]
        [LabelText("阴影高度偏移")]
        public float global_heightOffset = 0f;
        [Range(0, 10)]
        [LabelText("阴影透明度")]
        public float global_shadowFalloff = 0f;
        [Range(0, 1)]
        [LabelText("阴影透明裁剪")]
        public float global_shadowCutoff = 0.85f;


        [Header("角色高度渐变色")]
        [LabelText("开始颜色")]
        public Color global_GradientStartColor = Color.black;
        [LabelText("结束颜色")]
        public Color global_GradientEndColor = Color.white;
        [LabelText("高度")]
        public float global_GradientHeight = 10f;
        [LabelText("偏移")]
        public float global_GradientOffset = 0f;


        private Bloom bloom;

        private static int _SkyColor_ID = Shader.PropertyToID("_SkyColor");
        private static int _EquatorColor_ID = Shader.PropertyToID("_EquatorColor");
        private static int _GroundColor_ID = Shader.PropertyToID("_GroundColor");
        private static int _ComRefCube_ID = Shader.PropertyToID("_ComRefCube");

        private static int _ActorSkyColor_ID = Shader.PropertyToID("_ActorSkyColor");
        private static int _ActorEquatorColor_ID = Shader.PropertyToID("_ActorEquatorColor");
        private static int _ActorGroundColor_ID = Shader.PropertyToID("_ActorGroundColor");
        private static int _ActorAmbientStrength_ID = Shader.PropertyToID("_ActorAmbientStrength");


        private static int _FrontLightColor_ID = Shader.PropertyToID("_FrontLightColor");
        private static int _BackLightColor_ID = Shader.PropertyToID("_BackLightColor");
        private static int _FaceLightColor_ID = Shader.PropertyToID("_FaceLightColor");
        private static int _FrontLightDirection_ID = Shader.PropertyToID("_FrontLightDirection");
        private static int _BackLightDirection_ID = Shader.PropertyToID("_BackLightDirection");
        private static int _FaceLightDirection_ID = Shader.PropertyToID("_FaceLightDirection");

        private static int _SpecularLightScale_ID = Shader.PropertyToID("_SpecularLightScale");
        private static int _ActorSpecularLightScale_ID = Shader.PropertyToID("_ActorSpecularLightScale");
        private static int _GIMixStrength_ID = Shader.PropertyToID("_GIMixStrength");

        private static int _LightmapBrightness_ID = Shader.PropertyToID("_LightmapBrightness");
        private static int _LightmapSaturation_ID = Shader.PropertyToID("_LightmapSaturation");
        private static int _LightmapContrast_ID = Shader.PropertyToID("_LightmapContrast");


        private static int _GlobalShadowLightDir_ID = Shader.PropertyToID("_GlobalShadowLightDir");
        private static int _GlobalHeightOffset_ID = Shader.PropertyToID("_GlobalHeightOffset");
        private static int _GlobalShadowColor_ID = Shader.PropertyToID("_GlobalShadowColor");
        private static int _GlobalShadowFalloff_ID = Shader.PropertyToID("_GlobalShadowFalloff");
        private static int _GlobalShadowCutoff_ID = Shader.PropertyToID("_GlobalShadowCutoff");

        private static int _GlobalGradientStartColor_ID = Shader.PropertyToID("_GlobalGradientStartColor");
        private static int _GlobalGradientEndColor_ID = Shader.PropertyToID("_GlobalGradientEndColor");
        private static int _GlobalGradientHeight_ID = Shader.PropertyToID("_GlobalGradientHeight");
        private static int _GlobalGradientOffset_ID = Shader.PropertyToID("_GlobalGradientOffset");


        // Start is called before the first frame update
        void Start()
        {
            SetAmbientColor();
        }

        private void OnEnable()
        {
            SetAmbientColor();
        }

        // Update is called once per frame
        void Update()
        {
#if !ART_PROJECT
            if (!Application.isPlaying) { SetAmbientColor(); }
#else
            SetAmbientColor();
#endif
        }

        public void SetAmbientColor()
        {
            //if (mainLight != null)
            //{
            //    mainLight.color = mainLightColor;
            //    mainLight.intensity = mainLightIntensity;
            //    mainLight.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
            //}

            Shader.SetGlobalColor(_SkyColor_ID, ambientSkyColor);
            Shader.SetGlobalColor(_EquatorColor_ID, ambientEquatorColor);
            Shader.SetGlobalColor(_GroundColor_ID, ambientGroundColor);

            Shader.SetGlobalColor(_ActorSkyColor_ID, actorAmbientSkyColor);
            Shader.SetGlobalColor(_ActorEquatorColor_ID, actorAmbientEquatorColor);
            Shader.SetGlobalColor(_ActorGroundColor_ID, actorAmbientGroundColor);
            Shader.SetGlobalFloat(_ActorAmbientStrength_ID, actorAmbientStrength);

            if (mainVolume != null)
            {
                if (mainVolume.profile.TryGet(out bloom))
                {
                    bloom.threshold.value = bloomThreshold;
                    bloom.intensity.value = bloomIntensity;
                    bloom.scatter.value = bloomScatter;
                    bloom.tint.value = bloomTint;
                }
            }

            if (cubemap != null)
            {
                Shader.SetGlobalTexture(_ComRefCube_ID, cubemap);
            }

           

            if (frontLight != null)
            {
                Shader.SetGlobalColor(_FrontLightColor_ID, frontLight.color);
                Shader.SetGlobalVector(_FrontLightDirection_ID, -frontLight.transform.forward);
            }

            if (backLight != null)
            {
                Shader.SetGlobalColor(_BackLightColor_ID, backLight.color);
                Shader.SetGlobalVector(_BackLightDirection_ID, -backLight.transform.forward);
            }

            if (faceLight != null)
            {
                Shader.SetGlobalColor(_FaceLightColor_ID, faceLight.color);
                Shader.SetGlobalVector(_FaceLightDirection_ID, -faceLight.transform.forward);
            }


            Shader.SetGlobalFloat(_SpecularLightScale_ID, specularLightScale);
            Shader.SetGlobalFloat(_ActorSpecularLightScale_ID, actorSpecularLightScale);
            Shader.SetGlobalFloat(_GIMixStrength_ID, giMixStrength);

            Shader.SetGlobalFloat(_LightmapBrightness_ID, lightmapBrightness);
            Shader.SetGlobalFloat(_LightmapSaturation_ID, lightmapSaturation);
            Shader.SetGlobalFloat(_LightmapContrast_ID, lightmapContrast);

            Shader.SetGlobalVector(_GlobalShadowLightDir_ID, shadowLight == null ? Vector3.one: -shadowLight.transform.forward);
            Shader.SetGlobalColor(_GlobalShadowColor_ID, global_shadowColor);
            Shader.SetGlobalFloat(_GlobalHeightOffset_ID, global_heightOffset);
            Shader.SetGlobalFloat(_GlobalShadowFalloff_ID, global_shadowFalloff);
            Shader.SetGlobalFloat(_GlobalShadowCutoff_ID, global_shadowCutoff);

            Shader.SetGlobalColor(_GlobalGradientStartColor_ID, global_GradientStartColor);
            Shader.SetGlobalColor(_GlobalGradientEndColor_ID, global_GradientEndColor);
            Shader.SetGlobalFloat(_GlobalGradientHeight_ID, global_GradientHeight);
            Shader.SetGlobalFloat(_GlobalGradientOffset_ID, global_GradientOffset);



#if UNITY_EDITOR
            switch (debugMode)
            {
                case DebugMode.Default:
                    Shader.DisableKeyword("_LIGHT_DEBUG");
                    Shader.DisableKeyword("_COLOR_DEBUG");
                    break;
                case DebugMode.LightDebug:
                    Shader.EnableKeyword("_LIGHT_DEBUG");
                    Shader.DisableKeyword("_COLOR_DEBUG");
                    break;
                case DebugMode.ColorDebug:
                    Shader.DisableKeyword("_LIGHT_DEBUG");
                    Shader.EnableKeyword("_COLOR_DEBUG");
                    break;
            }
            if (isGray)
            {
                Shader.EnableKeyword("_GRAY_DEBUG");
            }
            else
            {
                Shader.DisableKeyword("_GRAY_DEBUG");
            }
#endif
        }
    }

}

