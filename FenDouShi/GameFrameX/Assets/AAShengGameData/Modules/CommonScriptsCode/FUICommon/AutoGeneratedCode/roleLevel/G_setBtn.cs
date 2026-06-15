/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_setBtn : GButton
    {
        public GImage n89;
        public GImage n90;
        public GTextField n91;
        public const string URL = "ui://oz2qb5cgppy63p";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "setBtn";

        public static G_setBtn CreateInstance()
        {
            return (G_setBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n89 = (GImage)GetChildAt(0);
            n90 = (GImage)GetChildAt(1);
            n91 = (GTextField)GetChildAt(2);
        }
    }
}