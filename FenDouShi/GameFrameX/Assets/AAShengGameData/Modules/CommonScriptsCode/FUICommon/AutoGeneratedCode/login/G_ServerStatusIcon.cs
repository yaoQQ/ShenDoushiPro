/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ServerStatusIcon : GComponent
    {
        public Controller c1;
        public GImage green;
        public GImage yellow;
        public GImage green_2;
        public GImage red;
        public const string URL = "ui://l64dumk9b2eri8c";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ServerStatusIcon";

        public static G_ServerStatusIcon CreateInstance()
        {
            return (G_ServerStatusIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            green = (GImage)GetChildAt(0);
            yellow = (GImage)GetChildAt(1);
            green_2 = (GImage)GetChildAt(2);
            red = (GImage)GetChildAt(3);
        }
    }
}