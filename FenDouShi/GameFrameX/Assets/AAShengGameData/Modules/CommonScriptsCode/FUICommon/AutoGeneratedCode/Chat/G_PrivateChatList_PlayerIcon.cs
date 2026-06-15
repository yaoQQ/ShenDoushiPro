/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_PrivateChatList_PlayerIcon : GButton
    {
        public Controller button;
        public GImage n61;
        public GTextField Name_Select;
        public GGroup Select;
        public GTextField Name_NotSelect;
        public GImage icon;
        public GTextField level;
        public GTextField State_Online;
        public GTextField State_Offline;
        public G_RedPointWithCount Player_RedPoint;
        public GImage n128;
        public const string URL = "ui://gw0bj7t0n0p53i";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "PrivateChatList_PlayerIcon";

        public static G_PrivateChatList_PlayerIcon CreateInstance()
        {
            return (G_PrivateChatList_PlayerIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n61 = (GImage)GetChildAt(0);
            Name_Select = (GTextField)GetChildAt(1);
            Select = (GGroup)GetChildAt(2);
            Name_NotSelect = (GTextField)GetChildAt(3);
            icon = (GImage)GetChildAt(4);
            level = (GTextField)GetChildAt(5);
            State_Online = (GTextField)GetChildAt(6);
            State_Offline = (GTextField)GetChildAt(7);
            Player_RedPoint = (G_RedPointWithCount)GetChildAt(8);
            n128 = (GImage)GetChildAt(9);
        }
    }
}