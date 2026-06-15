/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CommonBtn : GButton
    {
        public Controller button;
        public GImage n3;
        public GImage n1;
        public GTextField title;
        public const string URL = "ui://y4b7yuunpyg64s";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CommonBtn";

        public static G_CommonBtn CreateInstance()
        {
            return (G_CommonBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n3 = (GImage)GetChildAt(0);
            n1 = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}