/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_locationItem : GButton
    {
        public Controller button;
        public GImage n48;
        public GTextField localText;
        public GImage n45;
        public GImage n44;
        public GImage n47;
        public const string URL = "ui://oz2qb5cgi1ytb";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "locationItem";

        public static G_locationItem CreateInstance()
        {
            return (G_locationItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n48 = (GImage)GetChildAt(0);
            localText = (GTextField)GetChildAt(1);
            n45 = (GImage)GetChildAt(2);
            n44 = (GImage)GetChildAt(3);
            n47 = (GImage)GetChildAt(4);
        }
    }
}