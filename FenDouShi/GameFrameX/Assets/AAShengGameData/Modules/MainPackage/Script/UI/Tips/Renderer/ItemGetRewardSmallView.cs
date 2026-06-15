using FairyGUI;
using Tips;

public class GetRewardSmallItem : BaseRender
{
    public new G_ItemGetRewardSmallItem mRoot
    {
        get { return (G_ItemGetRewardSmallItem)base.mRoot; }
    }
    public override string mPackageName => G_ItemGetRewardSmallItem.PACKAGE_NAME;
    public override string mComponentName => G_ItemGetRewardSmallItem.COMPONENT_NAME;


    private GoodItemRenderer mGoodItemRender;

    private GTweener mTween1;
    private GTweener mTween2;
    private const float endY = -220f;
    private const float aniTime = 1.2f;
    public GetRewardSmallItem()
    {
    }

    protected override void onCreate()
    {
        mGoodItemRender = Create<GoodItemRenderer>(mRoot.goodItem);
    }

    protected override void dataChanged()
    {
        base.dataChanged();
        var data = mData as CommonItemData;
        data.IsShowNum = false;
        mGoodItemRender.setData(data);
        mRoot.Text_name.text = data.Name;
        mRoot.Text_count.text = data.Count.ToString();
        OnPlayAni();
    }

    public void OnPlayAni()
    {
        mRoot.y = 0;
        mRoot.alpha = 1f;
        mTween1?.Kill();
        mTween2?.Kill();
        mTween1 = mRoot.TweenMoveY(endY, aniTime);
        mTween2 = mRoot.TweenFade(0, aniTime).OnComplete(() =>
        {
            OnFinish(mRoot);
        });
    }

    private void OnFinish(G_ItemGetRewardSmallItem obj)
    {
        mTween1 = null;
        mTween2 = null;
        UIObjPool.Instance.ReturnObject(obj);
    }
}


