/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelFightPowerView : GComponent
    {
        public GImage n82;
        public GImage n2;
        public GList fightPower;
        public GImage n34;
        public G_RoleLevelLeftHeadIcon HeadIconLeft;
        public G_rightHeadIcon rightHeadIcon;
        public GImage n54;
        public GButton closeBtn;
        public GTextField title;
        public G_fightPowerTab powTabItem;
        public GImage n60;
        public GImage n67;
        public GTextField n66;
        public GTextField n68;
        public GTextField n69;
        public G_TabList tabList;
        public GGroup n79;
        public const string URL = "ui://oz2qb5cgzbyu2q";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelFightPowerView";

        public static G_RoleLevelFightPowerView CreateInstance()
        {
            return (G_RoleLevelFightPowerView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n82 = (GImage)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            fightPower = (GList)GetChildAt(2);
            n34 = (GImage)GetChildAt(3);
            HeadIconLeft = (G_RoleLevelLeftHeadIcon)GetChildAt(4);
            rightHeadIcon = (G_rightHeadIcon)GetChildAt(5);
            n54 = (GImage)GetChildAt(6);
            closeBtn = (GButton)GetChildAt(7);
            title = (GTextField)GetChildAt(8);
            powTabItem = (G_fightPowerTab)GetChildAt(9);
            n60 = (GImage)GetChildAt(10);
            n67 = (GImage)GetChildAt(11);
            n66 = (GTextField)GetChildAt(12);
            n68 = (GTextField)GetChildAt(13);
            n69 = (GTextField)GetChildAt(14);
            tabList = (G_TabList)GetChildAt(15);
            n79 = (GGroup)GetChildAt(16);
        }
    }
}