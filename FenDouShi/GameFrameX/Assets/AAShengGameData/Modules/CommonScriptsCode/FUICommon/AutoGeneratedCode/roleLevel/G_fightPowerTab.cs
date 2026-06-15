/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_fightPowerTab : GButton
    {
        public Controller button;
        public GImage n76;
        public GImage n82;
        public GTextField title;
        public GImage n59;
        public const string URL = "ui://oz2qb5cgegpff";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "fightPowerTab";

        public static G_fightPowerTab CreateInstance()
        {
            return (G_fightPowerTab)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n76 = (GImage)GetChildAt(0);
            n82 = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
            n59 = (GImage)GetChildAt(3);
        }
    }
}