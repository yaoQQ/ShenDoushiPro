using Bag;
using common;
using FairyGUI;
using System.Collections.Generic;

[FGUIViewAttribute(UIViewEnum.BagView, typeof(BagView))]
public class BagView : BaseView
{
    public override string PackageName => G_BagView.PACKAGE_NAME;
    public override string ComponentName => G_BagView.COMPONENT_NAME;

    private List<CommonItemData> goodsItemDatas;
    private ItemRender goodItemRenderer;
    private G_BagView mainView;
    private List<BagTableItemData> bagTableDatas;
    private eItemRenderType selectRenderType = eItemRenderType.Null;
    private BagTipsCompoment bagTipsView;

    private TableView<ItemRenderTest> mGoodTabeView;

    private BagSelectionView bagSelectionView;

    private List<List<BagSelectionItemData>> bagSelectionItemDatas = new();


    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "背包",
        //icon = "ui://Common/Gm/icon_gm",
        helpId = 10000,
        currencyId = 0,
    };
    protected override LeftTabData[] mleftTabDatas => new LeftTabData[]
       {
             new LeftTabData() { tabName = "道具", icon = UIHelper.GetFguiUrl("Bag","1tag_icon_daoju"), select = true,data =eItemRenderType.BagItem },
             new LeftTabData() { tabName = "碎片", icon = UIHelper.GetFguiUrl("Bag","1tag_icon_suipian"), select = false,data=eItemRenderType.FragmentList },
             //new LeftTabData() { tabName = "all", icon = UIHelper.GetFguiUrl("Bag","1tag_icon_xingming"), select = false,data=eItemRenderType.Normal },
       };


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.BagItemChange, OnBagItemChange },
        { EEventType.BagSelectChangeEvent, OnBagSelectChangeEvent },
    };


    private void OnBagSelectChangeEvent(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<int, List<int>> args) return;
        var index = args.args1;
        var arg0 = args.args2;
        //全选
        tempData ??= BagControl.Instance.Model.GetItemDatas(selectRenderType);
        var cfgCnt = bagSelectionItemDatas[index].Count;
        var isAll = arg0.Count >= cfgCnt || arg0.Contains((int)eBagSelectType.All) || arg0.Count <= 0;
        if (isAll)
        {
            RefreshBagView_Logic(tempData);
        }
        else
        {
            var isBox = arg0.Contains((int)eBagSelectType.Box);
            var isNonBox = arg0.Contains((int)eBagSelectType.NonBox);
            var mData = new List<CommonItemData>();
            foreach (var itemData in tempData)
            {
                if (selectRenderType == eItemRenderType.FragmentList)
                {
                    if (itemData.GetIsBoxItem && isBox || !itemData.GetIsBoxItem && isNonBox)
                    {
                        mData.Add(itemData);
                    }
                    //  1  非宝箱碎片    2 宝箱碎片 
                }
                else if (selectRenderType == eItemRenderType.BagItem)
                {
                    if (itemData.GetIsBoxItem && isBox || !itemData.GetIsBoxItem && isNonBox)
                    {
                        mData.Add(itemData);
                    }
                    //1 非宝箱道具和   2宝箱道具
                }
            }
            RefreshBagView_Logic(mData);
        }
    }


    private void OnBagItemChange(EventSysArgsBase argsBase)
    {
        switch (argsBase)
        {
            case null:
                return;
            case EventSysArgs<eBagType> args:
                {
                    if (args.args1 == eBagType.Material)
                    {
                        if (selectRenderType != eItemRenderType.Null)
                        {
                            RefreshBagView();
                        }
                    }
                    break;
                }
        }
    }

    public BagView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.BagView, false);
        BagBinder.BindAll();
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_BagView;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        mainView.List_tabs.defaultItem = G_BagTabItem.URL;

        //背包道具列表
        mainView.list_bag.defaultItem = G_ItemRender.URL;
        mainView.list_bag.itemProvider = GetListItemResource;

        mGoodTabeView = new(mainView.list_bag);
        mGoodTabeView.setClickCallBack(OnClickBagItem);
        //初始化左侧面板
        bagTipsView = BagTipsCompoment.Create<BagTipsCompoment>(mainView.bagTipsCompoment);
        bagTipsView.Hide();
        bagSelectionView = BaseRender.Create<BagSelectionView>(mainView.bagSelectView);
        bagSelectionView.Hide();
    }

    //换个写法测试下
    string GetListItemResource(int index)
    {
        var allData = mGoodTabeView.mDataList;
        if (allData == null || allData.Count - 1 < index) return G_ItemRender.URL;
        var data = allData[index] as CommonItemData;
        if (data != null)
        {
            var _mItemSource = data.mItemSource;
            var isInBag = _mItemSource == eItemSource.Bag;
            if (isInBag && data.GetIsFragmentItem)
            {
                return G_FragmentItemRender.URL;
            }
            else if (_mItemSource == eItemSource.RewardShow)
            {
                return G_CommonGoodItem2.URL;
            }
            else
            {
                return G_CommonGoodItem.URL;
            }
        }
        return G_ItemRender.URL;
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mainView.Button_select)
        {
            bagSelectionView.Show();
        }
        else if (clickedButton == mainView.Button_decompose)
        {
            BagControl.Instance.OnBagCombineBatchReq();
        }
    }

    private void OnClickBagItem(object data, int index)
    {
        RefreshRightContent((CommonItemData)data);
    }

    private void RefreshRightContent(CommonItemData itemData)
    {
        if (bagTipsView == null) return;
        bagTipsView.Show();
        bagTipsView.setData(itemData);
    }

    protected override void OnShown()
    {
        base.OnShown();
        mleftTabCompoment.setSelectIndex(0);
    }

    private List<CommonItemData> tempData;


    private void RefreshBagView_Logic(List<CommonItemData> allData)
    {
        goodsItemDatas = allData ?? new List<CommonItemData>();
        //sort
        var isNull = goodsItemDatas.Count <= 0;
        mainView.group_empty.visible = isNull;
        mainView.group_button.visible = !isNull;
        mainView.list_bag.visible = !isNull;
        if (!isNull)
        {
            goodsItemDatas.ForEach(itemData =>
            {
                itemData.mItemSource = eItemSource.Bag;
                itemData.ClientIsLock = false;
                itemData.IsShowNum = true;
            });
        }
        else
        {
            bagTipsView.Hide();
        }
        var lineSpace = selectRenderType == eItemRenderType.FragmentList ? 45 : 20;
        mGoodTabeView.SetLineGap(lineSpace);
        mGoodTabeView.setDatas(goodsItemDatas);
        mGoodTabeView.setMaxNum(500);
        var mSelectIdx = 0;
        mGoodTabeView.setSelectIndex(mSelectIdx);
        if (!isNull)
        {
            RefreshRightContent(goodsItemDatas[mSelectIdx]);
        }
    }

    private void RefreshBagView(int arg1 = 0)
    {
        tempData = BagControl.Instance.Model.GetItemDatas(selectRenderType);
        RefreshBagView_Logic(tempData);
    }



    protected override void selectLeftTab(LeftTabData data)
    {
        mGoodTabeView.ScrollTop();
        selectRenderType = (eItemRenderType)data.data;
        RefreshBagView();
        bagSelectionItemDatas.Clear();
        bagSelectionItemDatas.Add(new List<BagSelectionItemData>() {
            new BagSelectionItemData() { IconUrl = UIHelper.GetFguiUrl("Bag","beibao_icon_dj"),Type = eBagSelectType.NonBox },
            new BagSelectionItemData() {IconUrl = UIHelper.GetFguiUrl("Bag", "beibao_icon_bx"),Type = eBagSelectType.Box},
        });
        bagSelectionView.setData(bagSelectionItemDatas);
        bagSelectionView?.Hide();
    }
}

