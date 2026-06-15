using FairyGUI;
using System.Collections.Generic;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemTipsView, typeof(ItemTipsView))]
public class ItemTipsView : BaseView
{
    public override string PackageName => G_ItemTipsView.PACKAGE_NAME;
    public override string ComponentName => G_ItemTipsView.COMPONENT_NAME;

    private G_ItemTipsView mainView;

    private TableView<ItemTipsGetWayRenderer> getWayRenderer;

    private List<ItemTipsGetWayData> itemTipsGetWayDatas;

    private CommonItemData CommonItemData;

    private bool mSetIsShowGetWay = false;

    private bool SetIsShowGetWay
    {
        get => mSetIsShowGetWay;
        set
        {
            mSetIsShowGetWay = value;
            mainView.List_getWay.visible = mSetIsShowGetWay;
            getWayRenderer.ResizeToFit();
        }
    }

    public ItemTipsView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemTipsView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemTipsView;
        closeButton = mainView.viewMask;
        getWayRenderer = new TableView<ItemTipsGetWayRenderer>(mainView.List_getWay);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mainView.Button_getWay)
        {
            SetIsShowGetWay = !SetIsShowGetWay;
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        var mItemId = 0;
        if (showArgs is int itemId)
        {
            mItemId = itemId;
            SetIsShowGetWay = false;
        }
        else if (showArgs is TipsOpenParam openParam)
        {
            mItemId = openParam.ItemId;
            SetIsShowGetWay = openParam.IsShowGetWay;
        }
        CommonItemData ??= new CommonItemData();
        CommonItemData.SetItemId(mItemId);
        RefreshView();
    }

    private void RefreshView()
    {
        mainView.Text_name.text = CommonItemData.Name;
        mainView.Text_num.text = Utility.Text.Format("с╣сп:{0}", CommonItemData.CountImt);
        mainView.Image_icon.url = CommonItemData.IconUrl;
        mainView.Text_desc.text = CommonItemData.Desc;
        var mGetWayDatas = GetGetWayParam();
        getWayRenderer.setDatas(mGetWayDatas);
        mainView.group_getWay.visible = mGetWayDatas?.Count > 0;
        getWayRenderer.ResizeToFit();
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();


    private List<ItemTipsGetWayData> GetGetWayParam()
    {
        var temp = CommonItemData.GetGetWay;
        if (temp != null)
        {
            itemTipsGetWayDatas = temp;
        }
        else
        {
            if (itemTipsGetWayDatas == null) { itemTipsGetWayDatas = new List<ItemTipsGetWayData>(); }
            else { itemTipsGetWayDatas.Clear(); }
        }
        return itemTipsGetWayDatas;
    }
}

