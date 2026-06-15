/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_closeBtn : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://y4b7yuunvliv2ies";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "closeBtn";

        public static G_closeBtn CreateInstance()
        {
            return (G_closeBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}