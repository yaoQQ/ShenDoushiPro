/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendListPanel : GComponent
    {
        public Controller opt;
        public GButton rightBtn;
        public GButton leftBtn;
        public GTextField friendCount;
        public GImage n136;
        public GTextField friendPoint;
        public GGroup n200;
        public GList friendList;
        public GImage n205;
        public GTextField n206;
        public GGroup empty;
        public const string URL = "ui://jyya7ryuvliv4j";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendListPanel";

        public static G_FriendListPanel CreateInstance()
        {
            return (G_FriendListPanel)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            opt = GetControllerAt(0);
            rightBtn = (GButton)GetChildAt(0);
            leftBtn = (GButton)GetChildAt(1);
            friendCount = (GTextField)GetChildAt(2);
            n136 = (GImage)GetChildAt(3);
            friendPoint = (GTextField)GetChildAt(4);
            n200 = (GGroup)GetChildAt(5);
            friendList = (GList)GetChildAt(6);
            n205 = (GImage)GetChildAt(7);
            n206 = (GTextField)GetChildAt(8);
            empty = (GGroup)GetChildAt(9);
        }
    }
}