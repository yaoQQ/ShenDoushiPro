/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendApplyPanel : GComponent
    {
        public GButton agreeBtn;
        public GButton refuseBtn;
        public GImage n84;
        public GTextField n85;
        public GGroup empty;
        public GList applyList;
        public const string URL = "ui://jyya7ryuvliv4k";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendApplyPanel";

        public static G_FriendApplyPanel CreateInstance()
        {
            return (G_FriendApplyPanel)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            agreeBtn = (GButton)GetChildAt(0);
            refuseBtn = (GButton)GetChildAt(1);
            n84 = (GImage)GetChildAt(2);
            n85 = (GTextField)GetChildAt(3);
            empty = (GGroup)GetChildAt(4);
            applyList = (GList)GetChildAt(5);
        }
    }
}