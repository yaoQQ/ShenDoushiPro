/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankTopFiveItem : GComponent
    {
        public GImage n7;
        public GLoader Image_rank;
        public G_RankHeadItem headItem;
        public GImage n78;
        public GTextField Text_name;
        public GTextField Text_desc;
        public GTextField Text_rank;
        public const string URL = "ui://fxqdojw7eh0022";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankTopFiveItem";

        public static G_RankTopFiveItem CreateInstance()
        {
            return (G_RankTopFiveItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n7 = (GImage)GetChildAt(0);
            Image_rank = (GLoader)GetChildAt(1);
            headItem = (G_RankHeadItem)GetChildAt(2);
            n78 = (GImage)GetChildAt(3);
            Text_name = (GTextField)GetChildAt(4);
            Text_desc = (GTextField)GetChildAt(5);
            Text_rank = (GTextField)GetChildAt(6);
        }
    }
}