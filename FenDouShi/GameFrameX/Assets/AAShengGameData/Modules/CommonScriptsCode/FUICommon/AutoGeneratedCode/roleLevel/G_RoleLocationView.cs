/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLocationView : GComponent
    {
        public GImage n77;
        public GImage n2;
        public GImage n4;
        public GImage n6;
        public GImage n7;
        public GImage n8;
        public GTextField localTitle;
        public GTextField title;
        public GTextField n52;
        public GButton closeBtn;
        public G_showLocationToggle showLocationToggle;
        public GButton okBtn;
        public GImage n53;
        public GTextField tipsText;
        public GGroup hideLocal;
        public GImage n13;
        public GTextField myLocalTitle;
        public GTextField locationText;
        public G_AutolocationBtn autoLocationBtn;
        public GButton SelectListBtn;
        public GImage ListBg;
        public GList locationList;
        public GGroup showLocal;
        public GGroup n73;
        public const string URL = "ui://oz2qb5cgi1yta";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLocationView";

        public static G_RoleLocationView CreateInstance()
        {
            return (G_RoleLocationView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n77 = (GImage)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            n4 = (GImage)GetChildAt(2);
            n6 = (GImage)GetChildAt(3);
            n7 = (GImage)GetChildAt(4);
            n8 = (GImage)GetChildAt(5);
            localTitle = (GTextField)GetChildAt(6);
            title = (GTextField)GetChildAt(7);
            n52 = (GTextField)GetChildAt(8);
            closeBtn = (GButton)GetChildAt(9);
            showLocationToggle = (G_showLocationToggle)GetChildAt(10);
            okBtn = (GButton)GetChildAt(11);
            n53 = (GImage)GetChildAt(12);
            tipsText = (GTextField)GetChildAt(13);
            hideLocal = (GGroup)GetChildAt(14);
            n13 = (GImage)GetChildAt(15);
            myLocalTitle = (GTextField)GetChildAt(16);
            locationText = (GTextField)GetChildAt(17);
            autoLocationBtn = (G_AutolocationBtn)GetChildAt(18);
            SelectListBtn = (GButton)GetChildAt(19);
            ListBg = (GImage)GetChildAt(20);
            locationList = (GList)GetChildAt(21);
            showLocal = (GGroup)GetChildAt(22);
            n73 = (GGroup)GetChildAt(23);
        }
    }
}