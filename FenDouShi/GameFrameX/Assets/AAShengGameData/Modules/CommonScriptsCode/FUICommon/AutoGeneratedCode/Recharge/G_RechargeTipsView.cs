/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Recharge
{
    public partial class G_RechargeTipsView : GComponent
    {
        public Controller control;
        public GImage n35;
        public G_BgLogHuaWen n21;
        public GImage n37;
        public GImage n38;
        public GTextField giftName;
        public GLoader giftIcon;
        public GImage n5;
        public GList rewardSingleList;
        public GTextField singleTitle1;
        public GTextField singleTitle2;
        public GGroup rewardtitle;
        public GGroup reward1;
        public GImage n41;
        public GTextField n42;
        public GImage n43;
        public GImage n44;
        public GTextField n45;
        public GImage n46;
        public GList baseReward;
        public GList doubleReward;
        public GGroup reward2;
        public GButton cancelBtn;
        public GButton moneyBtn;
        public GButton voucherBtn;
        public GGroup bottom;
        public const string URL = "ui://ynu7b0nvmv0c11";
        public const string PACKAGE_NAME = "Recharge";
        public const string COMPONENT_NAME = "RechargeTipsView";

        public static G_RechargeTipsView CreateInstance()
        {
            return (G_RechargeTipsView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            control = GetControllerAt(0);
            n35 = (GImage)GetChildAt(0);
            n21 = (G_BgLogHuaWen)GetChildAt(1);
            n37 = (GImage)GetChildAt(2);
            n38 = (GImage)GetChildAt(3);
            giftName = (GTextField)GetChildAt(4);
            giftIcon = (GLoader)GetChildAt(5);
            n5 = (GImage)GetChildAt(6);
            rewardSingleList = (GList)GetChildAt(7);
            singleTitle1 = (GTextField)GetChildAt(8);
            singleTitle2 = (GTextField)GetChildAt(9);
            rewardtitle = (GGroup)GetChildAt(10);
            reward1 = (GGroup)GetChildAt(11);
            n41 = (GImage)GetChildAt(12);
            n42 = (GTextField)GetChildAt(13);
            n43 = (GImage)GetChildAt(14);
            n44 = (GImage)GetChildAt(15);
            n45 = (GTextField)GetChildAt(16);
            n46 = (GImage)GetChildAt(17);
            baseReward = (GList)GetChildAt(18);
            doubleReward = (GList)GetChildAt(19);
            reward2 = (GGroup)GetChildAt(20);
            cancelBtn = (GButton)GetChildAt(21);
            moneyBtn = (GButton)GetChildAt(22);
            voucherBtn = (GButton)GetChildAt(23);
            bottom = (GGroup)GetChildAt(24);
        }
    }
}