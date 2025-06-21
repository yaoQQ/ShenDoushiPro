using UnityEngine;
using UIWidget;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PlatformGameUpdateView : BaseView
{

    [SerializeField]

    private Mid_platform_game_update_panel main_mid;
    private EnumGameID gameId = 0;
    //private int gameType= 0;
    private int size = 0;
    private string confirmContent = "当前游戏有新版更新后才能正常进入,是否进行更新";
    private string cancelContent = "快要下载完了, 确定中止下载吗?";

    public PlatformGameUpdateView()
    {

        this.viewName = "platform_game_rule_panel";
        this.loadOrders = new List<string>() { "BasePackage:platform_game_update_panel" };
        //test@@@
        setViewAttribute(UIViewType.Platform_Second_View, UIViewEnum.Platform_Game_Update_View, false);
    }


    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_platform_game_update_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        addEvent();
    }
    protected override void onShowHandler(object msg)
    {
       
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();
        GameLoadInfo gameLoadInfo = (GameLoadInfo)msg;
        this.gameId = gameLoadInfo.gameId;
        this.size = gameLoadInfo.size;
        addNotice();
        showUpdatePanel();
    }
    private void addEvent()
    {
        this.main_mid.close_down_image.AddEventListener(UIEvent.PointerClick, this.showCancelPanel);
    }
    private void addNotice()
    {
        NoticeManager.Instance.AddNoticeLister(NoticeType.Game_Update_Progress, this.onGameUpdateProgress);
    }
    private void removeNotice()
    {
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.Game_Update_Progress, this.onGameUpdateProgress);
    }
    protected override void onClose()
    {
        removeNotice();
    }

    private void showUpdatePanel()
    {
        this.main_mid.update_panel.gameObject.SetActive(true);
        this.main_mid.game_down_panel.gameObject.SetActive(false);
        this.main_mid.update_mask_image.gameObject.SetActive(false);
        this.main_mid.down_process_fg.Img.fillAmount = 0;

        float showSize = size;
        showSize = showSize / 100;
        if (showSize < 0.01)
        {
            showSize = 0.01f;
        }
        this.main_mid.update_text.text = string.Format("{0}({1}M)", confirmContent, showSize);
        this.main_mid.update_left_btn.Txt.text = "下次吧";
        this.main_mid.update_right_btn.Txt.text = "确定更新";
        this.main_mid.update_left_btn.AddEventListener(UIEvent.PointerClick, closeGameUpdateView);
        this.main_mid.update_right_btn.AddEventListener(UIEvent.PointerClick, confirmUpdateHandler);

     }
    private void closeGameUpdateView(PointerEventData eveData)
    {
        UIViewManager.Instance.Close(UIViewEnum.Platform_Game_Update_View);
    }
    private void confirmUpdateHandler(PointerEventData eveData)
    {
        //  CSGameProcessManager.StartDownload(gameId)
        this.main_mid.update_panel.gameObject.SetActive(false);
        closeGameUpdateView(null);
        showLoadingGame();
        return;
       // this.main_mid.game_down_panel.gameObject.SetActive(true);
       //// downloadGameIcon(TableBaseGameList.data[gameId].iconURL, this.main_mid.down_game_image)
       //var dataList= TableBaseGameList.Instance.GetBannerList();
       // HeadBannerData gameConfigData = dataList[(int)gameId];
       // PlatformPicManagerProxy.Instance.downloadGameIcon(gameConfigData.iconURL, this.main_mid.down_game_image);
       // this.main_mid.down_game_text.text = string.Format("正在下载{0}", gameConfigData.name);
       // this.main_mid.down_cancel_btn.AddEventListener(UIEvent.PointerClick, this.showCancelPanel);
       // PreloadManager.Instance.PreLoadPackage(gameId.ToString());
    }
    private void showLoadingGame()
    {
        Logger.PrintDebug("showLoadingGame() gameId=" + gameId.ToString());
        PreloadManager.Instance.PreLoadPackage(gameId.ToString());
    }
    private void showCancelPanel(PointerEventData eveData)
    {
        this.main_mid.update_panel.gameObject.SetActive(true);
        this.main_mid.game_down_panel.gameObject.SetActive(true);
        this.main_mid.update_mask_image.gameObject.SetActive(true);
        this.main_mid.update_text.text = string.Format("{0}", cancelContent);

        this.main_mid.update_left_btn.Txt.text = "狠心中止";
        this.main_mid.update_right_btn.Txt.text = "继续下载";
        this.main_mid.update_left_btn.AddEventListener(UIEvent.PointerClick, this.cancelUpdateHandler);

        this.main_mid.update_right_btn.AddEventListener(UIEvent.PointerClick, (eve) => {
            this.main_mid.update_mask_image.gameObject.SetActive(false);
            this.main_mid.update_panel.gameObject.SetActive(false);
        });
    }
  
    private void cancelUpdateHandler(PointerEventData eveData)
    {
        //  CSGameProcessManager.CancelDownload(gameId)
        closeGameUpdateView(null);
    }
    private void onGameUpdateProgress(string noticeType, BaseNotice notice)
    {
        float process = (float)((ObjectNotice)notice).GetObj();
        if (process >= 1)
        {
            closeGameUpdateView(null);
            // GameManager.startGame(gameId, gameType, shopId, roomId)
        }
        else
        {
            this.main_mid.down_process_fg.Img.fillAmount = process;

            this.main_mid.down_capacity_text.text = string.Format("{0}/{1}", Mathf.Floor((size * process) / 1024), Mathf.Floor(size / 1024));

            this.main_mid.down_percent_text.text = string.Format("{0:##.#}", process * 100) + "%";
        }
    }
   

}