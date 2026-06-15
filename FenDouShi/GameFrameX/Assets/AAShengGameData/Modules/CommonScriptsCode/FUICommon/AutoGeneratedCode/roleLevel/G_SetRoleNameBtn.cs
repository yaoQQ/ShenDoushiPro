/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_SetRoleNameBtn : GButton
    {
        public GImage n138;
        public GTextField userName;
        public const string URL = "ui://oz2qb5cgppy63f";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "SetRoleNameBtn";

        public static G_SetRoleNameBtn CreateInstance()
        {
            return (G_SetRoleNameBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n138 = (GImage)GetChildAt(0);
            userName = (GTextField)GetChildAt(1);
        }
    }
}