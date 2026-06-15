/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_okBtn : GButton
    {
        public GImage n12;
        public GTextField n11;
        public const string URL = "ui://l64dumk9mm2ii8n";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "okBtn";

        public static G_okBtn CreateInstance()
        {
            return (G_okBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n12 = (GImage)GetChildAt(0);
            n11 = (GTextField)GetChildAt(1);
        }
    }
}