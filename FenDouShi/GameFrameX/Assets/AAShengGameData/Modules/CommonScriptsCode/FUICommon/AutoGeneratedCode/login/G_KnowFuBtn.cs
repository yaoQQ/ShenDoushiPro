/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_KnowFuBtn : GButton
    {
        public Controller button;
        public GImage n12;
        public GImage n13;
        public const string URL = "ui://l64dumk9b2eri87";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "KnowFuBtn";

        public static G_KnowFuBtn CreateInstance()
        {
            return (G_KnowFuBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n12 = (GImage)GetChildAt(0);
            n13 = (GImage)GetChildAt(1);
        }
    }
}