/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_GongGaoBtn : GButton
    {
        public GImage n23;
        public GImage n24;
        public GTextField n25;
        public const string URL = "ui://l64dumk9b2eri85";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "GongGaoBtn";

        public static G_GongGaoBtn CreateInstance()
        {
            return (G_GongGaoBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n23 = (GImage)GetChildAt(0);
            n24 = (GImage)GetChildAt(1);
            n25 = (GTextField)GetChildAt(2);
        }
    }
}