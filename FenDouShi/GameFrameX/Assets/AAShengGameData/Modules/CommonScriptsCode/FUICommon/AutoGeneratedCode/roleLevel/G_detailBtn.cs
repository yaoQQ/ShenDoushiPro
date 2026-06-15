/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_detailBtn : GButton
    {
        public GImage n13;
        public GTextField n14;
        public GImage n16;
        public const string URL = "ui://oz2qb5cgkmts2x";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "detailBtn";

        public static G_detailBtn CreateInstance()
        {
            return (G_detailBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n13 = (GImage)GetChildAt(0);
            n14 = (GTextField)GetChildAt(1);
            n16 = (GImage)GetChildAt(2);
        }
    }
}