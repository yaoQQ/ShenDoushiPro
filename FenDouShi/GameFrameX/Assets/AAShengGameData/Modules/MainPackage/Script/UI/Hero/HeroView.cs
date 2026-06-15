using FairyGUI;
using Hero;
using System.Collections.Generic;

public class HeroView : BaseView
{
    public override string PackageName => G_Main.PACKAGE_NAME;
    public override string ComponentName => G_Main.COMPONENT_NAME;

    private GList HeroList;

    private HeroItemRenderer heroItemRenderer;

    private List<Hero2dItemData> hero2DItemDatas;
    public HeroView()
    {
        //（声明界面所在层级 ,界面注册的Enum标志,是否加入层级堆栈管理）
        setViewAttribute(UIViewLayerType.Second_View, UIViewEnum.HeroView, false);
        HeroBinder.BindAll();
    }

    protected override void OnFinishLoad(GComponent gameObject)
    {
        this.contentPane = gameObject.asCom;
        //contentPane.MakeFullScreen();
        contentPane.AddRelation(GRoot.inst, RelationType.Size);
    }

    protected override void OnShown()
    {
        base.closeButton = contentPane.GetChild("closeButton");
        HeroList = contentPane.GetChild("heroList").asList;
        heroItemRenderer = new HeroItemRenderer();
        HeroList.itemRenderer = heroItemRenderer.Renderer;
        HeroList.SetVirtual();
        RefreshHeroView();
    }

    private void RefreshHeroView()
    {
        hero2DItemDatas = new List<Hero2dItemData>();
        for (int i = 0; i < 20; i++)
        {
            var num = DataTableFrame.Utility.Random.GetRandom(1, 6);
            var hero_res = DataTableFrame.Utility.Random.GetRandom(0, 4);

            hero2DItemDatas.Add(new Hero2dItemData() { level = i, name = string.Format($"name_{i}"), star = num, heroRes = string.Format($"ui://Hero/hero_{hero_res}") });
        }
        heroItemRenderer.SetData(hero2DItemDatas);
        HeroList.numItems = hero2DItemDatas.Count;
    }
}

