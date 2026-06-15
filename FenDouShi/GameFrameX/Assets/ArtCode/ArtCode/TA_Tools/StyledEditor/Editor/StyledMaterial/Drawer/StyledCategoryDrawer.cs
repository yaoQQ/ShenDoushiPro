using UnityEngine;
using UnityEditor;
using System;

namespace TA_Tools.StyledEditor.StyledMaterial
{
    public class StyledCategoryDrawer : MaterialPropertyDrawer
    {
        public float top;
        public float down;
        public string colapsable;
        public bool begin;

        public StyledCategoryDrawer()
        {
            this.colapsable = "true";
            this.top = 10;
            this.down = 10;
        }

        public StyledCategoryDrawer( string colapsable)
        {
            this.colapsable = colapsable;
            this.top = 10;
            this.down = 10;
        }

        public StyledCategoryDrawer(float top, float down)
        {
            this.colapsable = "true";
            this.top = top;
            this.down = down;
        }

        public StyledCategoryDrawer(string colapsable, float top, float down)
        {
            this.colapsable = colapsable;
            this.top = top;
            this.down = down;
        }


        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 0;
        }

        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materiaEditor)
        {
            GUI.enabled = true;
            EditorGUI.indentLevel = 0;

            bool isColapsable = false;
            if (colapsable == "true")
            {
                isColapsable = true;
            }

            bool isEnabled = true;
            if (prop.floatValue < 0.5f)
            {
                isEnabled = false;
            }

            isEnabled = TA_Tools.StyledEditor.StyledGUI.StyledGUI.DrawInspectorCategory(prop.displayName, isEnabled, top, down, isColapsable);

            if (isEnabled)
            {
                prop.floatValue = 1;
            }
            else
            {
                prop.floatValue = 0;
            }
        }



    }
}
