/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_goBtn : GButton
    {
        public GImage bg;
        public GTextField text;
        public GImage icon;
        public const string URL = "ui://y4b7yuunicym62";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "goBtn";

        public static G_goBtn CreateInstance()
        {
            return (G_goBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            text = (GTextField)GetChildAt(1);
            icon = (GImage)GetChildAt(2);
        }
    }
}