

using common;
using FairyGUI;
public class ItemRenderTest : BaseRender
{
    public new GComponent mRoot => base.mRoot.asCom;

    /// <summary>
    /// 道具来源
    /// </summary>
    private eItemSource _mItemSource;

    public override string mPackageName => G_ItemRender.PACKAGE_NAME;
    public override string mComponentName => G_ItemRender.COMPONENT_NAME;

    private BaseRender _mItemRenderer;
    private GoodItemRenderer _goodItemRenderer;
    private GoodItemRenderer2 _goodItemRenderer2;
    private FragmentItemRender _fragmentItemRender;

    private bool _isSelect = false;

    protected override void dataChanged()
    {
        CommonItemData data = null;
        if (mData is CommonItemData commonItemData)
        {
            data = commonItemData;
        }
        else
        {
            return;
        }
        var isNull = data == null || data.IsEmptyItem;
        if (isNull)
        {
            return;
        }
        _mItemSource = data.mItemSource;
        var isInBag = _mItemSource == eItemSource.Bag;
        if (isInBag && data.GetIsFragmentItem)
        {
            _fragmentItemRender = Create<FragmentItemRender>(mRoot);
            data.IsEnableClick = false;
            _mItemRenderer = _fragmentItemRender;
        }
        else if (_mItemSource == eItemSource.RewardShow)
        {
            _goodItemRenderer2 = Create<GoodItemRenderer2>(mRoot);
            _mItemRenderer = _goodItemRenderer2;
        }
        else
        {
            _goodItemRenderer = Create<GoodItemRenderer>(mRoot);
            _mItemRenderer = _goodItemRenderer;
        }
        _mItemRenderer.setData(data);
        SetSelectState(_isSelect);
    }
    public override void SetSelectState(bool state)
    {
        var data = (CommonItemData)mData;
        if (data is { IsEmptyItem: true })
        {
            return;
        }
        if (_mItemSource != eItemSource.Bag)
        {
            return;
        }
        _isSelect = state;
        _mItemRenderer?.SetSelectState(state);
    }
}

