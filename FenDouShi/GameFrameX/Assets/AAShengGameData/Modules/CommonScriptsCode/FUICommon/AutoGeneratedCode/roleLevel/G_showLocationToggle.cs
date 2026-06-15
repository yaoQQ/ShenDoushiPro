/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_showLocationToggle : GButton
    {
        public Controller button;
        public GImage n75;
        public GImage n76;
        public GImage n77;
        public const string URL = "ui://oz2qb5cgppy63a";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "showLocationToggle";

        public static G_showLocationToggle CreateInstance()
        {
            return (G_showLocationToggle)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n75 = (GImage)GetChildAt(0);
            n76 = (GImage)GetChildAt(1);
            n77 = (GImage)GetChildAt(2);
        }
    }
}