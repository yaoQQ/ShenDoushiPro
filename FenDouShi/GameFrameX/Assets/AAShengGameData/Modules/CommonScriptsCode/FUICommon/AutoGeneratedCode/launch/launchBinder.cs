/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace launch
{
    [UIBinder(PackageName = "launch")]
    public class launchBinder
    {
        public const string PACKAGE_NAME = "launch";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_LaunchPage.URL, typeof(G_LaunchPage));
            UIObjectFactory.SetPackageItemExtension(G_LaunchProgress.URL, typeof(G_LaunchProgress));
            UIObjectFactory.SetPackageItemExtension(G_LauchNoticeView.URL, typeof(G_LauchNoticeView));
            UIObjectFactory.SetPackageItemExtension(G_okBtn.URL, typeof(G_okBtn));
            UIObjectFactory.SetPackageItemExtension(G_closeBtn.URL, typeof(G_closeBtn));
            UIObjectFactory.SetPackageItemExtension(G_ageCloseBtn.URL, typeof(G_ageCloseBtn));
        }
    }
}