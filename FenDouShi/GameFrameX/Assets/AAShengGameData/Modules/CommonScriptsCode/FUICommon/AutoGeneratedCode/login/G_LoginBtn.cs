/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_LoginBtn : GButton
    {
        public GImage n4;
        public GTextField n3;
        public const string URL = "ui://l64dumk9b2eri83";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "LoginBtn";

        public static G_LoginBtn CreateInstance()
        {
            return (G_LoginBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GImage)GetChildAt(0);
            n3 = (GTextField)GetChildAt(1);
        }
    }
}