/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_VipBuyTipsView : GComponent
    {
        public GImage bg;
        public GImage n7;
        public GImage n9;
        public GImage n11;
        public GTextField titlelabel;
        public GImage n33;
        public GRichTextField tipsContentLabel;
        public GButton cancleBtn;
        public GButton buyBtn;
        public GImage n22;
        public GList rewardLIst;
        public GGroup node;
        public const string URL = "ui://yizyzd76mv0c3g";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "VipBuyTipsView";

        public static G_VipBuyTipsView CreateInstance()
        {
            return (G_VipBuyTipsView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            n7 = (GImage)GetChildAt(1);
            n9 = (GImage)GetChildAt(2);
            n11 = (GImage)GetChildAt(3);
            titlelabel = (GTextField)GetChildAt(4);
            n33 = (GImage)GetChildAt(5);
            tipsContentLabel = (GRichTextField)GetChildAt(6);
            cancleBtn = (GButton)GetChildAt(7);
            buyBtn = (GButton)GetChildAt(8);
            n22 = (GImage)GetChildAt(9);
            rewardLIst = (GList)GetChildAt(10);
            node = (GGroup)GetChildAt(11);
        }
    }
}