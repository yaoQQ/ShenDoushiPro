/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_AreaItem : GButton
    {
        public Controller button;
        public Controller icon;
        public GImage n7;
        public GImage n11;
        public GTextField title;
        public GImage n9;
        public GImage icon_2;
        public GImage n12;
        public GImage n13;
        public GTextField num;
        public const string URL = "ui://l64dumk9b2eri89";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "AreaItem";

        public static G_AreaItem CreateInstance()
        {
            return (G_AreaItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            icon = GetControllerAt(1);
            n7 = (GImage)GetChildAt(0);
            n11 = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
            n9 = (GImage)GetChildAt(3);
            icon_2 = (GImage)GetChildAt(4);
            n12 = (GImage)GetChildAt(5);
            n13 = (GImage)GetChildAt(6);
            num = (GTextField)GetChildAt(7);
        }
    }
}