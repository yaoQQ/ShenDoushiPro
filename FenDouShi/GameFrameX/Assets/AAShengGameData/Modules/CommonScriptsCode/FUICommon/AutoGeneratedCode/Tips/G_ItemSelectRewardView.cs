/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemSelectRewardView : GComponent
    {
        public GImage n5;
        public GImage n6;
        public GImage n8;
        public GImage n9;
        public GImage n10;
        public GButton Button_close;
        public GGroup 小弹窗底板;
        public GTextField Text_title;
        public GImage n68;
        public GTextInput Text_input;
        public G_common_btn_gongnengdi01_3 Button_sub10;
        public G_common_btn_gongnengdi01_1 Button_sub;
        public G_common_btn_gongnengdi01_1 Button_add;
        public G_common_btn_gongnengdi01_3 Button_add10;
        public G_common_btn_gongnengdi01_1 Button_max;
        public GList list_reward;
        public GButton Button_use;
        public GTextField Text_remain;
        public GGroup Frame;
        public const string URL = "ui://g2ec8shvsjco2ib0";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemSelectRewardView";

        public static G_ItemSelectRewardView CreateInstance()
        {
            return (G_ItemSelectRewardView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n5 = (GImage)GetChildAt(0);
            n6 = (GImage)GetChildAt(1);
            n8 = (GImage)GetChildAt(2);
            n9 = (GImage)GetChildAt(3);
            n10 = (GImage)GetChildAt(4);
            Button_close = (GButton)GetChildAt(5);
            小弹窗底板 = (GGroup)GetChildAt(6);
            Text_title = (GTextField)GetChildAt(7);
            n68 = (GImage)GetChildAt(8);
            Text_input = (GTextInput)GetChildAt(9);
            Button_sub10 = (G_common_btn_gongnengdi01_3)GetChildAt(10);
            Button_sub = (G_common_btn_gongnengdi01_1)GetChildAt(11);
            Button_add = (G_common_btn_gongnengdi01_1)GetChildAt(12);
            Button_add10 = (G_common_btn_gongnengdi01_3)GetChildAt(13);
            Button_max = (G_common_btn_gongnengdi01_1)GetChildAt(14);
            list_reward = (GList)GetChildAt(15);
            Button_use = (GButton)GetChildAt(16);
            Text_remain = (GTextField)GetChildAt(17);
            Frame = (GGroup)GetChildAt(18);
        }
    }
}