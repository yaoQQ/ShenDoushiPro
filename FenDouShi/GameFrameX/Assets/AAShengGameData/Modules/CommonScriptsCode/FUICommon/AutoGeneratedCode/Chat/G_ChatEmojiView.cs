/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatEmojiView : GComponent
    {
        public GGraph EmojiMask;
        public GList EmojiTabList;
        public GImage n112;
        public GImage n113;
        public const string URL = "ui://gw0bj7t0ozwm47";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatEmojiView";

        public static G_ChatEmojiView CreateInstance()
        {
            return (G_ChatEmojiView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            EmojiMask = (GGraph)GetChildAt(0);
            EmojiTabList = (GList)GetChildAt(1);
            n112 = (GImage)GetChildAt(2);
            n113 = (GImage)GetChildAt(3);
        }
    }
}