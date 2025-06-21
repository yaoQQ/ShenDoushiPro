using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using UIWidget;

public class EditBannerView : BaseEditView
{

    public override void Render(EditorWindow window, UIBaseWidget widget)
    {
        BannerWidget banner = widget as BannerWidget;
        DrawCommon(window, widget.gameObject, widget);
    }

}
