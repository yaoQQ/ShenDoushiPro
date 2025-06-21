using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class testView : BaseView
{
    [SerializeField]
    private Mid_login_panel2 main_mid;
    public testView()
    {

        this.viewName = "testView";
        this.loadOrders = new List<string>() { "BasePackage:login_panel2" };
        //test@@@
        setViewAttribute(UIViewType.Platform_Second_View, UIViewEnum.LoginView2, false);

    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        main_mid = gameObject.AddComponent<Mid_login_panel2>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);
    }
}
