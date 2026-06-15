/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_noticeTab : GButton
    {
        public Controller button;
        public GImage n32;
        public GImage n41;
        public GImage n40;
        public GTextField title;
        public GImage n42;
        public const string URL = "ui://l64dumk9mm2ii96";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "noticeTab";

        public static G_noticeTab CreateInstance()
        {
            return (G_noticeTab)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n32 = (GImage)GetChildAt(0);
            n41 = (GImage)GetChildAt(1);
            n40 = (GImage)GetChildAt(2);
            title = (GTextField)GetChildAt(3);
            n42 = (GImage)GetChildAt(4);
        }
    }
}