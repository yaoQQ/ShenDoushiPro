using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class GunListPanel : BaseView
{
    [SerializeField]
    private Mid_GunListPanel main_mid;
    private GunAttriItem gunItem;
    private List<GunAttriItem> gunList;
    public GunListPanel()
    {

        this.viewName = "GunListPanel";
        this.loadOrders = new List<string>() { "OutSpacePackage:GunListPanel" };

        setViewAttribute(UIViewType.Game_1, UIViewEnum.OutSpaceGunListPanel, false);
        Logger.PrintDebug("@@@@ GunListPanel()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        Logger.PrintDebug("GunListPanel onLoadUIEnd complte!! gameObject=" + gameObject);
        main_mid = gameObject.AddComponent<Mid_GunListPanel>();
        Logger.PrintDebug("onLoadUIEnd complte!! main_mid=" + main_mid);
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);
        gunItem = main_mid.gunItem;
        gunList = new List<GunAttriItem>();
       
        addEvent();
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
        OutSpaceGameManager.Instance.PauseGame();
        Logger.PrintDebug("onShowHandler() 显示界面：msg=" + msg);
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();


        bool isGameList = msg is List<GameGunData>;
        List<GameGunData> getList = msg as List<GameGunData>;

        Logger.PrintColor("red", "XXXX 在热更项目错误待解决isGameList=" + isGameList);
        Logger.PrintColor("red", "XXXX 在热更项目错误待解决GameList=" + getList);


        for (int i = 0; i < getList.Count; i++)
        {
            Logger.PrintDebug("======================");
            Logger.PrintDebug("getList[" + i + "].level=" + getList[i].level);
            Logger.PrintDebug("getList[" + i + "].attriType=" + getList[i].attriType);
            Logger.PrintDebug("getList[" + i + "].gunType=" + getList[i].gunType);
        }
        showGunList(getList);
        //string textStr = msg as string;
        //onShowTopTips(textStr);
    }
    private void showGunList(List<GameGunData> gunDataList)
    {
        RestView();
        for (int i=0;i< gunDataList.Count; i++)
        {
            GunAttriItem item;
            if (gunList.Count<= i)
            {
                GameObject obj = GameObject.Instantiate(gunItem.gameObject);
                obj.transform.parent = gunItem.transform.parent;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                item = obj.GetComponent<GunAttriItem>();
                gunList.Add(item);
                item.SelectFun = SelectItem;
            }
            else
            {
                item = gunList[i];
            }
            item.gameObject.SetActive(true);
            item.RefreshByData(gunDataList[i]);
           



        }
    }
    private void SelectItem(GameGunData itemGameData)
    {
        if (itemGameData.gunType != GunType.None)
        {
            CommonView.showTopTips("升級 "+ itemGameData.gunType);
        }
        else
        {
            CommonView.showTopTips("升級 " + itemGameData.attriType);
        }
        NoticeManager.Instance.Dispatch(OutSpaceNotice.LevelUpSelectData, itemGameData);
        UIViewManager.Instance.Close(UIViewEnum.OutSpaceGunListPanel);
    }
    private void RestView()
    {
        for(int i=0;i< gunList.Count; i++)
        {
            gunList[i].gameObject.SetActive(false);
        }
    }
    private void addEvent()
    {
        main_mid.closeBtn.AddEventListener(UIWidget.UIEvent.PointerClick, closeView);
    }

    private void closeView(PointerEventData eve)
    {
        UIViewManager.Instance.Close(UIViewEnum.OutSpaceGunListPanel);  
    }


    protected override void onClose()
    {
        OutSpaceGameManager.Instance.ResumeGame();
        base.onClose();
        
    }

}