/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Friend
{
    [UIBinder(PackageName = "Friend")]
    public class FriendBinder
    {
        public const string PACKAGE_NAME = "Friend";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_FriendRedBtn.URL, typeof(G_FriendRedBtn));
            UIObjectFactory.SetPackageItemExtension(G_FriendGreenBtn.URL, typeof(G_FriendGreenBtn));
            UIObjectFactory.SetPackageItemExtension(G_FriendView.URL, typeof(G_FriendView));
            UIObjectFactory.SetPackageItemExtension(G_FriendListPanel.URL, typeof(G_FriendListPanel));
            UIObjectFactory.SetPackageItemExtension(G_FriendApplyPanel.URL, typeof(G_FriendApplyPanel));
            UIObjectFactory.SetPackageItemExtension(G_FriendBlackPanel.URL, typeof(G_FriendBlackPanel));
            UIObjectFactory.SetPackageItemExtension(G_FriendSerachPanel.URL, typeof(G_FriendSerachPanel));
            UIObjectFactory.SetPackageItemExtension(G_FriendListRender.URL, typeof(G_FriendListRender));
            UIObjectFactory.SetPackageItemExtension(G_FriendOptionBtn.URL, typeof(G_FriendOptionBtn));
            UIObjectFactory.SetPackageItemExtension(G_SelectDeleteComp.URL, typeof(G_SelectDeleteComp));
        }
    }
}