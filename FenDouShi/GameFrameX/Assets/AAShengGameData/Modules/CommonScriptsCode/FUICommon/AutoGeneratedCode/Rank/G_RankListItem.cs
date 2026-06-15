/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankListItem : GComponent
    {
        public GLoader Image_bg;
        public G_RankHeadItem headItem;
        public GTextField Text_name;
        public GLoader Image_title;
        public GTextField Text_fight;
        public GImage Image_line;
        public GImage Image_rankbg;
        public GTextField Text_rank;
        public GGroup group_rankNum;
        public GLoader Image_rank;
        public GButton Button_like;
        public GTextField Text_likeNum;
        public GGroup group_like;
        public const string URL = "ui://fxqdojw7hhlq1m";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankListItem";

        public static G_RankListItem CreateInstance()
        {
            return (G_RankListItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_bg = (GLoader)GetChildAt(0);
            headItem = (G_RankHeadItem)GetChildAt(1);
            Text_name = (GTextField)GetChildAt(2);
            Image_title = (GLoader)GetChildAt(3);
            Text_fight = (GTextField)GetChildAt(4);
            Image_line = (GImage)GetChildAt(5);
            Image_rankbg = (GImage)GetChildAt(6);
            Text_rank = (GTextField)GetChildAt(7);
            group_rankNum = (GGroup)GetChildAt(8);
            Image_rank = (GLoader)GetChildAt(9);
            Button_like = (GButton)GetChildAt(10);
            Text_likeNum = (GTextField)GetChildAt(11);
            group_like = (GGroup)GetChildAt(12);
        }
    }
}