using FairyGUI;
using System.Collections.Generic;
using Tips;


[FGUIViewAttribute(UIViewEnum.ItemUseView, typeof(ItemUseView))]
public class ItemUseView : BaseView
{
    public override string PackageName => G_ItemUseView.PACKAGE_NAME;
    public override string ComponentName => G_ItemUseView.COMPONENT_NAME;

    private G_ItemUseView mainView;

    private CommonItemData mItemData;

    private TableView<ItemRender> mRewaradItems;

    private ItemRender mItemRender;

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


    public ItemUseView()
    {
        setViewAttribute(UIViewLayerType.Alert_box, UIViewEnum.ItemUseView, false);
    }

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.BagItemChange, OnBagItemChange },
    };

    private void OnBagItemChange(EventSysArgsBase argsBase)
    {
        RefreshNum();
        if (mAddSelectNum> mMaxNum)
        {
            SetSelectNum(1);
        }
        if (mMaxNum <= 0)
        {
            HideView();
        }
    }

    private void RefreshNum()
    {
        mMaxNum = (int)mItemData.CountImt;
        mainView.Text_count.text = Utility.Text.Format("拥有:{0}", mItemData.CountImt);
    }


    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mainView = contentPane as G_ItemUseView;
        closeButton = mainView.Button_close;
        mItemRender = BaseRender.Create<ItemRender>(mainView.goodItem);
        mRewaradItems = new TableView<ItemRender>(mainView.list_reward);
        mainView.Text_input.onChanged.Add(OnTextChange);
        //TODO挂机道具
        mainView.group_reward.visible = false;
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
        if (showArgs is CommonItemData itemDatas)
        {
            mItemData = itemDatas;
            mItemData.IsShowNum = false;
            mMaxNum = (int)mItemData.CountImt;
        }
        SetSelectNum(1);
        mainView.Text_input.text = 1.ToString();
        TryShowView();
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        base.OnButtonClick(clickedButton);
        if (clickedButton == mainView.Button_use)
        {
            var str = "";
            var num = mAddSelectNum;
            if (mItemData.GetIsFragmentItem && _mAddSelectNum < mItemData.GetFragmentSynthesisNum)
                num = 0;
            if (num <= 0)
            {
                str = "请先选择使用数量";
                if (mItemData.GetIsFragmentItem)
                {
                    str = "当前碎片不足以合成";
                }
            }
            if (!string.IsNullOrEmpty(str))
            {
                CommonViewUtils.ShowTopTips(str);
                return;
            }
            BagControl.Instance.OnUseItemReq(mItemData.UniqueId, mItemData.ItemId, num);
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
    private void TryShowView()
    {
        mItemRender.setData(mItemData);
        mainView.Text_name.text = mItemData.Name;
        var isNull = true;
        mainView.Text_count.text = Utility.Text.Format("拥有:{0}", mItemData.CountImt);
        mainView.Text_title.text = "批量使用";
        if (mItemData.GetIsHangUpItem)
        {
            var itemDatas = mItemData.GetUseRewardDatas;
            isNull = itemDatas == null || itemDatas.Count == 0;
            if (!isNull)
            {
                mRewaradItems.setDatas(itemDatas);
            }
        }
        else if (mItemData.GetIsFragmentItem)
        {
            mainView.Text_count.text = Utility.Text.Format("拥有碎片:{0}/{1}", mItemData.CountImt, mItemData.GetFragmentSynthesisNum);
            mainView.Text_title.text = "碎片合成";
        }
        mainView.list_reward.visible = !isNull;
    }

    protected override void DoShowAnimation() => OnShown();

    protected override void DoHideAnimation() => this.HideWindowImmediately();
}

