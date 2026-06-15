/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_checkBox : GButton
    {
        public Controller button;
        public GImage n17;
        public GImage select;
        public const string URL = "ui://y4b7yuunvliv2iew";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "checkBox";

        public static G_checkBox CreateInstance()
        {
            return (G_checkBox)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n17 = (GImage)GetChildAt(0);
            select = (GImage)GetChildAt(1);
        }
    }
}