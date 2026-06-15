/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_Button_vedio : GButton
    {
        public GImage n33;
        public GTextField n34;
        public GImage n35;
        public const string URL = "ui://ooop1fy6gxoq1l";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "Button_vedio";

        public static G_Button_vedio CreateInstance()
        {
            return (G_Button_vedio)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n33 = (GImage)GetChildAt(0);
            n34 = (GTextField)GetChildAt(1);
            n35 = (GImage)GetChildAt(2);
        }
    }
}