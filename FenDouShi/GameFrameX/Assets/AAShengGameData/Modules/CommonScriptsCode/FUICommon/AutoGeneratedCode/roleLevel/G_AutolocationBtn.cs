/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_AutolocationBtn : GButton
    {
        public GImage n17;
        public GTextField n18;
        public const string URL = "ui://oz2qb5cgqwrq39";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "AutolocationBtn";

        public static G_AutolocationBtn CreateInstance()
        {
            return (G_AutolocationBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n17 = (GImage)GetChildAt(0);
            n18 = (GTextField)GetChildAt(1);
        }
    }
}