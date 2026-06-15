using AthenaTrial;
using System.Collections.Generic;

public class AthenaTrialChapterStarItem : BaseRender
{
    public override string mPackageName => G_AthenaTrialChapterStarItem.PACKAGE_NAME;
    public override string mComponentName => G_AthenaTrialChapterStarItem.COMPONENT_NAME;

    public G_AthenaTrialChapterStarItem mRoot => base.mRoot as G_AthenaTrialChapterStarItem;

    protected override void OnCreate()
    {
    }

    protected override void DataChanged()
    {
        if (mData is List<int> ints)
        {
            var type = ints[0];
            var level = ints[1];
            var mMax = AthenaTrialControl.Instance.Model.GetPassMaxLevel(type);
            var isLight = mMax >= level;
            var res = isLight ? "yadiannashilian_icon_xingxing01" : "yadiannashilian_icon_xingxing02";
            mRoot.Image_satr.url = UIHelper.GetFguiUrl(mPackageName, res);
        }
    }
}



