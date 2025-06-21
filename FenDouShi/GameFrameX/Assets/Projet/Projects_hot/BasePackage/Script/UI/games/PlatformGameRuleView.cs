using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UIWidget;
using UnityEngine.EventSystems;

public class PlatformGameRuleView : BaseView
{

    [SerializeField]

    private Mid_platform_game_rule_panel main_mid;
    public PlatformGameRuleView()
    {

        this.viewName = "platform_game_rule_panel";
        this.loadOrders = new List<string>() { "BasePackage:platform_game_rule_panel" };
       
        setViewAttribute(UIViewType.Platform_Second_View, UIViewEnum.Platform_Game_Rule_View, false);
    }


    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_platform_game_rule_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        addEvent();
    }
    protected override void onShowHandler(object msg)
    {
        int dataId = (int)msg;
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();
        showGameRuleView(dataId);
    }
    private void showGameRuleView(int dataId)
    {
        // ProjectPackage
        Debug.Log("@@@@@@@@@@@ dataId="+ dataId);
        List<HeadBannerData> gameData = TableBaseGameList.Instance.GetBannerList();
        string content= gameData[dataId].GameRule;
        List<string> shotList = gameData[dataId].GamePicture;
        this.main_mid.rule_content_text.text = content;
        Debug.Log("@@@@@@@@@@@ gameData.count=" + gameData.Count);
        Debug.Log("@@@@@@@@@@@ content=" + content);
        Debug.Log("@@@@@@@@@@@ shotList=" + shotList);
        Debug.Log("@@@@@@@@@@@ shotList.count=" + shotList.Count);
        if (shotList == null || shotList.Count <= 0)
        {
            this.main_mid.game_shot_panel.gameObject.SetActive(false);
        }
        else
        {
            this.main_mid.game_shot_panel.gameObject.SetActive(true);
            this.main_mid.game_shot_scroll_panel.SetCellData(shotList,(go,data, startIndex, i)=>{
                Debug.LogFormat("@@@@@@@@@@@ index={0} ，i={1}", startIndex, i);
                var item = this.main_mid.shotItemArr[i+1];
                string gameName = gameData[dataId].package;
                string picName = "GamePictrue/" + gameName+"/" + data;
                Debug.LogFormat("@@@@@@@@@@@ picName[{0}]=" , picName);
                item.game_shot_image.Img.color = new Color(233, 444, 222);
                PlatformPicManagerProxy.Instance.downloadGameIcon(picName, item.game_shot_image);
            });
        }
        this.main_mid.enter_game_btn.AddEventListener(UIEvent.PointerClick, (eveData) =>
        {
            this.closeGameRuleView(null);
            // GameManager.enterGame(gameId, EnumGameType.Hall, -1, -1);
            GameLoadManager.Instance.checkDownloadOrStartGame(EnumGameID.littleprince, 0);
        });
    }
    private void addEvent()
    {
        this.main_mid.mask_image.AddEventListener(UIEvent.PointerClick, this.closeGameRuleView);
        this.main_mid.close_rule_image.AddEventListener(UIEvent.PointerClick, this.closeGameRuleView);
     }
    private void closeGameRuleView(PointerEventData eveData)
    {
        UIViewManager.Instance.Close(UIViewEnum.Platform_Game_Rule_View);
    }
}
