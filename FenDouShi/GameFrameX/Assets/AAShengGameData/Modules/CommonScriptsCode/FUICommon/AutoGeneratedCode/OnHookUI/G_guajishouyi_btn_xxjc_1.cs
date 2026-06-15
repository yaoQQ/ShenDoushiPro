/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_guajishouyi_btn_xxjc_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://pux1kqsbpb4z1j";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "guajishouyi_btn_xxjc_1";

        public static G_guajishouyi_btn_xxjc_1 CreateInstance()
        {
            return (G_guajishouyi_btn_xxjc_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}