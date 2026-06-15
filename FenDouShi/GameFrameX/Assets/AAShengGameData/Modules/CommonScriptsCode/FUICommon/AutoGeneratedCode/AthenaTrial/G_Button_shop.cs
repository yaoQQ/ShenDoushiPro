/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_Button_shop : GButton
    {
        public GImage n8;
        public GImage n9;
        public GTextField n10;
        public const string URL = "ui://ooop1fy6gxoq1w";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "Button_shop";

        public static G_Button_shop CreateInstance()
        {
            return (G_Button_shop)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n8 = (GImage)GetChildAt(0);
            n9 = (GImage)GetChildAt(1);
            n10 = (GTextField)GetChildAt(2);
        }
    }
}