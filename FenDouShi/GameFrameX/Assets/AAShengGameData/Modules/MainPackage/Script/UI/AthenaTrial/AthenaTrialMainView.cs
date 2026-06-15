using System.Collections.Generic;
using AthenaTrial;
using FairyGUI;

[FGUIViewAttribute(UIViewEnum.AthenaTrialMainView, typeof(AthenaTrialMainView))]
public class AthenaTrialMainView : BaseView
{
    public override string PackageName => G_AthenaTrialMainView.PACKAGE_NAME;
    public override string ComponentName => G_AthenaTrialMainView.COMPONENT_NAME;


    protected override TopRenderData mTopRenderData => new TopRenderData
    {
        titleName = "—≈µ‰ƒ» ‘¡∂",
        helpId = 10000,
    };

    protected override Dictionary<EEventType, OnEventLister> EventList => new Dictionary<EEventType, OnEventLister>()
    {
        { EEventType.AthenaTrialInfoUpdate,OnAthenaTrialEvent}
    };

    private void OnAthenaTrialEvent(EventSysArgsBase argsBase)
    {
        RefreshTab();
    }


    private G_AthenaTrialMainView mRoot;


    private List<AthenaTrialTabItemData> tabItemDatas;

    private List<AthenaTrialTabItem> List_tab;

    public AthenaTrialMainView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.AthenaTrialMainView, false);
    }

    protected override void OnFinishLoad(GComponent gComponent)
    {
        base.OnFinishLoad(gComponent);
        contentPane = gComponent.asCom;
        mRoot = contentPane as G_AthenaTrialMainView;
        contentPane.SetSize(GRoot.inst.width, GRoot.inst.height);
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        List_tab = new();
        tabItemDatas = new List<AthenaTrialTabItemData>() {
            new AthenaTrialTabItemData(){
                TextTitle = mRoot.Text_title1,
                TextDesc = mRoot.Text_desc1,
                ButtonClick = mRoot.Button_1,
                RedPoint = mRoot.redPoint1,
                Type = 1,
            },
            new AthenaTrialTabItemData(){
                TextTitle = mRoot.Text_title2,
                TextDesc = mRoot.Text_desc2,
                ButtonClick = mRoot.Button_2,
                RedPoint = mRoot.redPoint2,
                Type = 2,
            },
            new AthenaTrialTabItemData(){
                TextTitle = mRoot.Text_title3,
                TextDesc = mRoot.Text_desc3,
                ButtonClick = mRoot.Button_3,
                RedPoint = mRoot.redPoint3,
                Type = 3,
            },
            new AthenaTrialTabItemData(){
                TextTitle = mRoot.Text_title4,
                TextDesc = mRoot.Text_desc4,
                ButtonClick = mRoot.Button_4,
                RedPoint = mRoot.redPoint4,
                Type = 4,
            },
            new AthenaTrialTabItemData(){
                TextTitle = mRoot.Text_title5,
                TextDesc = mRoot.Text_desc5,
                ButtonClick = mRoot.Button_5,
                RedPoint = mRoot.redPoint5,
                Type = 5,
            },
        };
        List_tab ??= new List<AthenaTrialTabItem>();
        foreach (var tabItem in tabItemDatas)
        {
            var mItem = new AthenaTrialTabItem();
            mItem.Renderer(tabItem);
            mItem.SetClickCallBack(() =>
            {
                var copyType = tabItem.Type;
                var isOpen = AthenaTrialControl.Instance.Model.GetIsCopyTypeOpen(copyType);
                if (!isOpen)
                {
                    var descStr = AthenaTrialControl.Instance.Model.GetCopyTypeOpenStr(copyType);
                    CommonViewUtils.ShowTopTips(descStr);
                    return;
                }
                UIViewManager.Instance.Show(UIViewEnum.AthenaTrialChapterView, copyType);
            });
            List_tab.Add(mItem);
        }
    }

    private void RefreshTab()
    {
        if (List_tab == null)
        {
            return;
        }
        for (var index = 0; index < List_tab.Count; index++)
        {
            var tab = List_tab[index];
            var data = tabItemDatas[index];
            tab.Renderer(data);
        }
    }

    protected override void OnShown()
    {
        base.OnShown();
        RefreshTab();
    }
}

