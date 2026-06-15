using AthenaTrial;
using System.Collections.Generic;

public class AthenaTrialChapterProgressItem : BaseRender
{
    public override string mPackageName => G_AthenaTrialChapterProgressItem.PACKAGE_NAME;
    public override string mComponentName =>G_AthenaTrialChapterProgressItem.COMPONENT_NAME;

    public G_AthenaTrialChapterProgressItem mRoot => base.mRoot as G_AthenaTrialChapterProgressItem;

    private AthenaTrialChapterTowerItem towerItem;

    private TableView<AthenaTrialChapterStarItem> List_star;

    protected override void OnCreate()
    {
        towerItem = Create<AthenaTrialChapterTowerItem>(mRoot.towerItem);
        List_star = new TableView<AthenaTrialChapterStarItem>(mRoot.List_star);
    }

    protected override void DataChanged()
    {
        var data = mData as AthenaTrialChapterProgressItemData;
        if (data != null)
        {
            towerItem.SetIcon(data.TowerIcon);
            towerItem.SetChapter(data.Chapter);
            var isNull = data.starList == null || data.starList?.Count <= 0;
            towerItem.SetChapter(data.Chapter);
            towerItem.SetIcon(data.TowerIcon);
            mRoot.List_star.visible = !isNull;
            if (!isNull)
            {
                var starList = new List<List<int>>();
                for (var i = 0; i < data.starList.Count; i++)
                {
                    var level = data.starList[i];
                    starList.Add(new List<int>() { data.Type, level });
                }
                List_star.setDatas(starList);
            }
        }
    }
}

public class AthenaTrialChapterProgressItemData
{
    public string TowerIcon;

    public int Chapter;

    public int Type;

    public List<int> starList;
}



