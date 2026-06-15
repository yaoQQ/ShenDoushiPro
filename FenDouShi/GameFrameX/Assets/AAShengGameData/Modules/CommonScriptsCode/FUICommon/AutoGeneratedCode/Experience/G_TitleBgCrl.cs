/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Experience
{
    public partial class G_TitleBgCrl : GComponent
    {
        public Controller c1;
        public GImage nameBgControl;
        public GImage n61;
        public GImage n62;
        public GImage n63;
        public const string URL = "ui://neqo767fq1gds";
        public const string PACKAGE_NAME = "Experience";
        public const string COMPONENT_NAME = "TitleBgCrl";

        public static G_TitleBgCrl CreateInstance()
        {
            return (G_TitleBgCrl)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            nameBgControl = (GImage)GetChildAt(0);
            n61 = (GImage)GetChildAt(1);
            n62 = (GImage)GetChildAt(2);
            n63 = (GImage)GetChildAt(3);
        }
    }
}