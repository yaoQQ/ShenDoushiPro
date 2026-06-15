using OnHookUI;
using System.Collections.Generic;
using UnityEngine;

public class OnHookRenderer_DungeonReward : BaseRender
{
    public override string mPackageName => G_OnHookView_Main_IncomeItem.PACKAGE_NAME;
    public override string mComponentName => G_OnHookView_Main_IncomeItem.COMPONENT_NAME;

    public new G_OnHookView_Main_IncomeItem mRoot => (G_OnHookView_Main_IncomeItem)base.mRoot;

    protected override void dataChanged()
    {
        var data = (List<int>)mData;
        mRoot.Icon.url = BagControl.Instance.Model.GetItemIconUrlById(data[0]);
        mRoot.Count.text = $"{Mathf.FloorToInt(data[1] / 10000f)}/m";
    }
}