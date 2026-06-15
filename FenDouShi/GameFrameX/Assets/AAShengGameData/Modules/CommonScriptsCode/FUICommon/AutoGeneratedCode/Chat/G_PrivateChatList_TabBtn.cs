/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_PrivateChatList_TabBtn : GButton
    {
        public Controller expanded;
        public GImage n80;
        public GTextField Name_NotSelect;
        public GTextField Count_NotSelect;
        public GImage n124;
        public GGroup NotSelect;
        public GImage n77;
        public GTextField Name_Select;
        public GTextField Count_Select;
        public GImage n123;
        public GGroup Select;
        public G_RedPointWithCount RedPoint;
        public const string URL = "ui://gw0bj7t0n0p53f";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "PrivateChatList_TabBtn";

        public static G_PrivateChatList_TabBtn CreateInstance()
        {
            return (G_PrivateChatList_TabBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            expanded = GetControllerAt(0);
            n80 = (GImage)GetChildAt(0);
            Name_NotSelect = (GTextField)GetChildAt(1);
            Count_NotSelect = (GTextField)GetChildAt(2);
            n124 = (GImage)GetChildAt(3);
            NotSelect = (GGroup)GetChildAt(4);
            n77 = (GImage)GetChildAt(5);
            Name_Select = (GTextField)GetChildAt(6);
            Count_Select = (GTextField)GetChildAt(7);
            n123 = (GImage)GetChildAt(8);
            Select = (GGroup)GetChildAt(9);
            RedPoint = (G_RedPointWithCount)GetChildAt(10);
        }
    }
}