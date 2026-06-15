/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_returnBtn : GButton
    {
        public Controller button;
        public GImage n0;
        public GImage n1;
        public GTextField n2;
        public const string URL = "ui://y4b7yuunragy8";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "returnBtn";

        public static G_returnBtn CreateInstance()
        {
            return (G_returnBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n0 = (GImage)GetChildAt(0);
            n1 = (GImage)GetChildAt(1);
            n2 = (GTextField)GetChildAt(2);
        }
    }
}