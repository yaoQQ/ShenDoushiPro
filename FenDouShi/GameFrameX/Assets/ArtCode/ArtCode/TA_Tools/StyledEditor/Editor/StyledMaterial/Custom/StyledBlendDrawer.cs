using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

namespace TA_Tools.StyledEditor.StyledMaterial
{
    public class StyledBlendDrawer : MaterialPropertyDrawer
    {
        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 0;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, string label, MaterialEditor materialEditor)
        {
            BlendProperties properties = new BlendProperties(prop.targets);

            EditorGUI.BeginChangeCheck();

            DoBlendModePopup(Styles.blendModeStateText, properties.blendModeState, blendModeStrings, materialEditor);

            if (EditorGUI.EndChangeCheck())
            {
                foreach (Material item in prop.targets)
                {
                    SetKeyword(item);
                }
            }
        }

        public static void DoBlendModePopup(GUIContent label, MaterialProperty property, string[] options, MaterialEditor materialEditor)
        {
            if (property == null) return;

            EditorGUI.showMixedValue = property.hasMixedValue;

            var mode = property.floatValue;

            //重新映射一下
            if ((BlendModeState)mode == BlendModeState.Opaque)
            {
                mode = 0;
            }
            else if ((BlendModeState)mode == BlendModeState.Alpha)
            {
                mode = 1;
            }
            else if ((BlendModeState)mode == BlendModeState.PreMultiply)
            {
                mode = 2;
            }
            else if ((BlendModeState)mode == BlendModeState.Additive)
            {
                mode = 3;
            }

            EditorGUI.BeginChangeCheck();
            mode = EditorGUILayout.Popup(label, (int)mode, options);
            if (EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo(label.text);

                //重新映射一下
                if (mode == 0)
                {
                    mode = (float)BlendModeState.Opaque;
                }
                else if (mode == 1)
                {
                    mode = (float)BlendModeState.Alpha;
                }
                else if (mode == 2)
                {
                    mode = (float)BlendModeState.PreMultiply;
                }
                else if (mode == 3)
                {
                    mode = (float)BlendModeState.Additive;
                }
                property.floatValue = mode;
            }

            EditorGUI.showMixedValue = false;
        }

        private void SetKeyword(Material material)
        {
            if (material.HasProperty("_BlendMode"))
            {
                var curType = (BlendModeState)material.GetFloat("_BlendMode");

                switch (curType)
                {
                    case BlendModeState.Opaque:
                        material.SetFloat("_SrcBlend", (float)BlendMode.One);
                        material.SetFloat("_DstBlend", (float)BlendMode.Zero);
                        material.SetFloat("_PreMultiAlpha", (float)0);
                        break;
                    case BlendModeState.Additive:
                        material.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
                        material.SetFloat("_DstBlend", (float)BlendMode.One);
                        material.SetFloat("_PreMultiAlpha", (float)0);
                        break;
                    case BlendModeState.PreMultiply:
                        material.SetFloat("_SrcBlend", (float)BlendMode.One);
                        material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
                        material.SetFloat("_PreMultiAlpha", (float)1);
                        break;
                    case BlendModeState.Alpha:
                        material.SetFloat("_SrcBlend", (float)BlendMode.SrcAlpha);
                        material.SetFloat("_DstBlend", (float)BlendMode.OneMinusSrcAlpha);
                        material.SetFloat("_PreMultiAlpha", (float)0);
                        break;
                }
            }
           
        }

        public override void Apply(MaterialProperty prop)
        {
            base.Apply(prop);
        
            if (prop.hasMixedValue)
                return;

            foreach (Material item in prop.targets)
            {
                SetKeyword(item);
            }
        }


        #region Enum And Struct


        private static string[] blendModeStrings = new string[]
        {
            "不透明",
            "透明",
            "预乘透明",
            "加法"
        };

        private enum BlendModeState
        {
            Opaque = 0,
            Alpha = 10,
            PreMultiply = 2,
            Additive = 1,
        }


        private struct BlendProperties
        {

            public MaterialProperty blendModeState;


            public BlendProperties(Object[] objects)
            {
                blendModeState = MaterialEditor.GetMaterialProperty(objects, "_BlendMode");
            }
        }

        private static class Styles
        {
            public static GUIContent blendModeStateText = new GUIContent("混合模式",
                "混合模式");
        }
        #endregion
    }
}