/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_RandomBtn : GButton
    {
        public GImage n29;
        public const string URL = "ui://l64dumk9tz0lia8";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "RandomBtn";

        public static G_RandomBtn CreateInstance()
        {
            return (G_RandomBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n29 = (GImage)GetChildAt(0);
        }
    }
}