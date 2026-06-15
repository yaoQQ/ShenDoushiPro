using FairyGUI;
using System;
using System.Collections.Generic;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemSelectRewardView, typeof(ItemSelectRewardView))]
public class ItemSelectRewardView : BaseView
{
    public override string PackageName => G_ItemSelectRewardView.PACKAGE_NAME;
    public override string ComponentName => G_ItemSelectRewardView.COMPONENT_NAME;

    private G_ItemSelectRewardView mainView;

    private List<CommonItemData> mItemDatas;

    private CommonItemData mData;

    private TableView<ItemSelectRewardItem> mItemRender;

    private int mMaxNum;

    private int _mAddSelectNum;

    private int mAddSelectNum
    {
        get => _mAddSelectNum;
        set
        {
            _mAddSelectNum += value;
            SetSelectNum(_mAddSelectNum);
        }
    }

    private void SetSelectNum(int value)
    {
        if (value <= 0)
        {
            _mAddSelectNum = 1;
        }
        else if (value > mMaxNum)
        {
            _mAddSelectNum = mMaxNum;
        }
        else
        {
            _mAddSelectNum = value;
        }
        mainView.Text_input.text = _mAddSelectNum.ToString();
    }


    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.TipsRewardSelectEvent, OnItemSelect },
        { EEventType.BagItemChange, OnBagItemChange },

    };

    private void OnBagItemChange(EventSysArgsBase argsBase)
    {
        mItemRender.setDatas(mItemDatas);
        RefreshNum();
        if (mAddSelectNum > mMaxNum)
        {
            SetSelectNum(1);
        }
        if (mMaxNum <= 0)
        {
            HideView();
        }
    }

    private void OnItemSelect(EventSysArgsBase argsBase)
    {
        if (argsBase is not EventSysArgs<int> arg) return;
        if (mItemDatas != null)
        {
            mItemRender.setSelectIndex(arg.args1);
        }
    }

    public ItemSelectRewardView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemSelectRewardView, false);
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mainView.Button_use)
        {
            var str = "请先选择物品";
            var selectIdx = mItemRender.mSelectIndex;
            if (selectIdx <= 0 || mAddSelectNum <= 0)
            {
                CommonViewUtils.ShowTopTips(str);
                return;
            }
            BagControl.Instance.OnUseItemReq(mData.UniqueId, mData.ItemId, mAddSelectNum, new int[] { selectIdx, mAddSelectNum });
        }
        else if (clickedButton == mainView.Button_add)
        {
            mAddSelectNum = 1;
        }
        else if (clickedButton == mainView.Button_sub)
        {
            mAddSelectNum = -1;
        }
        else if (clickedButton == mainView.Button_sub10)
        {
            mAddSelectNum = -10;
        }
        else if (clickedButton == mainView.Button_add10)
        {
            mAddSelectNum = 10;
        }
        else if (clickedButton == mainView.Button_max)
        {
            mAddSelectNum = mMaxNum;
        }
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemSelectRewardView;
        closeButton = mainView.Button_close;
        mItemRender = new TableView<ItemSelectRewardItem>(mainView.list_reward);
        mItemRender.SetTouchEnable(false);
        mainView.Text_input.onChanged.Add(OnTextChange);
    }

    private void OnTextChange(EventContext context)
    {
        var gTextInput = context.sender as GTextInput;
        if (int.TryParse(gTextInput.text, out var num))
        {
            SetSelectNum(num);
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        if (showArgs is CommonItemData itemData)
        {
            mData = itemData;
        }
        RefreshNum();
        SetSelectNum(1);
        mainView.Text_input.text = 1.ToString();
        mItemDatas = mData.GetUseRewardDatas;
        TryShowView();
    }

    private void RefreshNum()
    {
        mMaxNum = (int)mData.CountImt;
        mainView.Text_remain.text = Utility.Text.Format("剩余：{0}", mMaxNum);
    }

    private void TryShowView()
    {
        mItemRender.setDatas(mItemDatas);
        mItemRender.setSelectIndex(-1);
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
}

