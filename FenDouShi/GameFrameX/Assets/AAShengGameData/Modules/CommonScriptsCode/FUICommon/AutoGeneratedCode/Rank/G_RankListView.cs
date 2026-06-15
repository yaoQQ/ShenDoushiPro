/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankListView : GComponent
    {
        public GImage n2;
        public GButton Button_close;
        public GTextField Text_title;
        public G_common_btn_yeqian02_line02_1 n158;
        public G_common_btn_yeqian02_line02_1 n159;
        public G_common_btn_yeqian02_line01_1 n157;
        public GTextField n8;
        public GTextField n9;
        public GTextField n10;
        public GImage n76;
        public GTextField Text_time;
        public GGroup group_time;
        public GImage Image_quality;
        public GLoader Image_role;
        public GImage n120;
        public GImage n121;
        public GTextField Text_name;
        public GImage n124;
        public GLoader Image_title;
        public GLoader Iamge_rank;
        public GLoader Image_typeSmall;
        public GTextField Text_fight;
        public GGroup group_type;
        public GGroup group_info;
        public G_Button_reward Button_reward;
        public G_RankListItem mRank;
        public GList List_rank;
        public GGroup root;
        public GImage n154;
        public GTextField n155;
        public GGroup group_empty;
        public const string URL = "ui://fxqdojw7qnl91y";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankListView";

        public static G_RankListView CreateInstance()
        {
            return (G_RankListView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            Button_close = (GButton)GetChildAt(1);
            Text_title = (GTextField)GetChildAt(2);
            n158 = (G_common_btn_yeqian02_line02_1)GetChildAt(3);
            n159 = (G_common_btn_yeqian02_line02_1)GetChildAt(4);
            n157 = (G_common_btn_yeqian02_line01_1)GetChildAt(5);
            n8 = (GTextField)GetChildAt(6);
            n9 = (GTextField)GetChildAt(7);
            n10 = (GTextField)GetChildAt(8);
            n76 = (GImage)GetChildAt(9);
            Text_time = (GTextField)GetChildAt(10);
            group_time = (GGroup)GetChildAt(11);
            Image_quality = (GImage)GetChildAt(12);
            Image_role = (GLoader)GetChildAt(13);
            n120 = (GImage)GetChildAt(14);
            n121 = (GImage)GetChildAt(15);
            Text_name = (GTextField)GetChildAt(16);
            n124 = (GImage)GetChildAt(17);
            Image_title = (GLoader)GetChildAt(18);
            Iamge_rank = (GLoader)GetChildAt(19);
            Image_typeSmall = (GLoader)GetChildAt(20);
            Text_fight = (GTextField)GetChildAt(21);
            group_type = (GGroup)GetChildAt(22);
            group_info = (GGroup)GetChildAt(23);
            Button_reward = (G_Button_reward)GetChildAt(24);
            mRank = (G_RankListItem)GetChildAt(25);
            List_rank = (GList)GetChildAt(26);
            root = (GGroup)GetChildAt(27);
            n154 = (GImage)GetChildAt(28);
            n155 = (GTextField)GetChildAt(29);
            group_empty = (GGroup)GetChildAt(30);
        }
    }
}