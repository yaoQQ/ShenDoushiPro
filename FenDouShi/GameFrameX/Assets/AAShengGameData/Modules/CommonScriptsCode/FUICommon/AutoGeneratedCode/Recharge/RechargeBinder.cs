/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Recharge
{
    [UIBinder(PackageName = "Recharge")]
    public class RechargeBinder
    {
        public const string PACKAGE_NAME = "Recharge";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_RechargeTipsView.URL, typeof(G_RechargeTipsView));
            UIObjectFactory.SetPackageItemExtension(G_RechargeLeftTab.URL, typeof(G_RechargeLeftTab));
            UIObjectFactory.SetPackageItemExtension(G_RechargeListItemRender.URL, typeof(G_RechargeListItemRender));
            UIObjectFactory.SetPackageItemExtension(G_BgLogHuaWen.URL, typeof(G_BgLogHuaWen));
            UIObjectFactory.SetPackageItemExtension(G_RechargeView.URL, typeof(G_RechargeView));
        }
    }
}