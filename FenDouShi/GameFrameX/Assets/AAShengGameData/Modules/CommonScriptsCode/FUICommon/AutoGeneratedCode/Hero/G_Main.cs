/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hero
{
    public partial class G_Main : GComponent
    {
        public GImage n99;
        public G_kaipai_btn_xinxi_1 n3;
        public G_kaipai_btn_fanhui_1 closeButton;
        public GTextField n5;
        public GList heroList;
        public G_kaipai_btn_haogan_1 n73;
        public G_kaipai_btn_tuijian_1 n74;
        public G_kaipai_btn_tujian_1 n75;
        public G_kaipai_btn_zhuzhan_1 n76;
        public G_kaipai_btn_buzhen_1 n77;
        public GTextField n78;
        public GTextField n79;
        public GTextField n80;
        public GTextField n81;
        public GTextField n82;
        public GImage n102;
        public GImage n85;
        public G_kaipai_btn_jia_1 n86;
        public G_kaipai_btn_shuaixuan_1 n89;
        public GTextField n90;
        public GTextField n87;
        public const string URL = "ui://0g1kba0btae91i";
        public const string PACKAGE_NAME = "Hero";
        public const string COMPONENT_NAME = "Main";

        public static G_Main CreateInstance()
        {
            return (G_Main)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n99 = (GImage)GetChildAt(0);
            n3 = (G_kaipai_btn_xinxi_1)GetChildAt(1);
            closeButton = (G_kaipai_btn_fanhui_1)GetChildAt(2);
            n5 = (GTextField)GetChildAt(3);
            heroList = (GList)GetChildAt(4);
            n73 = (G_kaipai_btn_haogan_1)GetChildAt(5);
            n74 = (G_kaipai_btn_tuijian_1)GetChildAt(6);
            n75 = (G_kaipai_btn_tujian_1)GetChildAt(7);
            n76 = (G_kaipai_btn_zhuzhan_1)GetChildAt(8);
            n77 = (G_kaipai_btn_buzhen_1)GetChildAt(9);
            n78 = (GTextField)GetChildAt(10);
            n79 = (GTextField)GetChildAt(11);
            n80 = (GTextField)GetChildAt(12);
            n81 = (GTextField)GetChildAt(13);
            n82 = (GTextField)GetChildAt(14);
            n102 = (GImage)GetChildAt(15);
            n85 = (GImage)GetChildAt(16);
            n86 = (G_kaipai_btn_jia_1)GetChildAt(17);
            n89 = (G_kaipai_btn_shuaixuan_1)GetChildAt(18);
            n90 = (GTextField)GetChildAt(19);
            n87 = (GTextField)GetChildAt(20);
        }
    }
}