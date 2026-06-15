/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankBannerItem : GComponent
    {
        public GLoader Image_quality;
        public GLoader Image_type;
        public GLoader Image_strType;
        public G_RankHeadItem headItem;
        public GImage n44;
        public GTextField Text_name;
        public GLoader Image_typeSmall;
        public GTextField Text_fight;
        public GGroup group_fight;
        public GGroup group_info;
        public GTextField Text_empty;
        public const string URL = "ui://fxqdojw7hhlq1e";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankBannerItem";

        public static G_RankBannerItem CreateInstance()
        {
            return (G_RankBannerItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_quality = (GLoader)GetChildAt(0);
            Image_type = (GLoader)GetChildAt(1);
            Image_strType = (GLoader)GetChildAt(2);
            headItem = (G_RankHeadItem)GetChildAt(3);
            n44 = (GImage)GetChildAt(4);
            Text_name = (GTextField)GetChildAt(5);
            Image_typeSmall = (GLoader)GetChildAt(6);
            Text_fight = (GTextField)GetChildAt(7);
            group_fight = (GGroup)GetChildAt(8);
            group_info = (GGroup)GetChildAt(9);
            Text_empty = (GTextField)GetChildAt(10);
        }
    }
}