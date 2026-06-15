/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagTabItem : GButton
    {
        public Controller button;
        public GImage Image_bg;
        public GButton Image_select;
        public GLoader Image_icon;
        public GTextField Text_name;
        public GImage n21;
        public GImage n22;
        public GTextField Text_time;
        public GGroup group_time;
        public const string URL = "ui://iy58luyakiy43k";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagTabItem";

        public static G_BagTabItem CreateInstance()
        {
            return (G_BagTabItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_bg = (GImage)GetChildAt(0);
            Image_select = (GButton)GetChildAt(1);
            Image_icon = (GLoader)GetChildAt(2);
            Text_name = (GTextField)GetChildAt(3);
            n21 = (GImage)GetChildAt(4);
            n22 = (GImage)GetChildAt(5);
            Text_time = (GTextField)GetChildAt(6);
            group_time = (GGroup)GetChildAt(7);
        }
    }
}