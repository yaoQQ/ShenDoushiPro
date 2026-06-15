/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace GM
{
    [UIBinder(PackageName = "GM")]
    public class GMBinder
    {
        public const string PACKAGE_NAME = "GM";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_GMTabListCompoment.URL, typeof(G_GMTabListCompoment));
            UIObjectFactory.SetPackageItemExtension(G_GmMainView.URL, typeof(G_GmMainView));
            UIObjectFactory.SetPackageItemExtension(G_GmApiPageView.URL, typeof(G_GmApiPageView));
            UIObjectFactory.SetPackageItemExtension(G_GmTabItem.URL, typeof(G_GmTabItem));
            UIObjectFactory.SetPackageItemExtension(G_GmCustomButtonItem.URL, typeof(G_GmCustomButtonItem));
            UIObjectFactory.SetPackageItemExtension(G_GmCustomPageView.URL, typeof(G_GmCustomPageView));
        }
    }
}