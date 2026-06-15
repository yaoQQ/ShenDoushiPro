/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_Button_look : GButton
    {
        public GImage n83;
        public GImage n84;
        public const string URL = "ui://ooop1fy6gxoq21";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "Button_look";

        public static G_Button_look CreateInstance()
        {
            return (G_Button_look)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n83 = (GImage)GetChildAt(0);
            n84 = (GImage)GetChildAt(1);
        }
    }
}