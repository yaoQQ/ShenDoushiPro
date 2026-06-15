using OnHookUI;
using System.Collections.Generic;
using UnityEngine;

public class OnHookRenderer_Detail_Item : BaseRender
{
    public override string mPackageName => G_OnHookView_Detail_Item.PACKAGE_NAME;
    public override string mComponentName => G_OnHookView_Detail_Item.COMPONENT_NAME;

    public new G_OnHookView_Detail_Item mRoot => (G_OnHookView_Detail_Item)base.mRoot;

    protected override void dataChanged()
    {
        var data = (List<int>)mData;

        mRoot.icon.url = BagControl.Instance.Model.GetItemIconUrlById(data[0]);
        var itemVo = BagControl.Instance.Model.GetItemCfgById(data[0]);
        if (itemVo != null)
        {
            mRoot.itemName.text = $"{itemVo.Name}:";
        }
        mRoot.count.text = $"{Mathf.FloorToInt(data[1] / 10000f)}/m";
    }
}