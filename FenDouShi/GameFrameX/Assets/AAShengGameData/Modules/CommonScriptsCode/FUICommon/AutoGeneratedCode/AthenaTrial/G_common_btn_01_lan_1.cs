/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_common_btn_01_lan_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public GTextField title;
        public const string URL = "ui://ooop1fy6wlj77";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "common_btn_01_lan_1";

        public static G_common_btn_01_lan_1 CreateInstance()
        {
            return (G_common_btn_01_lan_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
        }
    }
}