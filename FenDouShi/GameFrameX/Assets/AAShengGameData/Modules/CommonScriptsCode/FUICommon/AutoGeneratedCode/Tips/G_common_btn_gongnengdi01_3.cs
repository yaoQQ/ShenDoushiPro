/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_common_btn_gongnengdi01_3 : GButton
    {
        public Controller button;
        public GImage n1;
        public GLoader icon;
        public GTextField title;
        public const string URL = "ui://g2ec8shvqc4d5";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "common_btn_gongnengdi01_3";

        public static G_common_btn_gongnengdi01_3 CreateInstance()
        {
            return (G_common_btn_gongnengdi01_3)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            icon = (GLoader)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}