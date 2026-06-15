/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ageBtn : GButton
    {
        public GImage n8;
        public const string URL = "ui://l64dumk9mm2ii9c";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ageBtn";

        public static G_ageBtn CreateInstance()
        {
            return (G_ageBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n8 = (GImage)GetChildAt(0);
        }
    }
}