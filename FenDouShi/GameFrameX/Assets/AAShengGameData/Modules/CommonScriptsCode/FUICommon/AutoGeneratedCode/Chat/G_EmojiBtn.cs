/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_EmojiBtn : GButton
    {
        public GImage EmojiBtn;
        public const string URL = "ui://gw0bj7t0xf6940";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "EmojiBtn";

        public static G_EmojiBtn CreateInstance()
        {
            return (G_EmojiBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            EmojiBtn = (GImage)GetChildAt(0);
        }
    }
}