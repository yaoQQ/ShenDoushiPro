/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemBoxRandomTipsView : GComponent
    {
        public GButton n44;
        public GImage n4;
        public GImage n5;
        public GImage n7;
        public GImage n8;
        public GImage n9;
        public GButton Button_close;
        public GTextField n12;
        public GTextField Text_name;
        public GTextField Text_num;
        public GTextField Text_desc;
        public GButton goodItem;
        public GButton Button_getWay;
        public GTextField Text0;
        public GGroup group_getWay;
        public GList list_reward;
        public GButton Button_rareClose;
        public GImage n26;
        public GList list_rateShow;
        public GGroup group_rate;
        public const string URL = "ui://g2ec8shvixvja";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemBoxRandomTipsView";

        public static G_ItemBoxRandomTipsView CreateInstance()
        {
            return (G_ItemBoxRandomTipsView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n44 = (GButton)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n5 = (GImage)GetChildAt(2);
            n7 = (GImage)GetChildAt(3);
            n8 = (GImage)GetChildAt(4);
            n9 = (GImage)GetChildAt(5);
            Button_close = (GButton)GetChildAt(6);
            n12 = (GTextField)GetChildAt(7);
            Text_name = (GTextField)GetChildAt(8);
            Text_num = (GTextField)GetChildAt(9);
            Text_desc = (GTextField)GetChildAt(10);
            goodItem = (GButton)GetChildAt(11);
            Button_getWay = (GButton)GetChildAt(12);
            Text0 = (GTextField)GetChildAt(13);
            group_getWay = (GGroup)GetChildAt(14);
            list_reward = (GList)GetChildAt(15);
            Button_rareClose = (GButton)GetChildAt(16);
            n26 = (GImage)GetChildAt(17);
            list_rateShow = (GList)GetChildAt(18);
            group_rate = (GGroup)GetChildAt(19);
        }
    }
}