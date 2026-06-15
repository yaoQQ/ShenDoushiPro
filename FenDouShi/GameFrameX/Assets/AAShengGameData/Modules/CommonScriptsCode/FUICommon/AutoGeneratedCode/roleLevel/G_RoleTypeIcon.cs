/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleTypeIcon : GComponent
    {
        public Controller c1;
        public GImage n1;
        public GImage n2;
        public GImage n3;
        public GImage n4;
        public G_HeroIcon heroIcon;
        public const string URL = "ui://oz2qb5cgppy63n";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleTypeIcon";

        public static G_RoleTypeIcon CreateInstance()
        {
            return (G_RoleTypeIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            n3 = (GImage)GetChildAt(2);
            n4 = (GImage)GetChildAt(3);
            heroIcon = (G_HeroIcon)GetChildAt(4);
        }
    }
}