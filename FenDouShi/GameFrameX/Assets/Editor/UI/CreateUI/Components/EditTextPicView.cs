using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using UIWidget;
public class EditTextPicView : BaseEditView
{
    public override void Render(EditorWindow window, UIBaseWidget widget)
    {

        TextPicWidget textPicWidget = widget as TextPicWidget;
        DrawCommon(window, widget.gameObject, widget);

        

    }
}
