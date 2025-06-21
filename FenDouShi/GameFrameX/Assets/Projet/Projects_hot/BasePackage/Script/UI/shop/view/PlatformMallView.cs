using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static Mid_platform_mall_panel;
using UIWidget;

public enum ShopType
{
    Diamond = 1,	// 钻石
    Gold = 2,	// 金币
    YoCard = 3,	//优卡
    Blend = 4,	// 混合
}

public enum ShopFromType
{
    platform = 0,
    Game=1,
}
public class MailInfo{
    public ShopType shopType;
    public ShopFromType fromType;
    public Action succeedCallback;


}
public class PlatformMallView : BaseView
{
    //private string topTipsTimer = "PlatformMallView";
    private int m_fromType = 0;//--来源类型 0.平台 >0.游戏id
    private Action m_succeedCallback;//-从平台或游戏传入的充值兑换成功回调
    private MailInfo mailInfo;
   

    [SerializeField]
    private Mid_platform_mall_panel main_mid;

    public PlatformMallView()
    {

        this.viewName = "TopTipsView";
        this.loadOrders = new List<string>() { "BasePackage:platform_mall_panel" };
       //test@@@
        setViewAttribute(UIViewType.Pop_view, UIViewEnum.Platform_Mall_View, false);
         Debug.Log("@@@@ TopTipsView()");
    }
    protected override void onLoadUIEnd(string uiName, GameObject gameObject)
    {
    
        main_mid = gameObject.AddComponent<Mid_platform_mall_panel>();
        this.BindMonoTable(gameObject, main_mid);
        UITools.SetParentAndAlign(gameObject, this.container);

         addEvent();
        hidePoolUI();
        //showTopTips("选中倒计时 this.scanTime="..tostring(this.scanTime))
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
        if (msg != null)
        {
            mailInfo = (MailInfo)msg;
        }
        initView();
        addNotice();

        updateMallEnum(mailInfo.shopType);


    }
    private void addEvent()
    {
        this.main_mid.close_Image.AddEventListener(UIEvent.PointerClick, (PointerEventData eventData) =>
        {
            //test@@@
            UIViewManager.Instance.Close(UIViewEnum.Platform_Mall_View);

        });
        this.main_mid.blend_panel.gameObject.SetActive(false);
    }
    private void addNotice()
    {
        NoticeManager.Instance.AddNoticeLister(NoticeType.User_Init_Diamond_Money, onInitDiamondAndMoney);

        NoticeManager.Instance.AddNoticeLister(NoticeType.User_Update_Cash, updateCash);

        NoticeManager.Instance.AddNoticeLister(NoticeType.Mall_Update_Diamond, onUpdateDiamond);

        NoticeManager.Instance.AddNoticeLister(NoticeType.Mall_Update_Money, onUpdateMoney);
    }
    private void removeNotice()
    {
        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Init_Diamond_Money, this.onInitDiamondAndMoney);

        NoticeManager.Instance.RemoveNoticeLister(NoticeType.User_Update_Cash, updateCash);

        NoticeManager.Instance.RemoveNoticeLister(NoticeType.Mall_Update_Diamond,onUpdateDiamond);

        NoticeManager.Instance.RemoveNoticeLister(NoticeType.Mall_Update_Money, onUpdateMoney);
    }

    private IconWidget mallIcon;
    private PanelWidget mallPanel;
    private void updateMallEnum(ShopType mallEnum)
    {
        UserPlayerData userInfo = GameDataManager.Instance.userPlayerData;
        //downloadUserHead(this.currMallBaseData.head_url, this.main_mid.head_Icon);
        if (mallIcon != null)
        {
            mallIcon.ChangeIcon(0);
        }
        if (mallPanel!=null)
        {
            this.mallPanel.gameObject.SetActive(false);
        }
        if(mallEnum == ShopType.Diamond)
        {
            TableBaseRechargeShop.Instance.InitDiamondConfig();
            List<ShopDiamondConfig> dataList = TableBaseRechargeShop.Instance.dataDiamond;
            this.mallIcon = this.main_mid.diamond_Icon;
            this.mallPanel = this.main_mid.diamond_Panel;
            Logger.PrintDebug("1111@@@@@ dataList.count="+ dataList.Count);

            this.main_mid.diamond_GridRecycleScrollPanel.SetCellDataObjet<ShopDiamondConfig>(dataList, onSetDiamond, true);
        }else if(mallEnum == ShopType.Gold)
        {
            this.mallIcon = this.main_mid.gold_Icon;
            this.mallPanel = this.main_mid.gold_Panel;
            TableBaseRechargeShop.Instance.InitGoldConfig();
            List<ShopGoldConfig> dataList = TableBaseRechargeShop.Instance.dataGold;
          //  PlayFabUserModule.getStoreCatalogData();
            Logger.PrintDebug("2222@@@@@ dataList.count=" + dataList.Count);
           // List<object> p_dataList = dataList as List<object>;
            //Logger.PrintDebug("2222@@@@@@@@@@@@@SetCellData22222 p_dataList.count=" + p_dataList.Count);

            this.main_mid.gold_GridRecycleScrollPanel.SetCellDataObjet<ShopGoldConfig>(dataList, onSetGold, true);
        }

        this.mallIcon.ChangeIcon(1);

        this.mallPanel.gameObject.SetActive(true);

       
        this.onInitDiamondAndMoney();
        this.main_mid.top_packet_Text.text = "0";
        this.topGoldEffectTimer();
    }
    private void onInitDiamondAndMoney()
    {
        UserPlayerData userInfo= GameDataManager.Instance.userPlayerData;
        oldValueDiamond = userInfo.Diamond;
        oldValueGold = userInfo.Gold;
        string money = CommonView.NumberToShow(userInfo.Gold);
        string diamond = CommonView.NumberToShow(userInfo.Diamond);
        this.main_mid.top_gold_Text.text = (money);
        this.main_mid.top_diamond_Text.text = (diamond);
    }
    private void topGoldEffectTimer()
    {
        GlobalTimeManager.Instance.timerController.AddTimer(
        "goldEffectTime",
        2500,
        -1,
       (int te) => {

           this.main_mid.goldEffect.Play();
        });

        GlobalTimeManager.Instance.timerController.AddTimer(
       "diamondEffectTime",
       2900,
       -1,
      (int te) => {

          this.main_mid.diamondEffect.Play();
      });


        GlobalTimeManager.Instance.timerController.AddTimer(
       "ucardEffectTime",
       3500,
       -1,
      (int te) => {

          this.main_mid.packetEffect.Play();
      });
    }
    private void onSetDiamond(GameObject go, object data, int index)
    {
        Diamondcell item = this.main_mid.diamondcellArr[index];
        ShopDiamondConfig config = (ShopDiamondConfig)data;
        exchangeNum(item.cost_Text, config.cash);
        exchangeNum(item.return_num_Text, config.item);
        if (config.id>6)
        {
            item.go_Icon.ChangeIcon(5);
        }
        else
        {
            item.go_Icon.ChangeIcon(config.id-1);
        }

        if (config.id > 3)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("TimeDiamondRandom" + config.id,
            800 * config.id,
            -1,
            (int te) =>
            {
                item.mallEffect.Play();
            }
            ) ;
        }
        item.go_circle_Button.AddEventListener(UIEvent.PointerClick, (PointerEventData eventData) =>
        {
#if UNITY_EDITOR
            CommonView.showTopTips("UNITY_EDITOR不能充值");
            #endif
            Action succeedCallback;
            if (m_fromType == 0)
            {
                succeedCallback = () =>
                {
                 //   UIViewManager.Instance.Open(UIViewEnum.Platform_Mall_Tip_View,{enum = 3,successData = data})
                };
            }

           // --充值
           // RechargeManager.recharge("0", LoginDataProxy.token, tostring(data.id), tostring(LoginDataProxy.playerId), "1", succeedCallback)
        });
    }
    private void onSetGold(GameObject go, object _data, int index)
    {
        Goldcell item = this.main_mid.goldcellArr[index];
        ShopGoldConfig data = (ShopGoldConfig)_data;
        exchangeNum(item.cost_Text, data.src_item);
        exchangeNum(item.return_num_Text, data.dest_item);
        if (data.id > 6)
        {
            item.go_Icon.ChangeIcon(5);
        }
        else
        {
            item.go_Icon.ChangeIcon(data.id - 1);
        }

        if (data.id > 3)
        {
            GlobalTimeManager.Instance.timerController.AddTimer("TimeRandom" + data.id,
             UnityEngine.Random.Range(500,800)*index,
            -1,
            (int te) =>
            {
                item.mallEffect.Play();
            }
            );
        }
        item.go_circle_Button.AddEventListener(UIEvent.PointerClick, (PointerEventData eventData) =>
        {
        UserPlayerData playdata = GameDataManager.Instance.userPlayerData;
        if (data.src_item > playdata.Diamond)
        {
                CommonView.showVerifyMsg("钻石不足", "是否立即充值", "去充值?", (int te) => {

                this.updateMallEnum(ShopType.Diamond);

            }, "取消", null, false);
        }
        else
        {
            string tip = string.Format("<color=#000000FF>{0}</color><color=#f34b4bFF>{1}</color><color=#000000FF>{2}</color><color=#f34b4bFF>{3}</color><color=#000000FF>{4}</color>",
               "确定用 ", data.src_item + "钻石 ", "兑换 ", data.dest_item + "金币 ", "吗?");
                CommonView.showVerifyMsg("兑换金币提示", tip, "取消", null, "确定", (int te) =>{
                ShopModule.BuyGoldStoreItem(data.id - 1);//--1 开始
            });
         }
            // --充值
            // RechargeManager.recharge("0", LoginDataProxy.token, tostring(data.id), tostring(LoginDataProxy.playerId), "1", succeedCallback)
        });
    }
    //  --价格汉字转换
    private void exchangeNum(TextWidget Text, int num) {
        if (num >= 10000)
        {
            Text.text = Mathf.Floor(num / 10000) + "万";
        }
        else
        {
            Text.text = num.ToString();
        }
        
    }
    private void hidePoolUI()
    {
        this.main_mid.gold_icon_pool.gameObject.SetActive(false);
        this.main_mid.diamond_icon_pool.gameObject.SetActive(false);
        this.main_mid.cost_icon_pool.gameObject.SetActive(false);
    }

    //打开界面时初始化
    private void initView()
    {
        this.main_mid.diamond_Panel.gameObject.SetActive(false);
        this.main_mid.gold_Panel.gameObject.SetActive(false);
        this.main_mid.yo_card_Panel.gameObject.SetActive(false);
        //this.main_mid.noClick_Image.gameObject:SetActive(false)
        this.main_mid.top_yo_card_Text.text = "0";
        this.main_mid.top_gold_Text.text = "0";
        this.main_mid.top_diamond_Text.text = "0";
    }

    private int oldValueDiamond = 0;
    private int oldValueGold = 0;
    private void onInitDiamondAndMoney(string noticeType, BaseNotice notice)
    {
        UserPlayerData userInfo = GameDataManager.Instance.userPlayerData;
        if (userInfo == null) {
            return;
        }
        oldValueDiamond = userInfo.Diamond;
        oldValueGold = userInfo.Gold;

        string money = ""+ CommonView.NumberToShow(userInfo.Gold);

        string diamond = "" + CommonView.NumberToShow(userInfo.Diamond);


        this.main_mid.top_gold_Text.text = money;
        this.main_mid.top_diamond_Text.text = diamond;
    }

    private void updateCash(string noticeType, BaseNotice notice)
    {
        this.main_mid.top_packet_Text.text = "0";
    }

    //--更新顶部钻石
    private void onUpdateDiamond(string noticeType, BaseNotice notice)
    {
       
        UserPlayerData currUserData = GameDataManager.Instance.userPlayerData;
        if (currUserData.Diamond > 10000)
        {
            onInitDiamondAndMoney("", null);
            return;
        }
        int count = 0;
        int maxCount = 60;
        int oldValue = this.oldValueDiamond;

        int newValue = currUserData.Diamond;
        this.main_mid.top_gold_Text.text = "" + currUserData.Gold;
        GlobalTimeManager.Instance.timerController.AddTimer(
            "PlatformMallUpdateDiamond",
            -1,
            maxCount,
           (int te) =>
           {
               count = count + 1;
               this.main_mid.top_diamond_Text.text = ""+Mathf.Floor((newValue - oldValue) * count / maxCount) + oldValue;

           });
    }

   // --更新顶部金币
    private void onUpdateMoney(string noticeType, BaseNotice notice)
    {

        UserPlayerData currUserData = GameDataManager.Instance.userPlayerData;
        if (currUserData.Gold > 10000)
        {
            onInitDiamondAndMoney("", null);
            return;
        }
        int count = 0;
        int maxCount = 60;
        int oldValue = this.oldValueGold;

        int newValue = currUserData.Gold;
        this.main_mid.top_diamond_Text.text = "" + currUserData.Diamond;
        GlobalTimeManager.Instance.timerController.AddTimer(
            "PlatformMallUpdateGold",
            -1,
            maxCount,
           (int te) =>
           {
               count = count + 1;
               int curCount = count - 10;
               if (curCount < 0)
                   curCount = 0;
               this.main_mid.top_gold_Text.text =""+ Mathf.Floor((newValue - oldValue) * curCount / (maxCount - 10)) + oldValue;
           });
    }





    private void onHideView(int countNum)
    {
        if (this.main_mid)
        {
            //test@@@
            // UIViewManager.Instance.Close(UIViewEnum.TopTips_View);
        }
    }

    protected override void onClose()
    {
        base.onClose();
    }
}
