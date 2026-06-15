/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Main_BuffItem : GComponent
    {
        public GImage n84;
        public GImage n85;
        public GTextField n86;
        public GTextField n87;
        public const string URL = "ui://pux1kqsbpb4z2m";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Main_BuffItem";

        public static G_OnHookView_Main_BuffItem CreateInstance()
        {
            return (G_OnHookView_Main_BuffItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n84 = (GImage)GetChildAt(0);
            n85 = (GImage)GetChildAt(1);
            n86 = (GTextField)GetChildAt(2);
            n87 = (GTextField)GetChildAt(3);
        }
    }
}