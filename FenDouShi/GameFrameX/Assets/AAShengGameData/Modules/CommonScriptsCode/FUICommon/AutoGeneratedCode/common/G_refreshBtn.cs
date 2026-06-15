/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_refreshBtn : GButton
    {
        public GImage n182;
        public GImage n27;
        public GTextField n28;
        public const string URL = "ui://y4b7yuunicym5x";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "refreshBtn";

        public static G_refreshBtn CreateInstance()
        {
            return (G_refreshBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n182 = (GImage)GetChildAt(0);
            n27 = (GImage)GetChildAt(1);
            n28 = (GTextField)GetChildAt(2);
        }
    }
}