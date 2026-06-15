/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_SelServerWin : GComponent
    {
        public GImage n3;
        public GTextField n4;
        public GImage n5;
        public G_circleCloseBtn backBtn;
        public GImage bgIcon;
        public GTextField emptyText;
        public GImage n29;
        public GImage n30;
        public GImage n31;
        public GImage n32;
        public GTextField n33;
        public GTextField n34;
        public GTextField n35;
        public GTextField n36;
        public GImage n38;
        public GList serverList;
        public GList areaList;
        public GGroup 选取弹窗;
        public const string URL = "ui://l64dumk9mue6k";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "SelServerWin";

        public static G_SelServerWin CreateInstance()
        {
            return (G_SelServerWin)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GTextField)GetChildAt(1);
            n5 = (GImage)GetChildAt(2);
            backBtn = (G_circleCloseBtn)GetChildAt(3);
            bgIcon = (GImage)GetChildAt(4);
            emptyText = (GTextField)GetChildAt(5);
            n29 = (GImage)GetChildAt(6);
            n30 = (GImage)GetChildAt(7);
            n31 = (GImage)GetChildAt(8);
            n32 = (GImage)GetChildAt(9);
            n33 = (GTextField)GetChildAt(10);
            n34 = (GTextField)GetChildAt(11);
            n35 = (GTextField)GetChildAt(12);
            n36 = (GTextField)GetChildAt(13);
            n38 = (GImage)GetChildAt(14);
            serverList = (GList)GetChildAt(15);
            areaList = (GList)GetChildAt(16);
            选取弹窗 = (GGroup)GetChildAt(17);
        }
    }
}