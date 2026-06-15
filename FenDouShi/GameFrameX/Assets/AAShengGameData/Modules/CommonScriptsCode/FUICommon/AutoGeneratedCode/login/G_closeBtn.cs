/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_closeBtn : GButton
    {
        public GImage n14;
        public GTextField n13;
        public const string URL = "ui://l64dumk9mm2ii8m";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "closeBtn";

        public static G_closeBtn CreateInstance()
        {
            return (G_closeBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n14 = (GImage)GetChildAt(0);
            n13 = (GTextField)GetChildAt(1);
        }
    }
}