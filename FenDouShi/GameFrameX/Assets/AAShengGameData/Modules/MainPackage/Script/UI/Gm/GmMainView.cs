using System.Collections.Generic;
using System.Threading.Tasks;
using FairyGUI;
using GM;
using static GM.GmDefine;
[FGUIViewAttribute(UIViewEnum.GmMainView, typeof(GmMainView))]
public class GmMainView : BaseView
{
    private G_GmMainView mainView;
    public override string PackageName => G_GmMainView.PACKAGE_NAME;
    public override string ComponentName => G_GmMainView.COMPONENT_NAME;

    private TableView<GmTabItem> tabList;

    private Dictionary<eTabType, BaseRender> pages = new();

    private GMTabItemData mTabItemData;


    public GmMainView()
    {
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.GmMainView, false);
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        base.OnFinishLoad(gameObject);
        this.contentPane = gameObject.asCom;
        mainView = contentPane.asCom as G_GmMainView;
        mainView.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
        closeButton = mainView.closoButton;
        //Ìì∫û
        tabList = new TableView<GmTabItem>(mainView.List_tab.List_tab);
        tabList.setClickCallBack(onClickTab);
        tabList.setDatas(gmTabs);
        tabList.setSelectIndex(0);
        mTabItemData = gmTabs[tabList.mSelectIndex];
        _ = showPage(mTabItemData.eTabType);
    }

    private void onClickTab(object data, int index)
    {
        mTabItemData = data as GMTabItemData;
        _ = showPage(mTabItemData.eTabType);
    }

    /// <summary>
    /// œ‘ æ“≥√Ê
    /// </summary>
    private async Task showPage(eTabType type)
    {
        hideAllPages();
        //view
        if (pages.TryGetValue(type, out var page))
        {
            page.Show();
        }
        else
        {
            if (type == eTabType.client)
            {
                page = await BaseRender.Create<GmCustomPageView>();
            }
            else if (type == eTabType.custom)
            {
                page = await BaseRender.Create<GmApiPageView>();
            }
            mainView.viewParent.AddChild(page.mRoot);
            pages.Add(type, page);
        }
        page.setData(mTabItemData);
        page.Show();
    }

    private void hideAllPages()
    {
        foreach (var page in pages.Values)
        {
            page.Hide();
        }
    }
}

