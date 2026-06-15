/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_common_btn_01_1 : GButton
    {
        public Controller button;
        public GImage icon;
        public GTextField title;
        public const string URL = "ui://y4b7yuunswrc2i7z";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "common_btn_01_1";

        public static G_common_btn_01_1 CreateInstance()
        {
            return (G_common_btn_01_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            icon = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
        }
    }
}