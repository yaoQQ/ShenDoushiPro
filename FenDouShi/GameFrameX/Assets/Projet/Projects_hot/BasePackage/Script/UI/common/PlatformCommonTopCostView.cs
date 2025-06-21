using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UIWidget;

public class PlatformCommonTopCostView : BaseView
{



    [SerializeField]
    private Mid_common_top_cost_panel main_mid;

    private int oldValueGold = 0;
    private int oldValueDiamond = 0;
    public PlatformCommonTopCostView()
    {

        this.viewName = "PlatformCommonTopCostView";
        this.loadOrders = new List<string>() { "BasePackage:common_top_cost_panel" };
        //test@@@
        setViewAttribute(UIViewType.Main_view, UIViewEnum.Platform_Top_Cost_View, false);
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
        Logger.PrintDebug("onLoadUIEnd complte!! uiName=" + uiName);

        main_mid = gameObject.AddComponent<Mid_common_top_cost_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

        addEvent();
        addNotice();
    }
 
    protected override void onShowHandler(object msg)
    {
        Logger.PrintDebug("onShowHandler() 显示界面：msg=" + msg);
        GameObject go = this.getViewGO();
        if (go == null)
        {
            Logger.PrintError("go == null");
            return;
        }
        go.transform.SetAsLastSibling();
    }
    private void addEvent()
    {
        this.main_mid.top_gold_Button.AddEventListener(UIEvent.PointerClick, onBtnGoldEvent);
        this.main_mid.top_diamond_Button.AddEventListener(UIEvent.PointerClick, onBtnDiamondEvent);
    }
    private void addNotice()
    {
        //test
       NoticeManager.Instance.AddNoticeLister(NoticeType.User_Update_UserBaseInfo, updateDiamondAndMoney);
       
        NoticeManager.Instance.AddNoticeLister(NoticeType.User_Init_Diamond_Money, updateDiamondAndMoney);
        NoticeManager.Instance.AddNoticeLister(NoticeType.User_Update_Diamond, updateDiamondAndMoney);
        NoticeManager.Instance.AddNoticeLister(NoticeType.User_Update_Cash,updateDiamondAndMoney);
      //  NoticeManager.Instance.AddNoticeLister(NoticeType.User_Update_Money, this.updateDiamondAndMoney)
        NoticeManager.Instance.AddNoticeLister(NoticeType.Mall_Update_Money, onUpdateMoney);
    }
    private void removeNotice()
    {
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Update_UserBaseInfo, updateDiamondAndMoney);
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Init_Diamond_Money, updateDiamondAndMoney);
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Update_Diamond, updateDiamondAndMoney);

        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Update_Cash, updateDiamondAndMoney);
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.Mall_Update_Money, onUpdateMoney);
    }
    private void onUpdateMoney(string noticeType, BaseNotice notice)
    {
        int count = 0;
        int maxCount = 60;
        int oldValue = this.oldValueGold;

        
        UserPlayerData currUserData = GameDataManager.Instance.userPlayerData;

        if (currUserData.Gold > 10000)
        {
            updateDiamondAndMoney("", null);
            return;
        }
        int newValue = currUserData.Gold;
        this.main_mid.top_diamond_Text.text = "" + currUserData.Diamond;
        GlobalTimeManager.Instance.timerController.AddTimer(
            "PlatformTopUpdateGold",
            -1,
            maxCount,
           (int te) =>
           {
               count = count + 1;
               int curCount = count - 10;

               if (curCount < 0)
                   curCount = 0;
               
               this.main_mid.top_gold_Text.text = "" + Mathf.Floor((newValue - oldValue) * curCount / (maxCount - 10)) + oldValue;

           });
    }
    private void updateDiamondAndMoney(string noticeType, BaseNotice notice)
    {
          UserPlayerData currBaseData = GameDataManager.Instance.userPlayerData;
        if (currBaseData == null)
        {
            return;
        }

        oldValueGold = currBaseData.Gold;
        oldValueDiamond = currBaseData.Diamond;
        int cash = 0;
        this.main_mid.top_gold_Text.text = "" + oldValueGold;
        this.main_mid.top_diamond_Text.text = "" + oldValueDiamond;

        this.main_mid.top_packet_Text.text = "" + cash;
        //local subsidyCount = PlatformUserProxy:GetInstance():getSubsidyCountDailyCount();

    }
   
    private  void updataUserHead()
    {
        this.main_mid.press_Image.AddEventListener(UIEvent.PointerClick, (PointerEventData point) =>
        {
            //test@@@
            // UIViewManager.Instance.Open(UIViewEnum.Personal_Change_Info_View);
        });
    }
    private void topGoldEffectTimer()
    {
        GlobalTimeManager.Instance.timerController.AddTimer(
             "goldRedBagTime",
             2500,
             -1,
             (int te) => {
                 this.main_mid.goldEffect.Play();
             });
        GlobalTimeManager.Instance.timerController.AddTimer(
            "diamondRedBagTime",
            2900,
            -1,
            (int te) => {
                this.main_mid.diamondEffect.Play();
            });
        GlobalTimeManager.Instance.timerController.AddTimer(
          "ucardRedBagTime",
          3500,
          -1,
          (int te) => {
              this.main_mid.packetEffect.Play();
          });
    }
 
    private void onBtnDiamondEvent(PointerEventData point)
    {
        // --RechargeManager.openShop(1, 0)
        // --临时

        // RechargeManager.openShop(2, 0);
        ShopModule.getStoreMoneyCatalogData();
    }
    private void onBtnGoldEvent(PointerEventData point)
    {
        // RechargeManager.openShop(2, 0);
        ShopModule.getStoreMoneyCatalogData();
    }
    protected override void onClose()
    {
        removeNotice();
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey("ucardRedBagTime");
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey("diamondRedBagTime");
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey("goldRedBagTime");
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey("PlatformTopUpdateGold");
    }
}
