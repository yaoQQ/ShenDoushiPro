/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatChancelBtn : GButton
    {
        public Controller button;
        public GImage n119;
        public GImage n124;
        public GLoader icon;
        public GLoader Icon_Select;
        public GTextField Name_NotSelect;
        public GTextField Name_Select;
        public G_RedPointWithCount RedPointIcon;
        public const string URL = "ui://gw0bj7t0n0p52s";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatChancelBtn";

        public static G_ChatChancelBtn CreateInstance()
        {
            return (G_ChatChancelBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n119 = (GImage)GetChildAt(0);
            n124 = (GImage)GetChildAt(1);
            icon = (GLoader)GetChildAt(2);
            Icon_Select = (GLoader)GetChildAt(3);
            Name_NotSelect = (GTextField)GetChildAt(4);
            Name_Select = (GTextField)GetChildAt(5);
            RedPointIcon = (G_RedPointWithCount)GetChildAt(6);
        }
    }
}