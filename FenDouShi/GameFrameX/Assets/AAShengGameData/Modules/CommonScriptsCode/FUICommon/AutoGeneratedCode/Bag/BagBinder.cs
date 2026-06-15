/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Bag
{
    [UIBinder(PackageName = "Bag")]
    public class BagBinder
    {
        public const string PACKAGE_NAME = "Bag";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_BagTabItem.URL, typeof(G_BagTabItem));
            UIObjectFactory.SetPackageItemExtension(G_BagTipsCompoment.URL, typeof(G_BagTipsCompoment));
            UIObjectFactory.SetPackageItemExtension(G_BagView.URL, typeof(G_BagView));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_gongnengdi.URL, typeof(G_common_btn_gongnengdi));
            UIObjectFactory.SetPackageItemExtension(G_BagSelectionItem.URL, typeof(G_BagSelectionItem));
            UIObjectFactory.SetPackageItemExtension(G_BagSelectionView.URL, typeof(G_BagSelectionView));
            UIObjectFactory.SetPackageItemExtension(G_BagSelectionGroup.URL, typeof(G_BagSelectionGroup));
        }
    }
}