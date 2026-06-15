using AthenaTrial;

public class AthenaTrialChapterTowerItem : BaseRender
{
    public override string mPackageName => G_AthenaTrialChapterTowerItem.PACKAGE_NAME;
    public override string mComponentName => G_AthenaTrialChapterTowerItem.COMPONENT_NAME;

    public G_AthenaTrialChapterTowerItem mRoot => base.mRoot as G_AthenaTrialChapterTowerItem;

    public void SetIcon(string icon)
    {
        if (mRoot == null) return;
        mRoot.Image_icon.url = UIHelper.GetFguiUrl(mPackageName, icon);
    }

    public void SetChapter(int level)
    {
        if (mRoot == null) return;
        mRoot.Text_num.text = level.ToString();
    }
}






