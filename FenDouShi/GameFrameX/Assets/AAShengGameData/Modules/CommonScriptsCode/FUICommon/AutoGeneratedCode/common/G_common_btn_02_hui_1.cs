/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_common_btn_02_hui_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public GTextField title_disabled;
        public GGroup disabled;
        public GImage n2;
        public GTextField title_enabled;
        public GGroup up;
        public const string URL = "ui://y4b7yuunswrc2i7y";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "common_btn_02_hui_1";

        public static G_common_btn_02_hui_1 CreateInstance()
        {
            return (G_common_btn_02_hui_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            title_disabled = (GTextField)GetChildAt(1);
            disabled = (GGroup)GetChildAt(2);
            n2 = (GImage)GetChildAt(3);
            title_enabled = (GTextField)GetChildAt(4);
            up = (GGroup)GetChildAt(5);
        }
    }
}