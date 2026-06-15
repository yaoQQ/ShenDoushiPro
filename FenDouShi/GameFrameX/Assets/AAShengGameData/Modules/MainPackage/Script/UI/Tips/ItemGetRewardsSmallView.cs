//using Cysharp.Threading.Tasks;
using FairyGUI;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemGetRewardsSmallView, typeof(ItemGetRewardsSmallView))]
public class ItemGetRewardsSmallView : BaseView
{
    public override string PackageName => G_ItemGetRewardSmallView.PACKAGE_NAME;
    public override string ComponentName => G_ItemGetRewardSmallView.COMPONENT_NAME;

    private G_ItemGetRewardSmallView mainView;

    private List<CommonItemData> mItemDatas;

    protected override Dictionary<EEventType, OnEventLister> EventList => new() {

        {EEventType.BagItemChange_Show,OnAddItemList },
    };

    public ItemGetRewardsSmallView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemGetRewardsSmallView, false);
    }

    private void OnAddItemList(EventSysArgsBase argsBase)
    {
        switch (argsBase)
        {
            case EventSysArgs<List<CommonItemData>> args:
                {
                    mItemDatas ??= new List<CommonItemData>();
                    mItemDatas.AddRange(args.args1);
                    TryShowView();
                    break;
                }
        }

    }



    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemGetRewardSmallView;
    }


    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is List<CommonItemData> itemDatas)
        {
            mItemDatas = itemDatas;
        }
        TryShowView();
    }

    private void TryShowView()
    {
        Clear();
        if (mItemDatas is not { Count: > 0 }) return;
        GlobalTimeManager.Instance.timerController.AddTimer(this, 500, -1, OnPopItem);
        OnPopItem(0);
    }

    private void Clear()
    {
        GlobalTimeManager.Instance.timerController.RemoveTimerByKey(this);
    }

    private void OnPopItem(int num)
    {
        if (mItemDatas is not { Count: > 0 })
        {
            Clear();
            UIViewManager.Instance.Hide(getViewEnum());
            return;
        }
        var item = mItemDatas[0];
        mItemDatas.Remove(item);
        InsertItem(item);
    }

    public void InsertItem(CommonItemData CommonItemData)
    {
        var popItem = BaseRender.CreateTest<GetRewardSmallItem>(mainView.viewParent);
        popItem.setData(CommonItemData);
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
}

