/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace launch
{
    public partial class G_ageCloseBtn : GButton
    {
        public Controller button;
        public GImage n2;
        public const string URL = "ui://ynb47g4ju5vmi5p";
        public const string PACKAGE_NAME = "launch";
        public const string COMPONENT_NAME = "ageCloseBtn";

        public static G_ageCloseBtn CreateInstance()
        {
            return (G_ageCloseBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n2 = (GImage)GetChildAt(0);
        }
    }
}