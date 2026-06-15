/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_TopContentCompoment : GComponent
    {
        public G_common_btn_guanbi_1 closeButton;
        public GTextField Text_title;
        public G_common_btn_xinxi01_1 Button_help;
        public GGroup back;
        public G_CurrencyList currencyList;
        public GGroup n59;
        public const string URL = "ui://y4b7yuunkiy42ias";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "TopContentCompoment";

        public static G_TopContentCompoment CreateInstance()
        {
            return (G_TopContentCompoment)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            closeButton = (G_common_btn_guanbi_1)GetChildAt(0);
            Text_title = (GTextField)GetChildAt(1);
            Button_help = (G_common_btn_xinxi01_1)GetChildAt(2);
            back = (GGroup)GetChildAt(3);
            currencyList = (G_CurrencyList)GetChildAt(4);
            n59 = (GGroup)GetChildAt(5);
        }
    }
}