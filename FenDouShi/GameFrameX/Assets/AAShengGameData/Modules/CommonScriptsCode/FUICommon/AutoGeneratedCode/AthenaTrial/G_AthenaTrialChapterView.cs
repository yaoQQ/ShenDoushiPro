/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialChapterView : GComponent
    {
        public GImage n111;
        public G_Button_shop Button_shop;
        public G_Button_rank Button_rank;
        public GImage n16;
        public GImage n20;
        public GImage n93;
        public GImage n94;
        public GImage n95;
        public GImage n96;
        public GTextField Text_guanqia;
        public GImage n37;
        public GImage Image_slider;
        public GImage Image_point;
        public GImage n72;
        public GList List_reward;
        public GList List_progress;
        public GImage n80;
        public GTextField Text_fight;
        public GImage n82;
        public G_Button_look Button_look;
        public G_Button_box Button_box;
        public GTextField Text_remainCnt;
        public GButton Button_buy;
        public GGroup group_time;
        public GButton Button_sweep;
        public G_common_btn_01_lan_1 Button_fight;
        public GGroup group_button;
        public GGroup frame;
        public const string URL = "ui://ooop1fy6wlj71v";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialChapterView";

        public static G_AthenaTrialChapterView CreateInstance()
        {
            return (G_AthenaTrialChapterView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n111 = (GImage)GetChildAt(0);
            Button_shop = (G_Button_shop)GetChildAt(1);
            Button_rank = (G_Button_rank)GetChildAt(2);
            n16 = (GImage)GetChildAt(3);
            n20 = (GImage)GetChildAt(4);
            n93 = (GImage)GetChildAt(5);
            n94 = (GImage)GetChildAt(6);
            n95 = (GImage)GetChildAt(7);
            n96 = (GImage)GetChildAt(8);
            Text_guanqia = (GTextField)GetChildAt(9);
            n37 = (GImage)GetChildAt(10);
            Image_slider = (GImage)GetChildAt(11);
            Image_point = (GImage)GetChildAt(12);
            n72 = (GImage)GetChildAt(13);
            List_reward = (GList)GetChildAt(14);
            List_progress = (GList)GetChildAt(15);
            n80 = (GImage)GetChildAt(16);
            Text_fight = (GTextField)GetChildAt(17);
            n82 = (GImage)GetChildAt(18);
            Button_look = (G_Button_look)GetChildAt(19);
            Button_box = (G_Button_box)GetChildAt(20);
            Text_remainCnt = (GTextField)GetChildAt(21);
            Button_buy = (GButton)GetChildAt(22);
            group_time = (GGroup)GetChildAt(23);
            Button_sweep = (GButton)GetChildAt(24);
            Button_fight = (G_common_btn_01_lan_1)GetChildAt(25);
            group_button = (GGroup)GetChildAt(26);
            frame = (GGroup)GetChildAt(27);
        }
    }
}