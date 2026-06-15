using OnHookUI;
using System.Collections.Generic;

public class OnHookRenderer_Detail_Chapter : BaseRender
{
    public override string mPackageName => G_OnHookView_Detail_Chapter.PACKAGE_NAME;
    public override string mComponentName => G_OnHookView_Detail_Chapter.COMPONENT_NAME;

    public new G_OnHookView_Detail_Chapter mRoot => (G_OnHookView_Detail_Chapter)base.mRoot;

    TableView<OnHookRenderer_Detail_Item> dungeronTableView;

    protected override void onCreate()
    {
        dungeronTableView = new(mRoot.itemList);
        dungeronTableView.SetAlign(FairyGUI.AlignType.Center);
    }

    protected override void dataChanged()
    {
        var data = (int)mData;

        var DATA = ON_HOOK_REWARD_DIC.DIC[data];
        mRoot.title.text = DATA.DungeonId.ToString();

        List<List<int>> reward = new();
        for (int i = 0; i < 4; i++)
            if (i < DATA.Rewards.Count)
                reward.Add(DATA.Rewards[i]);
        dungeronTableView.setDatas(reward);

        var dungeonId = OnHookControl.Instance.Model.dungeonId;
        bool isFinish = dungeonId >= DATA.DungeonId;
        mRoot.n51.visible = mRoot.bar.visible = isFinish;
        mRoot.n52.visible = !mRoot.n51.visible;

        if (DATA.HighRewards != null && DATA.HighRewards.Count > 0)
        {
            mRoot.RT.visible = true;
            mRoot.ExtensionIcon.url = BagControl.Instance.Model.GetItemIconUrlById(DATA.HighRewards[0]);
        }
        else
        {
            mRoot.RT.visible = false;
        }
    }
}