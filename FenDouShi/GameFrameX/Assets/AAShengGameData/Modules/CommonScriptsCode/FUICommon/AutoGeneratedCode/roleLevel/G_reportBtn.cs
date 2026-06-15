/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_reportBtn : GButton
    {
        public GImage n108;
        public GImage n109;
        public GTextField n110;
        public const string URL = "ui://oz2qb5cgkmtsr";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "reportBtn";

        public static G_reportBtn CreateInstance()
        {
            return (G_reportBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n108 = (GImage)GetChildAt(0);
            n109 = (GImage)GetChildAt(1);
            n110 = (GTextField)GetChildAt(2);
        }
    }
}