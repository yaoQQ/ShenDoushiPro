/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_giftBuyBtn : GButton
    {
        public Controller button;
        public GImage n1;
        public GLoader icon;
        public GTextField title;
        public GGroup n4;
        public const string URL = "ui://yizyzd76mv0c35";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "giftBuyBtn";

        public static G_giftBuyBtn CreateInstance()
        {
            return (G_giftBuyBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            icon = (GLoader)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
            n4 = (GGroup)GetChildAt(3);
        }
    }
}