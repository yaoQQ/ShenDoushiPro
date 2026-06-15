/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_SetLocationBtn : GButton
    {
        public GImage n71;
        public GTextField locationText;
        public const string URL = "ui://oz2qb5cgppy63g";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "SetLocationBtn";

        public static G_SetLocationBtn CreateInstance()
        {
            return (G_SetLocationBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n71 = (GImage)GetChildAt(0);
            locationText = (GTextField)GetChildAt(1);
        }
    }
}