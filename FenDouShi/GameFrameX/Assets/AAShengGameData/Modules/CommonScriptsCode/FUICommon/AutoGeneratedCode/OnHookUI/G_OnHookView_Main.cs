/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Main : GComponent
    {
        public GImage n3;
        public GImage n4;
        public GImage n32;
        public GImage n12;
        public GImage n13;
        public GImage s1_disabled;
        public GImage s1_enabled;
        public GTextField s1_text;
        public GImage s2_disabled;
        public GImage s2_enabled;
        public GTextField s2_text;
        public GImage s3_disabled;
        public GImage s3_enabled;
        public GTextField s3_text;
        public GImage s4_disabled;
        public GImage s4_enabled;
        public GTextField s4_text;
        public G_guajishouyi_btn_xxjc_1 ShowDetailBtn;
        public GButton CloseBtn;
        public GList CanReceiveList;
        public GImage n80;
        public GTextField n81;
        public GGroup NotRewards;
        public GTextField n27;
        public GImage n26;
        public G_guajishouyi_btn_tisheng_1 ShowGainBtn;
        public GList RewardList;
        public GTextField n47;
        public GTextField FreeTimeTxt;
        public GButton FaskBattleBtn;
        public GTextField GetRewardTimeStr;
        public GTextField n73;
        public GTextField ExRemainTimesTxt;
        public GTextField n75;
        public GTextField n49;
        public GTextField IncomeTimeTxt;
        public GButton GetRewardBtn;
        public GImage n52;
        public GImage n53;
        public GImage n61;
        public GImage n62;
        public GList BuyReward;
        public GButton BuyBtn;
        public GImage n78;
        public GTextField n76;
        public GTextField RemainTimes;
        public GGroup ActiveGroup;
        public GImage n83;
        public GList BuffList;
        public GGroup Tips;
        public GGroup Window;
        public const string URL = "ui://pux1kqsbpb4z1w";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Main";

        public static G_OnHookView_Main CreateInstance()
        {
            return (G_OnHookView_Main)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n32 = (GImage)GetChildAt(2);
            n12 = (GImage)GetChildAt(3);
            n13 = (GImage)GetChildAt(4);
            s1_disabled = (GImage)GetChildAt(5);
            s1_enabled = (GImage)GetChildAt(6);
            s1_text = (GTextField)GetChildAt(7);
            s2_disabled = (GImage)GetChildAt(8);
            s2_enabled = (GImage)GetChildAt(9);
            s2_text = (GTextField)GetChildAt(10);
            s3_disabled = (GImage)GetChildAt(11);
            s3_enabled = (GImage)GetChildAt(12);
            s3_text = (GTextField)GetChildAt(13);
            s4_disabled = (GImage)GetChildAt(14);
            s4_enabled = (GImage)GetChildAt(15);
            s4_text = (GTextField)GetChildAt(16);
            ShowDetailBtn = (G_guajishouyi_btn_xxjc_1)GetChildAt(17);
            CloseBtn = (GButton)GetChildAt(18);
            CanReceiveList = (GList)GetChildAt(19);
            n80 = (GImage)GetChildAt(20);
            n81 = (GTextField)GetChildAt(21);
            NotRewards = (GGroup)GetChildAt(22);
            n27 = (GTextField)GetChildAt(23);
            n26 = (GImage)GetChildAt(24);
            ShowGainBtn = (G_guajishouyi_btn_tisheng_1)GetChildAt(25);
            RewardList = (GList)GetChildAt(26);
            n47 = (GTextField)GetChildAt(27);
            FreeTimeTxt = (GTextField)GetChildAt(28);
            FaskBattleBtn = (GButton)GetChildAt(29);
            GetRewardTimeStr = (GTextField)GetChildAt(30);
            n73 = (GTextField)GetChildAt(31);
            ExRemainTimesTxt = (GTextField)GetChildAt(32);
            n75 = (GTextField)GetChildAt(33);
            n49 = (GTextField)GetChildAt(34);
            IncomeTimeTxt = (GTextField)GetChildAt(35);
            GetRewardBtn = (GButton)GetChildAt(36);
            n52 = (GImage)GetChildAt(37);
            n53 = (GImage)GetChildAt(38);
            n61 = (GImage)GetChildAt(39);
            n62 = (GImage)GetChildAt(40);
            BuyReward = (GList)GetChildAt(41);
            BuyBtn = (GButton)GetChildAt(42);
            n78 = (GImage)GetChildAt(43);
            n76 = (GTextField)GetChildAt(44);
            RemainTimes = (GTextField)GetChildAt(45);
            ActiveGroup = (GGroup)GetChildAt(46);
            n83 = (GImage)GetChildAt(47);
            BuffList = (GList)GetChildAt(48);
            Tips = (GGroup)GetChildAt(49);
            Window = (GGroup)GetChildAt(50);
        }
    }
}