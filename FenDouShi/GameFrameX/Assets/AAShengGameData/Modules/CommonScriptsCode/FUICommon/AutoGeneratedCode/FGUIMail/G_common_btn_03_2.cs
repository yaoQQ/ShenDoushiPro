/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_common_btn_03_2 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://p4ru1eicswrc15";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "common_btn_03_2";

        public static G_common_btn_03_2 CreateInstance()
        {
            return (G_common_btn_03_2)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}