/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendSerachPanel : GComponent
    {
        public GButton addAllBtn;
        public GButton changeBtn;
        public GGroup opt;
        public GTextField friendCountLabel;
        public GImage n97;
        public GImage n41;
        public GTextField friendPoint;
        public GGroup n98;
        public GImage n86;
        public GTextField n87;
        public GGroup ttlb;
        public GImage n90;
        public GTextInput inpuName;
        public GButton serachBtn;
        public GImage n92;
        public GList serachList;
        public GImage n100;
        public GTextField n101;
        public GGroup empty;
        public const string URL = "ui://jyya7ryuvliv4m";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendSerachPanel";

        public static G_FriendSerachPanel CreateInstance()
        {
            return (G_FriendSerachPanel)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            addAllBtn = (GButton)GetChildAt(0);
            changeBtn = (GButton)GetChildAt(1);
            opt = (GGroup)GetChildAt(2);
            friendCountLabel = (GTextField)GetChildAt(3);
            n97 = (GImage)GetChildAt(4);
            n41 = (GImage)GetChildAt(5);
            friendPoint = (GTextField)GetChildAt(6);
            n98 = (GGroup)GetChildAt(7);
            n86 = (GImage)GetChildAt(8);
            n87 = (GTextField)GetChildAt(9);
            ttlb = (GGroup)GetChildAt(10);
            n90 = (GImage)GetChildAt(11);
            inpuName = (GTextInput)GetChildAt(12);
            serachBtn = (GButton)GetChildAt(13);
            n92 = (GImage)GetChildAt(14);
            serachList = (GList)GetChildAt(15);
            n100 = (GImage)GetChildAt(16);
            n101 = (GTextField)GetChildAt(17);
            empty = (GGroup)GetChildAt(18);
        }
    }
}