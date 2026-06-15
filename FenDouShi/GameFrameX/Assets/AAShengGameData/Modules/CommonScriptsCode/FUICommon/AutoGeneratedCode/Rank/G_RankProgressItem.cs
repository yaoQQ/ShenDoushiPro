/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankProgressItem : GComponent
    {
        public GImage Image_bg;
        public GTextField Text_desc;
        public GImage Image_get;
        public GTextField n9;
        public GGroup group_get;
        public GTextField Text_name;
        public G_RankHeadItem headItem;
        public GTextField Text_first;
        public GImage n105;
        public GButton Button_look;
        public GGroup group_info;
        public GTextField Text_empty;
        public GList list_reward;
        public const string URL = "ui://fxqdojw7eh0020";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankProgressItem";

        public static G_RankProgressItem CreateInstance()
        {
            return (G_RankProgressItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_bg = (GImage)GetChildAt(0);
            Text_desc = (GTextField)GetChildAt(1);
            Image_get = (GImage)GetChildAt(2);
            n9 = (GTextField)GetChildAt(3);
            group_get = (GGroup)GetChildAt(4);
            Text_name = (GTextField)GetChildAt(5);
            headItem = (G_RankHeadItem)GetChildAt(6);
            Text_first = (GTextField)GetChildAt(7);
            n105 = (GImage)GetChildAt(8);
            Button_look = (GButton)GetChildAt(9);
            group_info = (GGroup)GetChildAt(10);
            Text_empty = (GTextField)GetChildAt(11);
            list_reward = (GList)GetChildAt(12);
        }
    }
}