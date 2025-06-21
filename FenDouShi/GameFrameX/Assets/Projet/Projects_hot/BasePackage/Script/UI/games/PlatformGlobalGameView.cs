using UnityEngine;

using System.Collections.Generic;
using UIWidget;
using UnityEngine.EventSystems;

public class PlatformGlobalGameView : BaseView
{

    [SerializeField]

    private Mid_platform_game_panel main_mid;
    public PlatformGlobalGameView()
    {

        this.viewName = "PlatformGlobalGameView";
        this.loadOrders = new List<string>() { "BasePackage:platform_game_panel" };
        //test@@@
        setViewAttribute(UIViewType.Main_view, UIViewEnum.Platform_Global_Game_View, true);
    }

 
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        // main_mid = gameObject.GetComponent<UIBaseMono>();
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_platform_game_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);
        TableBaseGameList.Instance.Init();

        this.main_mid.closeBtn.AddEventListener(UIEvent.PointerClick, closeFun);
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
        string textStr = msg as string;
        reqFind();
        updateHeadBanner();
        onHotGameInfoChangeHandler();
    }
    private void initOriginSprite()
    {
        BannerWidget bannerWidget = main_mid.head_banner;
        for(int i=0;i< bannerWidget.viewList.Length - 1; i++)
        {
            ImageWidget image= bannerWidget.viewList[i].GetComponent<ImageWidget>();
        
        }
    }
    private void updateHeadBanner()
    {
        List<HeadBannerData> gameList = TableBaseGameList.Instance.GetBannerList();
        BannerWidget bannerWidget = main_mid.head_banner;
        bannerWidget.content = bannerWidget.viewList[0];
        bannerWidget.SetBannerData(gameList, 0, (go,data,currIndex) => {
            int index = currIndex - 1;
            RectTransform[] viewList = bannerWidget.viewList;
          //  Debug.LogFormat("currIndex{0} index: {1}, banner_name: {2}", currIndex, index, data);
            HeadBannerData headData = (index<0||index >= gameList.Count) ? null : gameList[index];
            showGameBanner(headData, viewList[0]);
            headData = (index+1 < 0 || index +1 >= gameList.Count) ? null : gameList[index+1];
            showGameBanner(headData, viewList[1]);
            headData = (index +2 < 0||index + 2 >= gameList.Count) ? null : gameList[index + 2];
            showGameBanner(headData, viewList[2]);
        });
        this.main_mid.head_banner.AddEventListener(UIEvent.PointerShortClick, 
            onPointerShortClick);
    }
    private void showGameBanner(HeadBannerData data, RectTransform view)
    {
        if (data == null)
        {
            return;
        }
        ImageWidget image = view.GetComponent<ImageWidget>();
       
        PlatformPicManagerProxy.Instance.downloadGameIcon(data.iconURL, image); 
    }
    private void onPointerShortClick(PointerEventData go)
    {
        int index = this.main_mid.head_banner.dataIndex;
         List<HeadBannerData> gameList = TableBaseGameList.Instance.GetBannerList();
        int id = gameList[index].id;
      //  GameManager.checkDownloadOrStartGame(id, EnumGameType.Hall, -1, -1)
    }

    private void onHotGameInfoChangeHandler()
    {
        List<HeadBannerData> dataList = TableBaseGameList.Instance.GetBannerList();
        this.main_mid.hot_bg_image.ActiveLoadImage(false);
        this.main_mid.hot_group.gameObject.SetActive(true);
        this.main_mid.hot_no_text.gameObject.SetActive(dataList.Count== 0);

        this.main_mid.hot_group.SetCellData(dataList, (go,data,startIndex, index) =>
        {
            //  object gameIndex = data;
            Debug.Log("onHotGameInfoChangeHandler data=" + data + " index=" + index);
           
            var item = this.main_mid.hotGameItemArr[index+1];
            HeadBannerData getdata = (HeadBannerData)data;
            if (getdata == null)
            {
                return;
            }
            if (getdata != null)
            {
              
                item.game_name_text.text = "" + getdata.name;
                item.game_introduce_text.text = "" + getdata.GameSketch;
                PlatformPicManagerProxy.Instance.downloadGameIcon(getdata.iconURL, item.game_image);

            }
            item.bg_icon.AddEventListener(UIEvent.PointerClick, (evtData) =>
            {
                CommonView.showTopTips("点击了");
                Debug.Log("点击了");
                int dataId = getdata.id;
                UIViewManager.Instance.Open(UIViewEnum.Platform_Game_Rule_View, dataId);
            });

        });
    }
    private void closeFun(PointerEventData eve)
    {
        UIViewManager.Instance.Close(UIViewEnum.Platform_Global_Game_View);
    }
    private void reqFind()
    {
        this.main_mid.hot_group.gameObject.SetActive(false);
        this.main_mid.hot_no_text.gameObject.SetActive(false);

    }
}
