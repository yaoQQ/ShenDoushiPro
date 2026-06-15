/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagSelectionItem : GButton
    {
        public Controller button;
        public GImage n127;
        public GImage n128;
        public GImage n129;
        public GLoader Image_icon;
        public GTextField Text_All;
        public GImage Image_line;
        public GGroup BagSelectionItem;
        public const string URL = "ui://iy58luyasru53y";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagSelectionItem";

        public static G_BagSelectionItem CreateInstance()
        {
            return (G_BagSelectionItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n127 = (GImage)GetChildAt(0);
            n128 = (GImage)GetChildAt(1);
            n129 = (GImage)GetChildAt(2);
            Image_icon = (GLoader)GetChildAt(3);
            Text_All = (GTextField)GetChildAt(4);
            Image_line = (GImage)GetChildAt(5);
            BagSelectionItem = (GGroup)GetChildAt(6);
        }
    }
}