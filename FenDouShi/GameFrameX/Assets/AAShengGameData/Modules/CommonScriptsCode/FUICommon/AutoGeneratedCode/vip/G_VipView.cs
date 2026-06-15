/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_VipView : GComponent
    {
        public Controller rightTab;
        public GLoader n47;
        public GLoader n48;
        public GImage n49;
        public GImage n58;
        public GGroup bg;
        public GButton closeBtn;
        public G_RightTab n52;
        public G_RightTab n115;
        public GGroup rightTab_2;
        public G_ArowBtn leftArow;
        public G_ArowBtn rightArow;
        public GImage n11;
        public GImage n13;
        public GImage n14;
        public GImage n142;
        public GImage n16;
        public GImage n18;
        public GList luxuryList;
        public GTextField n21;
        public G_giftBuyBtn luxuryBtn;
        public GImage n6;
        public GImage n8;
        public GGroup luxuryReadyBuy;
        public GGroup luxury;
        public GImage n24;
        public GImage n26;
        public GImage n1471;
        public GImage n30;
        public GImage n1481;
        public GImage n1501;
        public GList exclusiveList;
        public G_giftBuyBtn exclusiveBtn;
        public GImage n7;
        public GImage n157;
        public GGroup exclusiveReadyBuy;
        public GGroup exclusive;
        public GGroup gift;
        public GTextField vipExpDescLabel;
        public G_vipExpProgress vipExpProgress;
        public GTextField vipExpLabel;
        public GImage n88;
        public GImage n89;
        public GTextField vipLevel;
        public GGroup vipTopGroup;
        public GTextField vipDescTitle;
        public GImage n64;
        public GList vipDescList;
        public GTextField n125;
        public GLoader discountIcon;
        public GTextField discountLabel;
        public GGroup con;
        public GImage line;
        public GGroup discountGroup;
        public GTextField nd;
        public GLoader normalIcon;
        public GTextField normalLabel;
        public GGroup normal;
        public GGroup priceGroup;
        public GList vipBuyRewardList;
        public GImage n43;
        public GTextField n44;
        public GGroup readyBuy;
        public G_guizu_btn_goumai_1 buyBtn;
        public GGroup vipBuyGroup;
        public GList monthRewardList;
        public GImage n42;
        public GImage n45;
        public GGroup monthRewardReadyGet;
        public GImage n110;
        public GTextField vipGetTips;
        public GImage n95;
        public GImage n96;
        public GButton getRewardBtn;
        public GGroup vipMonthGroup;
        public GGroup content;
        public const string URL = "ui://yizyzd76mv0c23";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "VipView";

        public static G_VipView CreateInstance()
        {
            return (G_VipView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            rightTab = GetControllerAt(0);
            n47 = (GLoader)GetChildAt(0);
            n48 = (GLoader)GetChildAt(1);
            n49 = (GImage)GetChildAt(2);
            n58 = (GImage)GetChildAt(3);
            bg = (GGroup)GetChildAt(4);
            closeBtn = (GButton)GetChildAt(5);
            n52 = (G_RightTab)GetChildAt(6);
            n115 = (G_RightTab)GetChildAt(7);
            rightTab_2 = (GGroup)GetChildAt(8);
            leftArow = (G_ArowBtn)GetChildAt(9);
            rightArow = (G_ArowBtn)GetChildAt(10);
            n11 = (GImage)GetChildAt(11);
            n13 = (GImage)GetChildAt(12);
            n14 = (GImage)GetChildAt(13);
            n142 = (GImage)GetChildAt(14);
            n16 = (GImage)GetChildAt(15);
            n18 = (GImage)GetChildAt(16);
            luxuryList = (GList)GetChildAt(17);
            n21 = (GTextField)GetChildAt(18);
            luxuryBtn = (G_giftBuyBtn)GetChildAt(19);
            n6 = (GImage)GetChildAt(20);
            n8 = (GImage)GetChildAt(21);
            luxuryReadyBuy = (GGroup)GetChildAt(22);
            luxury = (GGroup)GetChildAt(23);
            n24 = (GImage)GetChildAt(24);
            n26 = (GImage)GetChildAt(25);
            n1471 = (GImage)GetChildAt(26);
            n30 = (GImage)GetChildAt(27);
            n1481 = (GImage)GetChildAt(28);
            n1501 = (GImage)GetChildAt(29);
            exclusiveList = (GList)GetChildAt(30);
            exclusiveBtn = (G_giftBuyBtn)GetChildAt(31);
            n7 = (GImage)GetChildAt(32);
            n157 = (GImage)GetChildAt(33);
            exclusiveReadyBuy = (GGroup)GetChildAt(34);
            exclusive = (GGroup)GetChildAt(35);
            gift = (GGroup)GetChildAt(36);
            vipExpDescLabel = (GTextField)GetChildAt(37);
            vipExpProgress = (G_vipExpProgress)GetChildAt(38);
            vipExpLabel = (GTextField)GetChildAt(39);
            n88 = (GImage)GetChildAt(40);
            n89 = (GImage)GetChildAt(41);
            vipLevel = (GTextField)GetChildAt(42);
            vipTopGroup = (GGroup)GetChildAt(43);
            vipDescTitle = (GTextField)GetChildAt(44);
            n64 = (GImage)GetChildAt(45);
            vipDescList = (GList)GetChildAt(46);
            n125 = (GTextField)GetChildAt(47);
            discountIcon = (GLoader)GetChildAt(48);
            discountLabel = (GTextField)GetChildAt(49);
            con = (GGroup)GetChildAt(50);
            line = (GImage)GetChildAt(51);
            discountGroup = (GGroup)GetChildAt(52);
            nd = (GTextField)GetChildAt(53);
            normalIcon = (GLoader)GetChildAt(54);
            normalLabel = (GTextField)GetChildAt(55);
            normal = (GGroup)GetChildAt(56);
            priceGroup = (GGroup)GetChildAt(57);
            vipBuyRewardList = (GList)GetChildAt(58);
            n43 = (GImage)GetChildAt(59);
            n44 = (GTextField)GetChildAt(60);
            readyBuy = (GGroup)GetChildAt(61);
            buyBtn = (G_guizu_btn_goumai_1)GetChildAt(62);
            vipBuyGroup = (GGroup)GetChildAt(63);
            monthRewardList = (GList)GetChildAt(64);
            n42 = (GImage)GetChildAt(65);
            n45 = (GImage)GetChildAt(66);
            monthRewardReadyGet = (GGroup)GetChildAt(67);
            n110 = (GImage)GetChildAt(68);
            vipGetTips = (GTextField)GetChildAt(69);
            n95 = (GImage)GetChildAt(70);
            n96 = (GImage)GetChildAt(71);
            getRewardBtn = (GButton)GetChildAt(72);
            vipMonthGroup = (GGroup)GetChildAt(73);
            content = (GGroup)GetChildAt(74);
        }
    }
}