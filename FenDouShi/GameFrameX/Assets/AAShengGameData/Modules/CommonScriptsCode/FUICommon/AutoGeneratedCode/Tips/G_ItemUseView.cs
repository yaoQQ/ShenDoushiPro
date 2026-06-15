/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemUseView : GComponent
    {
        public GImage n5;
        public GImage n6;
        public GImage n8;
        public GImage n9;
        public GImage n10;
        public GButton Button_close;
        public GGroup 小弹窗底板;
        public GImage n68;
        public GTextInput Text_input;
        public GTextField Text_title;
        public GButton Button_use;
        public G_common_btn_gongnengdi01_3 Button_sub10;
        public G_common_btn_gongnengdi01_1 Button_sub;
        public G_common_btn_gongnengdi01_1 Button_add;
        public G_common_btn_gongnengdi01_3 Button_add10;
        public G_common_btn_gongnengdi01_1 Button_max;
        public GTextField Text_name;
        public GTextField Text_count;
        public GButton goodItem;
        public GGroup group_item;
        public GTextField n111;
        public GList list_reward;
        public GGroup group_reward;
        public GGroup content;
        public GGroup Frame;
        public const string URL = "ui://g2ec8shvqc4df";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemUseView";

        public static G_ItemUseView CreateInstance()
        {
            return (G_ItemUseView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
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
            n68 = (GImage)GetChildAt(7);
            Text_input = (GTextInput)GetChildAt(8);
            Text_title = (GTextField)GetChildAt(9);
            Button_use = (GButton)GetChildAt(10);
            Button_sub10 = (G_common_btn_gongnengdi01_3)GetChildAt(11);
            Button_sub = (G_common_btn_gongnengdi01_1)GetChildAt(12);
            Button_add = (G_common_btn_gongnengdi01_1)GetChildAt(13);
            Button_add10 = (G_common_btn_gongnengdi01_3)GetChildAt(14);
            Button_max = (G_common_btn_gongnengdi01_1)GetChildAt(15);
            Text_name = (GTextField)GetChildAt(16);
            Text_count = (GTextField)GetChildAt(17);
            goodItem = (GButton)GetChildAt(18);
            group_item = (GGroup)GetChildAt(19);
            n111 = (GTextField)GetChildAt(20);
            list_reward = (GList)GetChildAt(21);
            group_reward = (GGroup)GetChildAt(22);
            content = (GGroup)GetChildAt(23);
            Frame = (GGroup)GetChildAt(24);
        }
    }
}