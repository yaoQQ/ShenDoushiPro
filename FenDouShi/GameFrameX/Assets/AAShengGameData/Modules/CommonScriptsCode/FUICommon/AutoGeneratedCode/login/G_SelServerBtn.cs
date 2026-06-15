/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_selserverBtn : GButton
    {
        public GImage n27;
        public GTextField loginAccount;
        public GTextField n30;
        public G_ServerStatusIcon ServerStatueIcon;
        public const string URL = "ui://l64dumk9b2eri88";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "selserverBtn";

        public static G_selserverBtn CreateInstance()
        {
            return (G_selserverBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n27 = (GImage)GetChildAt(0);
            loginAccount = (GTextField)GetChildAt(1);
            n30 = (GTextField)GetChildAt(2);
            ServerStatueIcon = (G_ServerStatusIcon)GetChildAt(3);
        }
    }
}