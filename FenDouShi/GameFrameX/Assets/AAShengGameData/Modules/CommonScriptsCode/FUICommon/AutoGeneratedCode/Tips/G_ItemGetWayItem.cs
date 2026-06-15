/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemGetWayItem : GComponent
    {
        public GImage Image_bg;
        public GButton Button_getway;
        public GImage n126;
        public GTextField Text_name;
        public GGroup group_info;
        public GTextField Text_desc;
        public GGroup group_getway;
        public GGroup ItemGetWayitem;
        public const string URL = "ui://g2ec8shvkiy42iao";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemGetWayItem";

        public static G_ItemGetWayItem CreateInstance()
        {
            return (G_ItemGetWayItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_bg = (GImage)GetChildAt(0);
            Button_getway = (GButton)GetChildAt(1);
            n126 = (GImage)GetChildAt(2);
            Text_name = (GTextField)GetChildAt(3);
            group_info = (GGroup)GetChildAt(4);
            Text_desc = (GTextField)GetChildAt(5);
            group_getway = (GGroup)GetChildAt(6);
            ItemGetWayitem = (GGroup)GetChildAt(7);
        }
    }
}