using FairyGUI;
using System.Collections.Generic;
using System.Linq;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemBoxRandomTipsView, typeof(ItemBoxRandomTipsView))]
public class ItemBoxRandomTipsView : BaseView
{
    public override string PackageName => G_ItemBoxRandomTipsView.PACKAGE_NAME;
    public override string ComponentName => G_ItemBoxRandomTipsView.COMPONENT_NAME;

    private G_ItemBoxRandomTipsView mainView;

    private TableView<ItemTipsRateItem> tipsRateView;

    private TableView<ItemRender> list_reward;

    private CommonItemData CommonItemData = null;

    private ItemRender mGoodItemRender;

    private List<ItemTipsRateData> boxDatas;

    private bool isShowGetWay = false;


    public ItemBoxRandomTipsView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemBoxRandomTipsView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemBoxRandomTipsView;
        closeButton = mainView.Button_close;
        mGoodItemRender = BaseRender.Create<ItemRender>(mainView.goodItem);
        tipsRateView = new TableView<ItemTipsRateItem>(mainView.list_rateShow);
        list_reward = new TableView<ItemRender>(mainView.list_reward);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mainView.Button_getWay)
        {
            isShowGetWay = !isShowGetWay;
            mainView.group_rate.visible = isShowGetWay;
        }
        else if (clickedButton == mainView.Button_rareClose)
        {
            isShowGetWay = false;
            mainView.group_rate.visible = isShowGetWay;
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        isShowGetWay = false;
        if (showArgs is CommonItemData itemData)
        {
            CommonItemData = itemData;
        }
        else if (showArgs is int itemId)
        {
            CommonItemData = new CommonItemData(itemId);
        }
        else if (showArgs is TipsOpenParam openParam)
        {
            CommonItemData = new CommonItemData(openParam.ItemId);
            isShowGetWay = openParam.IsShowGetWay;
        }
        boxDatas = CommonItemData.GetBoxRewardDatas;
        boxDatas ??= new List<ItemTipsRateData>();
        mainView.group_rate.visible = false;
        mainView.group_getWay.visible = CommonItemData.GetIsRandomBoxGiftType;
        RefreshView();
    }


    private void RefreshView()
    {
        mainView.Text_name.text = CommonItemData.Name;
        mainView.Text_num.text = Utility.Text.Format("拥有:{0}", CommonItemData.CountImt);
        CommonItemData.IsShowNum = false;
        mGoodItemRender.setData(CommonItemData);
        mainView.Text_desc.text = CommonItemData.Desc;
        tipsRateView.setDatas(boxDatas);
        var itemDatas = boxDatas.Select(s => new CommonItemData(s.ItemId, s.ItemCount)).ToList();
        list_reward.setDatas(itemDatas);
        if (CommonItemData.GetIsRandomOneBoxType)
        {
            mainView.Text_desc.text = "打开后可随机获得以下物品中的一项：";
        }
        else if (CommonItemData.GetIsRandomMultiBoxType)
        {
            mainView.Text_desc.text = "打开后可随机获得以下物品：";
        }
        else if (CommonItemData.GetIsAllGetBoxType)
        {
            mainView.Text_desc.text = "打开宝箱后可选择以下物品：";
        }
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
}

