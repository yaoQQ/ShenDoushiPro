/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankProgressTabItem : GButton
    {
        public Controller button;
        public GImage Image_line;
        public GImage Image_select;
        public GTextField Text_name;
        public GTextField Text_nameSelect;
        public GComponent redPoint;
        public const string URL = "ui://fxqdojw7hhlq1o";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankProgressTabItem";

        public static G_RankProgressTabItem CreateInstance()
        {
            return (G_RankProgressTabItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_line = (GImage)GetChildAt(0);
            Image_select = (GImage)GetChildAt(1);
            Text_name = (GTextField)GetChildAt(2);
            Text_nameSelect = (GTextField)GetChildAt(3);
            redPoint = (GComponent)GetChildAt(4);
        }
    }
}