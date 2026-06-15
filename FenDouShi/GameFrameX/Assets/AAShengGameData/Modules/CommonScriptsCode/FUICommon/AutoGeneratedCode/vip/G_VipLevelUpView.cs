/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_VipLevelUpView : GComponent
    {
        public G_RewardBgCompoent frameBg;
        public GTextField vip1;
        public GImage n51;
        public GTextField vip2;
        public GList descList;
        public GImage n17;
        public GImage n66;
        public GTextField n19;
        public GGroup tqlb;
        public GButton goBuyBtn;
        public GList rewardList;
        public GGroup content;
        public GImage n70;
        public GImage n71;
        public GTextField n72;
        public GGroup closeTips;
        public GGroup node;
        public const string URL = "ui://yizyzd76mv0c2x";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "VipLevelUpView";

        public static G_VipLevelUpView CreateInstance()
        {
            return (G_VipLevelUpView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frameBg = (G_RewardBgCompoent)GetChildAt(0);
            vip1 = (GTextField)GetChildAt(1);
            n51 = (GImage)GetChildAt(2);
            vip2 = (GTextField)GetChildAt(3);
            descList = (GList)GetChildAt(4);
            n17 = (GImage)GetChildAt(5);
            n66 = (GImage)GetChildAt(6);
            n19 = (GTextField)GetChildAt(7);
            tqlb = (GGroup)GetChildAt(8);
            goBuyBtn = (GButton)GetChildAt(9);
            rewardList = (GList)GetChildAt(10);
            content = (GGroup)GetChildAt(11);
            n70 = (GImage)GetChildAt(12);
            n71 = (GImage)GetChildAt(13);
            n72 = (GTextField)GetChildAt(14);
            closeTips = (GGroup)GetChildAt(15);
            node = (GGroup)GetChildAt(16);
        }
    }
}