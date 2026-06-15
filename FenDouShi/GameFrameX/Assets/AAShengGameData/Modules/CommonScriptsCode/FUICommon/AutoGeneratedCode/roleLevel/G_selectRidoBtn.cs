/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_selectRidoBtn : GButton
    {
        public Controller button;
        public GImage n16;
        public GTextField title;
        public GImage n35;
        public const string URL = "ui://oz2qb5cgi1yt5";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "selectRidoBtn";

        public static G_selectRidoBtn CreateInstance()
        {
            return (G_selectRidoBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n16 = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
            n35 = (GImage)GetChildAt(2);
        }
    }
}