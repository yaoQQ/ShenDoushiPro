using FairyGUI;
using Tips;

public class ItemSelectRewardItem : BaseRender
{
    public new G_ItemSelectRewardItem mRoot
    {
        get { return (G_ItemSelectRewardItem)base.mRoot; }
    }

    public override string mPackageName => G_ItemSelectRewardItem.PACKAGE_NAME;
    public override string mComponentName => G_ItemSelectRewardItem.COMPONENT_NAME;

    private ItemRender mItemRender;

    private bool _isSelect = false;

    protected override void onCreate()
    {
        base.onCreate();
        mItemRender = Create<ItemRender>(mRoot.goodItem);
        _isSelect = false;
    }

    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.Button_select)
        {
            EventManager.Instance.Dispatch(EEventType.TipsRewardSelectEvent, mIndex);
        }
    }

    protected override void dataChanged()
    {
        var data = (CommonItemData)mData;
        data.IsShowNum = false;
        mItemRender.setData(data);
        mRoot.Text_name.text = data.Name;
        mRoot.Text_desc.text = Utility.Text.Format("拥有:{0}", data.CountImt);
        SetSelectState(_isSelect);
    }

    public override void SetSelectState(bool state)
    {
        _isSelect = state;
        if (mRoot != null)
        {
            mRoot.Button_select.Imgae_select.visible = state;
        }
    }


    protected override void OnHide()
    {
        _isSelect = false;
    }

}



