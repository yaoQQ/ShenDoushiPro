using Bag;
using FairyGUI;
using System.Collections.Generic;
using Tips;

public class BagSelectionView : BaseRender
{
    public new G_BagSelectionView mRoot
    {
        get { return (G_BagSelectionView)base.mRoot; }
    }

    public override string mPackageName => G_BagSelectionView.PACKAGE_NAME;
    public override string mComponentName => G_BagSelectionView.COMPONENT_NAME;

    private BagSelectionGroup List_group;


    protected override void OnButtonClick(GButton clickedButton)
    {
        if (clickedButton == mRoot.viewMask)
        {
            Hide();
        }
    }


    protected override void onCreate()
    {
        List_group = Create<BagSelectionGroup>(mRoot.bagGroup);
    }

    protected override void dataChanged()
    {
        if (mData is List<List<BagSelectionItemData>> data)
        {
            List_group.setData(data[0]);
        }
    }
}




