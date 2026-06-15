/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FightDailyUI
{
    public partial class G_GoBtn : GButton
    {
        public GImage n26;
        public GImage n27;
        public GTextField n28;
        public const string URL = "ui://k0wbd4rbd0iz1p";
        public const string PACKAGE_NAME = "FightDailyUI";
        public const string COMPONENT_NAME = "GoBtn";

        public static G_GoBtn CreateInstance()
        {
            return (G_GoBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
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