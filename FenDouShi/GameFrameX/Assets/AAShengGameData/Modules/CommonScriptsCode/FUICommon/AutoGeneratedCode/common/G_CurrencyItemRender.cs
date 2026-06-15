/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CurrencyItemRender : GComponent
    {
        public GImage n32;
        public GLoader itemIcon;
        public GTextField number;
        public G_CurrencyAddBtn addBtn;
        public GGroup n43;
        public GGroup n39;
        public const string URL = "ui://y4b7yuunzbyu2iaw";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CurrencyItemRender";

        public static G_CurrencyItemRender CreateInstance()
        {
            return (G_CurrencyItemRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n32 = (GImage)GetChildAt(0);
            itemIcon = (GLoader)GetChildAt(1);
            number = (GTextField)GetChildAt(2);
            addBtn = (G_CurrencyAddBtn)GetChildAt(3);
            n43 = (GGroup)GetChildAt(4);
            n39 = (GGroup)GetChildAt(5);
        }
    }
}