/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_adviceBtn : GButton
    {
        public GImage n85;
        public GImage n86;
        public GTextField n87;
        public const string URL = "ui://oz2qb5cgppy63o";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "adviceBtn";

        public static G_adviceBtn CreateInstance()
        {
            return (G_adviceBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n85 = (GImage)GetChildAt(0);
            n86 = (GImage)GetChildAt(1);
            n87 = (GTextField)GetChildAt(2);
        }
    }
}