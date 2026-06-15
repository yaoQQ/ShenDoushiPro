/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Experience
{
    [UIBinder(PackageName = "Experience")]
    public class ExperienceBinder
    {
        public const string PACKAGE_NAME = "Experience";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_ExperienceView.URL, typeof(G_ExperienceView));
            UIObjectFactory.SetPackageItemExtension(G_ExperienceListItem.URL, typeof(G_ExperienceListItem));
            UIObjectFactory.SetPackageItemExtension(G_YaDianNaDungeon.URL, typeof(G_YaDianNaDungeon));
            UIObjectFactory.SetPackageItemExtension(G_InstanceLogo.URL, typeof(G_InstanceLogo));
            UIObjectFactory.SetPackageItemExtension(G_InstanceBg.URL, typeof(G_InstanceBg));
            UIObjectFactory.SetPackageItemExtension(G_TitleBgCrl.URL, typeof(G_TitleBgCrl));
        }
    }
}