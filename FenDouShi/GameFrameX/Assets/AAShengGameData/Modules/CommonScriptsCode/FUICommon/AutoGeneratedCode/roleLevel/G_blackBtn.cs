/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_blackBtn : GButton
    {
        public GImage n112;
        public GImage n116;
        public GTextField n114;
        public const string URL = "ui://oz2qb5cgkmtsq";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "blackBtn";

        public static G_blackBtn CreateInstance()
        {
            return (G_blackBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n112 = (GImage)GetChildAt(0);
            n116 = (GImage)GetChildAt(1);
            n114 = (GTextField)GetChildAt(2);
        }
    }
}