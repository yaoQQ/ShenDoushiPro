/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_rightHeadIcon : GComponent
    {
        public GImage n55;
        public GImage n56;
        public GImage n48;
        public GImage n49;
        public GTextField fightPowerTotalText;
        public GTextField roleName;
        public GImage n52;
        public GTextField levelText;
        public const string URL = "ui://oz2qb5cgegpfc";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "rightHeadIcon";

        public static G_rightHeadIcon CreateInstance()
        {
            return (G_rightHeadIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n55 = (GImage)GetChildAt(0);
            n56 = (GImage)GetChildAt(1);
            n48 = (GImage)GetChildAt(2);
            n49 = (GImage)GetChildAt(3);
            fightPowerTotalText = (GTextField)GetChildAt(4);
            roleName = (GTextField)GetChildAt(5);
            n52 = (GImage)GetChildAt(6);
            levelText = (GTextField)GetChildAt(7);
        }
    }
}