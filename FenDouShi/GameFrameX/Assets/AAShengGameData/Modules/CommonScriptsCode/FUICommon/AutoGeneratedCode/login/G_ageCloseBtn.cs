/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ageCloseBtn : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://l64dumk9mue61";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ageCloseBtn";

        public static G_ageCloseBtn CreateInstance()
        {
            return (G_ageCloseBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}