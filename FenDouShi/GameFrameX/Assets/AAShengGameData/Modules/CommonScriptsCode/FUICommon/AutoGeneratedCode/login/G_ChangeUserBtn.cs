/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ChangeUserBtn : GButton
    {
        public GImage n20;
        public GTextField n22;
        public GImage n21;
        public const string URL = "ui://l64dumk9b2eri86";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ChangeUserBtn";

        public static G_ChangeUserBtn CreateInstance()
        {
            return (G_ChangeUserBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n20 = (GImage)GetChildAt(0);
            n22 = (GTextField)GetChildAt(1);
            n21 = (GImage)GetChildAt(2);
        }
    }
}