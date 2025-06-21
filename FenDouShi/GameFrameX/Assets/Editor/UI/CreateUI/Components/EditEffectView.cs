using UnityEngine;
using System.Collections;
using UnityEditor;
using UIWidget;


public class EditEffectView : BaseEditView
{

    public override void Render(EditorWindow window, UIBaseWidget widget)
    {
        EffectWidget effectWidget = widget as EffectWidget;
        DrawCommon(window, widget.gameObject, widget);

        effectWidget.maskImg.enabled = EditorGUILayout.Toggle("使用自身遮罩 ", effectWidget.maskImg.enabled, GUILayout.ExpandWidth(true));

        effectWidget.effectName = EditorGUILayout.TextField("特效资源名 ",effectWidget.effectName, GUILayout.ExpandWidth(true));
    }
}