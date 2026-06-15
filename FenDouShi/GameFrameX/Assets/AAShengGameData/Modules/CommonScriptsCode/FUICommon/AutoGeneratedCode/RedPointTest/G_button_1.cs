/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace RedPointTest
{
    public partial class G_button_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public GTextField text;
        public GComponent n4;
        public const string URL = "ui://tpkzz9jlgj9wd";
        public const string PACKAGE_NAME = "RedPointTest";
        public const string COMPONENT_NAME = "button_1";

        public static G_button_1 CreateInstance()
        {
            return (G_button_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            text = (GTextField)GetChildAt(1);
            n4 = (GComponent)GetChildAt(2);
        }
    }
}