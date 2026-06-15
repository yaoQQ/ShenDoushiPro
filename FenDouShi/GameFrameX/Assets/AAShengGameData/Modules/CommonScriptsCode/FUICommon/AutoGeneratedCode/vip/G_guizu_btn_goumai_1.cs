/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_guizu_btn_goumai_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://yizyzd76mv0c1d";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "guizu_btn_goumai_1";

        public static G_guizu_btn_goumai_1 CreateInstance()
        {
            return (G_guizu_btn_goumai_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}