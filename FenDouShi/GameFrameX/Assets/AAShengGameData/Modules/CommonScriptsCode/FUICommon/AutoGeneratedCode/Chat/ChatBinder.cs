/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Chat
{
    [UIBinder(PackageName = "Chat")]
    public class ChatBinder
    {
        public const string PACKAGE_NAME = "Chat";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_ChatChancelBtn.URL, typeof(G_ChatChancelBtn));
            UIObjectFactory.SetPackageItemExtension(G_CloseBtn.URL, typeof(G_CloseBtn));
            UIObjectFactory.SetPackageItemExtension(G_EmojiTabBtn.URL, typeof(G_EmojiTabBtn));
            UIObjectFactory.SetPackageItemExtension(G_RedPointWithCount.URL, typeof(G_RedPointWithCount));
            UIObjectFactory.SetPackageItemExtension(G_PrivateChatList_TabBtn.URL, typeof(G_PrivateChatList_TabBtn));
            UIObjectFactory.SetPackageItemExtension(G_PrivateChatList_PlayerIcon.URL, typeof(G_PrivateChatList_PlayerIcon));
            UIObjectFactory.SetPackageItemExtension(G_ChatMessage_Other.URL, typeof(G_ChatMessage_Other));
            UIObjectFactory.SetPackageItemExtension(G_ChatMessage_Self.URL, typeof(G_ChatMessage_Self));
            UIObjectFactory.SetPackageItemExtension(G_ChatMessage_Sys.URL, typeof(G_ChatMessage_Sys));
            UIObjectFactory.SetPackageItemExtension(G_VoiceBtn.URL, typeof(G_VoiceBtn));
            UIObjectFactory.SetPackageItemExtension(G_ChatView.URL, typeof(G_ChatView));
            UIObjectFactory.SetPackageItemExtension(G_ChatEmojiView.URL, typeof(G_ChatEmojiView));
            UIObjectFactory.SetPackageItemExtension(G_SmallWindow.URL, typeof(G_SmallWindow));
            UIObjectFactory.SetPackageItemExtension(G_ChatMessage_Time.URL, typeof(G_ChatMessage_Time));
            UIObjectFactory.SetPackageItemExtension(G_EmojiBtn.URL, typeof(G_EmojiBtn));
        }
    }
}