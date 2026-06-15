/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace FGUIMail
{
    [UIBinder(PackageName = "FGUIMail")]
    public class FGUIMailBinder
    {
        public const string PACKAGE_NAME = "FGUIMail";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_TextList.URL, typeof(G_TextList));
            UIObjectFactory.SetPackageItemExtension(G_MailView.URL, typeof(G_MailView));
            UIObjectFactory.SetPackageItemExtension(G_MailTypeTab.URL, typeof(G_MailTypeTab));
            UIObjectFactory.SetPackageItemExtension(G_MailItem.URL, typeof(G_MailItem));
            UIObjectFactory.SetPackageItemExtension(G_AlreadyReceive.URL, typeof(G_AlreadyReceive));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_guanbi_xiao_1.URL, typeof(G_common_btn_guanbi_xiao_1));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_02_2.URL, typeof(G_common_btn_02_2));
            UIObjectFactory.SetPackageItemExtension(G_common_btn_03_2.URL, typeof(G_common_btn_03_2));
            UIObjectFactory.SetPackageItemExtension(G_MailDeleteComfirm.URL, typeof(G_MailDeleteComfirm));
        }
    }
}