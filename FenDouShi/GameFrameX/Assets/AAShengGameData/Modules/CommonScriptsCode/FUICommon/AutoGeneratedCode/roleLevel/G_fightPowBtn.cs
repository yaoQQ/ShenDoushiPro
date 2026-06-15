/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_fightPowBtn : GButton
    {
        public GImage n96;
        public GImage n97;
        public GTextField n98;
        public const string URL = "ui://oz2qb5cgkmtsu";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "fightPowBtn";

        public static G_fightPowBtn CreateInstance()
        {
            return (G_fightPowBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n96 = (GImage)GetChildAt(0);
            n97 = (GImage)GetChildAt(1);
            n98 = (GTextField)GetChildAt(2);
        }
    }
}