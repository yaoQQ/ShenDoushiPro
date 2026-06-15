/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ClenBtn : GButton
    {
        public GImage n26;
        public GImage n27;
        public GTextField n28;
        public const string URL = "ui://l64dumk9b2eri84";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ClenBtn";

        public static G_ClenBtn CreateInstance()
        {
            return (G_ClenBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n26 = (GImage)GetChildAt(0);
            n27 = (GImage)GetChildAt(1);
            n28 = (GTextField)GetChildAt(2);
        }
    }
}