
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UIWidget;

public class GunAttriTopPanel : BaseView
{
    [SerializeField]
    private Mid_GunAttriTopPanel main_mid;
    public GunAttriTopPanel()
    {

        this.viewName = "GunAttriTopPanel";
        this.loadOrders = new List<string>() { "OutSpacePackage:GunAttriPanel" };

        setViewAttribute(UIViewType.Game_1, UIViewEnum.OutSpaceGunAttriInfoPanel, false);
        Logger.PrintDebug("@@@@ GunAttriTopPanel()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {

        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_GunAttriTopPanel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        if (main_mid.colseBtnBg != null)
        {
            main_mid.colseBtnBg.AddEventListener(UIEvent.PointerClick, colseView);
        }
    }
    private void OpenSetting(PointerEventData pointData)
    {
        Logger.PrintDebug("click openSetting");
    }
    private void colseView(PointerEventData eve)
    {
        UIViewManager.Instance.Close(UIViewEnum.OutSpaceGunAttriInfoPanel);
    }

    protected override void onShowHandler(object msg)
    {


        GameObject go = this.main_mid.gameObject;
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();

        OutSpaceGameManager.Instance.PauseGame();

        GunInfo gunInfo = msg as GunInfo;
        if (gunInfo)
        {
            main_mid.gunAttriview.reFreshByGunInfo(gunInfo);
            
            main_mid.addAttriInfoview.reFreshByAddAttriInfo();
        }
    }





    protected override void onClose()
    {
        OutSpaceGameManager.Instance.ResumeGame();
        base.onClose();

    }

}