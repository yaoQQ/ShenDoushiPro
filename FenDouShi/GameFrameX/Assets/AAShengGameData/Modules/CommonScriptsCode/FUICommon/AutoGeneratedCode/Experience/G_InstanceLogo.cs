/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Experience
{
    public partial class G_InstanceLogo : GComponent
    {
        public Controller c1;
        public GImage n64;
        public G_YaDianNaDungeon n65;
        public const string URL = "ui://neqo767fq1gdq";
        public const string PACKAGE_NAME = "Experience";
        public const string COMPONENT_NAME = "InstanceLogo";

        public static G_InstanceLogo CreateInstance()
        {
            return (G_InstanceLogo)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            n64 = (GImage)GetChildAt(0);
            n65 = (G_YaDianNaDungeon)GetChildAt(1);
        }
    }
}