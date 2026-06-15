/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hero
{
    public partial class G_kaipai_btn_jia_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://0g1kba0btae91e";
        public const string PACKAGE_NAME = "Hero";
        public const string COMPONENT_NAME = "kaipai_btn_jia_1";

        public static G_kaipai_btn_jia_1 CreateInstance()
        {
            return (G_kaipai_btn_jia_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}