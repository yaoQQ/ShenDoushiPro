/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_Button_box : GButton
    {
        public GLoader icon;
        public const string URL = "ui://ooop1fy6gxoqq";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "Button_box";

        public static G_Button_box CreateInstance()
        {
            return (G_Button_box)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = (GLoader)GetChildAt(0);
        }
    }
}