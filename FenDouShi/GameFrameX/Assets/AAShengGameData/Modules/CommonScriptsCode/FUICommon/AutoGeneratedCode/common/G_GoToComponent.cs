/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_GoToComponent : GButton
    {
        public GImage n129;
        public GTextField n123;
        public GImage n125;
        public const string URL = "ui://y4b7yuunkiy42iav";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "GoToComponent";

        public static G_GoToComponent CreateInstance()
        {
            return (G_GoToComponent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n129 = (GImage)GetChildAt(0);
            n123 = (GTextField)GetChildAt(1);
            n125 = (GImage)GetChildAt(2);
        }
    }
}