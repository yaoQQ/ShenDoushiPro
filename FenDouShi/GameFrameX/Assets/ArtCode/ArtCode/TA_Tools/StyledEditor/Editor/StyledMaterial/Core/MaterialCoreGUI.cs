using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TA_Tools.StyledEditor.Constants;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using static TA_Tools.StyledEditor.StyledMaterial.StyledRenderSettingConfig;

namespace TA_Tools.StyledEditor.StyledMaterial
{
    public class MaterialCoreGUI : ShaderGUI
    {
        //bool multiSelection = false;
        //bool showAdvancedSetting = true;
        StyledRenderSettingConfig selectedConfig = null;

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            var material0 = materialEditor.target as Material;
            var materials = materialEditor.targets;

            //if (materials.Length > 1)
            //    multiSelection = true;

            DrawDynamicInspector(material0, materialEditor, props);

        }

        void DrawDynamicInspector(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
        {
            var customPropsList = new List<MaterialProperty>();

            var shaderName = material.shader.name;

            var bannerText = Path.GetFileNameWithoutExtension(shaderName);

            StyledGUI.StyledGUI.DrawInspectorBanner(bannerText);

            var isSet = DrawSetRenderSetting();

            bool isShowByCategory = true;
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];

                if (((int)prop.flags & (int)MaterialProperty.PropFlags.HideInInspector) == 1)
                    continue;

                if (prop.name == "unity_Lightmaps")
                    continue;

                if (prop.name == "unity_LightmapsInd")
                    continue;

                if (prop.name == "unity_ShadowMasks")
                    continue;


                if (prop.name.StartsWith("_Category"))
                {
                    isShowByCategory = true;
                    if (prop.name.StartsWith("_Category_Colapsable"))
                    {
                        isShowByCategory = prop.floatValue == 1.0f;
                    }

                    customPropsList.Add(prop);
                }
                else
                {
                    if (isShowByCategory == true)
                    {
                        customPropsList.Add(prop);
                    }
                }
            }

            //Draw Custom GUI
            for (int i = 0; i < customPropsList.Count; i++)
            {
                var displayName = customPropsList[i].displayName;

                materialEditor.ShaderProperty(customPropsList[i], displayName);
            }

            //GUILayout.Space(3.5f);

            //showAdvancedSetting = YLib.StyledGUI.StyledGUI.DrawInspectorCategory("Advanced Settings", showAdvancedSetting, 10,7,true);

            if (isShowByCategory)
            {
                materialEditor.EnableInstancingField();

                materialEditor.RenderQueueField();

                materialEditor.DoubleSidedGIField();

                EditorGUI.BeginChangeCheck();
                materialEditor.LightmapEmissionProperty();
                if (EditorGUI.EndChangeCheck())
                {
                    foreach (Material item in materialEditor.targets)
                    {
                        item.globalIlluminationFlags &= ~MaterialGlobalIlluminationFlags.EmissiveIsBlack;
                    }
                }
            }
            //GUILayout.Space(10);
            //EditorGUI.indentLevel++;

            if (isSet)
            {
                foreach (Material item in materialEditor.targets)
                {
                    ApplyRenderSetting(item);
                }
            }

        }

        public override void AssignNewShaderToMaterial(Material material, Shader oldShader, Shader newShader)
        {
            if (material == null)
                throw new ArgumentNullException("material");

            var renderQueue = material.renderQueue;

            base.AssignNewShaderToMaterial(material, oldShader, newShader);

            material.renderQueue = renderQueue;
        }

        private bool DrawSetRenderSetting()
        {
            bool isSet = false;
            GUILayout.BeginHorizontal(); // 创建一个水平布局

            // 左边：绘制一个标签
            GUILayout.Label("设置渲染预设:", GUILayout.Width(100));

            // 中间：绘制一个对象选择框，选择 StyledRenderSettingConfig 对象
            selectedConfig = (StyledRenderSettingConfig)EditorGUILayout.ObjectField(selectedConfig, typeof(StyledRenderSettingConfig), false);

            // 右边：绘制一个按钮，点击时打印选中的对象名字
            if (GUILayout.Button("使用"))
            {
                if (selectedConfig != null)
                {
                    isSet = true;
                }
                else
                {
                    Debug.Log("没有选择预设");
                }
            }

            GUILayout.EndHorizontal(); // 结束水平布局

            return isSet;
        }

        private void ApplyRenderSetting(Material material)
        {
            material.SetInt("_SrcBlend", (int)selectedConfig._SrcBlend);
            material.SetInt("_DstBlend", (int)selectedConfig._DstBlend);
            material.SetInt("_ZWrite", (int)selectedConfig._ZWrite);
            material.SetInt("_ZTest", (int)selectedConfig._ZTest);
            material.SetInt("_Cull", (int)selectedConfig._Cull);
            material.SetFloat("_AlphaClip", selectedConfig._AlphaClip ? 1.0f : 0.0f);
            material.SetFloat("_Cutoff", selectedConfig._Cutoff);

            int rendererQueue = (int)selectedConfig._RenderQueue;
            material.renderQueue = rendererQueue == -1 ? -1 : rendererQueue + selectedConfig._RenderQueueOffset;

            if (selectedConfig._AlphaClip)
            {
                material.EnableKeyword("_ALPHATEST_ON");
            }
            else
            {
                material.DisableKeyword("_ALPHATEST_ON");
            }
        }
    }

}
