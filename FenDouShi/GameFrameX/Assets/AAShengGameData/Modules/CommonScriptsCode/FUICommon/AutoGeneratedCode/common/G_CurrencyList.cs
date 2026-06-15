/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CurrencyList : GComponent
    {
        public G_CurrencyItemRender currenItem4;
        public G_CurrencyItemRender currenItem3;
        public G_CurrencyItemRender currenItem2;
        public G_CurrencyItemRender currenItem1;
        public GGroup n60;
        public const string URL = "ui://y4b7yuunkiy42iar";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CurrencyList";

        public static G_CurrencyList CreateInstance()
        {
            return (G_CurrencyList)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            currenItem4 = (G_CurrencyItemRender)GetChildAt(0);
            currenItem3 = (G_CurrencyItemRender)GetChildAt(1);
            currenItem2 = (G_CurrencyItemRender)GetChildAt(2);
            currenItem1 = (G_CurrencyItemRender)GetChildAt(3);
            n60 = (GGroup)GetChildAt(4);
        }
    }
}