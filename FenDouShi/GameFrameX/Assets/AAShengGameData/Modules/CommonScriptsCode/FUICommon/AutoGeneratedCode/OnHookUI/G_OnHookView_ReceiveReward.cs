/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_ReceiveReward : GComponent
    {
        public G_OnHookView_ReceiveReward_Btn closeMask;
        public GImage n59;
        public GImage n60;
        public GImage n37;
        public GImage n38;
        public GTextField n39;
        public GTextField level;
        public GTextField n53;
        public GTextField time;
        public G_ExpSlider n56;
        public GList RewardList;
        public GImage n23;
        public GImage n24;
        public GImage n25;
        public GImage n26;
        public GImage n28;
        public GImage n29;
        public GImage n30;
        public GImage n31;
        public GImage n32;
        public GImage n19;
        public GImage n20;
        public GImage n35;
        public GImage n10;
        public GImage n11;
        public GTextField n6;
        public GGroup n63;
        public const string URL = "ui://pux1kqsbpb4z2e";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_ReceiveReward";

        public static G_OnHookView_ReceiveReward CreateInstance()
        {
            return (G_OnHookView_ReceiveReward)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            closeMask = (G_OnHookView_ReceiveReward_Btn)GetChildAt(0);
            n59 = (GImage)GetChildAt(1);
            n60 = (GImage)GetChildAt(2);
            n37 = (GImage)GetChildAt(3);
            n38 = (GImage)GetChildAt(4);
            n39 = (GTextField)GetChildAt(5);
            level = (GTextField)GetChildAt(6);
            n53 = (GTextField)GetChildAt(7);
            time = (GTextField)GetChildAt(8);
            n56 = (G_ExpSlider)GetChildAt(9);
            RewardList = (GList)GetChildAt(10);
            n23 = (GImage)GetChildAt(11);
            n24 = (GImage)GetChildAt(12);
            n25 = (GImage)GetChildAt(13);
            n26 = (GImage)GetChildAt(14);
            n28 = (GImage)GetChildAt(15);
            n29 = (GImage)GetChildAt(16);
            n30 = (GImage)GetChildAt(17);
            n31 = (GImage)GetChildAt(18);
            n32 = (GImage)GetChildAt(19);
            n19 = (GImage)GetChildAt(20);
            n20 = (GImage)GetChildAt(21);
            n35 = (GImage)GetChildAt(22);
            n10 = (GImage)GetChildAt(23);
            n11 = (GImage)GetChildAt(24);
            n6 = (GTextField)GetChildAt(25);
            n63 = (GGroup)GetChildAt(26);
        }
    }
}