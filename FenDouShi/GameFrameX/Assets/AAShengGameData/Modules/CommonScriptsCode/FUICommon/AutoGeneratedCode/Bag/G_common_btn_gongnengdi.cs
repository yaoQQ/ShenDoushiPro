/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_common_btn_gongnengdi : GButton
    {
        public Controller button;
        public GImage n1;
        public GLoader icon;
        public GTextField title;
        public const string URL = "ui://iy58luyasru53w";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "common_btn_gongnengdi";

        public static G_common_btn_gongnengdi CreateInstance()
        {
            return (G_common_btn_gongnengdi)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            icon = (GLoader)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}