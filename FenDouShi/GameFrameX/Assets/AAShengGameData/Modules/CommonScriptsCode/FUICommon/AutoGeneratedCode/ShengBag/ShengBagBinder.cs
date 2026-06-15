/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ShengBag
{
    [UIBinder(PackageName = "ShengBag")]
    public class ShengBagBinder
    {
        public const string PACKAGE_NAME = "ShengBag";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_GameMainView.URL, typeof(G_GameMainView));
            UIObjectFactory.SetPackageItemExtension(G_BagButton.URL, typeof(G_BagButton));
            UIObjectFactory.SetPackageItemExtension(G_FightButton.URL, typeof(G_FightButton));
        }
    }
}