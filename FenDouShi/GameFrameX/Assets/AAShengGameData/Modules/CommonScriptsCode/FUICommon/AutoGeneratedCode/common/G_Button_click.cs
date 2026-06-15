/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_Button_click : GButton
    {
        public GLoader icon;
        public const string URL = "ui://y4b7yuunhhlq2ien";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "Button_click";

        public static G_Button_click CreateInstance()
        {
            return (G_Button_click)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = (GLoader)GetChildAt(0);
        }
    }
}