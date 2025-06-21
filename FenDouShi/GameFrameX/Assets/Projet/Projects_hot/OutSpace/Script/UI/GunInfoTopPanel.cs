
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GunInfoTopPanel : BaseView
{
    [SerializeField]
    private Mid_GunInfoPanel main_mid;
    public GunInfoTopPanel()
    {

        this.viewName = "GunInfoTopPanel";
        this.loadOrders = new List<string>() { "OutSpacePackage:GunInfoTopPanel" };

        setViewAttribute(UIViewType.Game_1, UIViewEnum.OutSpaceGunTopInfoPanel, false);
        Logger.PrintDebug("@@@@ GunAttriInfoPanel()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_GunInfoPanel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);
       

    }
    private void OpenSetting(PointerEventData pointData)
    {
        Logger.PrintDebug("click openSetting");
    }
    private void CloseFun(PointerEventData pointData)
    {

    }

    protected override void onShowHandler(System.Object msg)
    {
      
       
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();

        List<GameGunData> gunDataList = OutSpacePlayerInfoManager.Instance.GetGameAttriList();
       
        if (gunDataList != null)
        {
            Logger.PrintColor("blue", "GunAttriInfoPanel gunDataList.count=" + gunDataList.Count);
            main_mid.selectGunListView.showGunList(gunDataList);
        }
        main_mid.selectGunListView.InitView(main_mid.gunInfoview);
        OutSpaceGameManager.Instance.PauseGame();
    }
  




    protected override void onClose()
    {
        OutSpaceGameManager.Instance.ResumeGame();
        base.onClose();

    }

}