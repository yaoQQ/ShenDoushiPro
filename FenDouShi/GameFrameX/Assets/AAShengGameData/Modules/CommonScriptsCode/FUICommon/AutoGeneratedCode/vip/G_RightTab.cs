/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_RightTab : GButton
    {
        public Controller button;
        public GImage n115;
        public GImage select;
        public GTextField title;
        public const string URL = "ui://yizyzd76mv0c2y";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "RightTab";

        public static G_RightTab CreateInstance()
        {
            return (G_RightTab)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n115 = (GImage)GetChildAt(0);
            select = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}