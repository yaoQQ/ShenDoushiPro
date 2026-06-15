/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankTabItem : GButton
    {
        public Controller button;
        public GLoader Image_bg;
        public GImage Image_select;
        public GLoader Image_icon;
        public GTextField Text_name;
        public GComponent redPoint;
        public const string URL = "ui://fxqdojw7cwss1a";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankTabItem";

        public static G_RankTabItem CreateInstance()
        {
            return (G_RankTabItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_bg = (GLoader)GetChildAt(0);
            Image_select = (GImage)GetChildAt(1);
            Image_icon = (GLoader)GetChildAt(2);
            Text_name = (GTextField)GetChildAt(3);
            redPoint = (GComponent)GetChildAt(4);
        }
    }
}