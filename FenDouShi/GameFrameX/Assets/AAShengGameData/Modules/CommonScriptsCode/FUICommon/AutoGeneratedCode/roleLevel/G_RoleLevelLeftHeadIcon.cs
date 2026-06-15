/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelLeftHeadIcon : GComponent
    {
        public GImage n35;
        public GImage n46;
        public GImage n37;
        public GImage n41;
        public GTextField fightPowerTotalText;
        public GTextField roleName;
        public GImage n44;
        public GTextField levelText;
        public const string URL = "ui://oz2qb5cgzbyu2p";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelLeftHeadIcon";

        public static G_RoleLevelLeftHeadIcon CreateInstance()
        {
            return (G_RoleLevelLeftHeadIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n35 = (GImage)GetChildAt(0);
            n46 = (GImage)GetChildAt(1);
            n37 = (GImage)GetChildAt(2);
            n41 = (GImage)GetChildAt(3);
            fightPowerTotalText = (GTextField)GetChildAt(4);
            roleName = (GTextField)GetChildAt(5);
            n44 = (GImage)GetChildAt(6);
            levelText = (GTextField)GetChildAt(7);
        }
    }
}