/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CurrencyAddBtn : GButton
    {
        public GImage n33;
        public G_common_btn_jia_1 n34;
        public const string URL = "ui://y4b7yuunkiy42iap";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CurrencyAddBtn";

        public static G_CurrencyAddBtn CreateInstance()
        {
            return (G_CurrencyAddBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n33 = (GImage)GetChildAt(0);
            n34 = (G_common_btn_jia_1)GetChildAt(1);
        }
    }
}