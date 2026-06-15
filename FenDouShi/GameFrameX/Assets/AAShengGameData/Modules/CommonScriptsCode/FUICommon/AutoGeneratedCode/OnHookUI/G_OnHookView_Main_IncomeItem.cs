/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Main_IncomeItem : GComponent
    {
        public GImage n38;
        public GTextField Count;
        public GLoader Icon;
        public const string URL = "ui://pux1kqsbpb4z2j";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Main_IncomeItem";

        public static G_OnHookView_Main_IncomeItem CreateInstance()
        {
            return (G_OnHookView_Main_IncomeItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n38 = (GImage)GetChildAt(0);
            Count = (GTextField)GetChildAt(1);
            Icon = (GLoader)GetChildAt(2);
        }
    }
}