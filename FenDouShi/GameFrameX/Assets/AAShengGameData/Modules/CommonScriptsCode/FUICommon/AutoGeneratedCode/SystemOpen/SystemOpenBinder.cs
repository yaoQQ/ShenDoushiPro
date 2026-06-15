/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace SystemOpen
{
    [UIBinder(PackageName = "SystemOpen")]
    public class SystemOpenBinder
    {
        public const string PACKAGE_NAME = "SystemOpen";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_SystemTaskItem.URL, typeof(G_SystemTaskItem));
            UIObjectFactory.SetPackageItemExtension(G_SystemNewOpenView.URL, typeof(G_SystemNewOpenView));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_gongnengdi01_1.URL, typeof(G_common_btn_gongnengdi01_1));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_jiantou02_1.URL, typeof(G_common_btn_jiantou02_1));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_02_1.URL, typeof(G_common_btn_02_1));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_guanbi_da_1.URL, typeof(G_common_btn_guanbi_da_1));
            UIObjectFactory.SetPackageItemExtension(G_SystemOpenView.URL, typeof(G_SystemOpenView));
            UIObjectFactory.SetPackageItemExtension(G_SystemTabItem.URL, typeof(G_SystemTabItem));
            UIObjectFactory.SetPackageItemExtension(G_Group_Jump.URL, typeof(G_Group_Jump));
        }
    }
}