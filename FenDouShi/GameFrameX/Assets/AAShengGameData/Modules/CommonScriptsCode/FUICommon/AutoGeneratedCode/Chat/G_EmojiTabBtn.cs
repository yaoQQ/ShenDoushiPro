/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_EmojiTabBtn : GButton
    {
        public Controller button;
        public GImage SelectBg;
        public GTextField TextDown;
        public GGroup Down;
        public GImage n127;
        public GTextField TextUp;
        public GGroup Up;
        public const string URL = "ui://gw0bj7t0n0p53c";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "EmojiTabBtn";

        public static G_EmojiTabBtn CreateInstance()
        {
            return (G_EmojiTabBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            SelectBg = (GImage)GetChildAt(0);
            TextDown = (GTextField)GetChildAt(1);
            Down = (GGroup)GetChildAt(2);
            n127 = (GImage)GetChildAt(3);
            TextUp = (GTextField)GetChildAt(4);
            Up = (GGroup)GetChildAt(5);
        }
    }
}