using UnityEngine;
using UnityEditor;
using System;

namespace TA_Tools.StyledEditor.StyledMaterial
{
    public class StyledBaseDecorator : MaterialPropertyDrawer
    {
        public override void OnGUI(Rect position, MaterialProperty prop, String label, MaterialEditor materiaEditor)
        {

        }

        public override float GetPropertyHeight(MaterialProperty prop, string label, MaterialEditor editor)
        {
            return 0;
        }
    }
}
