/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_SystemTabItem : GButton
    {
        public Controller button;
        public GLoader Image_bg;
        public GLoader Image_select;
        public GLoader Image_icon;
        public GImage Image_finish;
        public GImage n60;
        public GComponent redPoint;
        public GTextField Text_name;
        public GTextField Text_limit;
        public GGroup n69;
        public const string URL = "ui://i3fvjlvyk00yf";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "SystemTabItem";

        public static G_SystemTabItem CreateInstance()
        {
            return (G_SystemTabItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_bg = (GLoader)GetChildAt(0);
            Image_select = (GLoader)GetChildAt(1);
            Image_icon = (GLoader)GetChildAt(2);
            Image_finish = (GImage)GetChildAt(3);
            n60 = (GImage)GetChildAt(4);
            redPoint = (GComponent)GetChildAt(5);
            Text_name = (GTextField)GetChildAt(6);
            Text_limit = (GTextField)GetChildAt(7);
            n69 = (GGroup)GetChildAt(8);
        }
    }
}