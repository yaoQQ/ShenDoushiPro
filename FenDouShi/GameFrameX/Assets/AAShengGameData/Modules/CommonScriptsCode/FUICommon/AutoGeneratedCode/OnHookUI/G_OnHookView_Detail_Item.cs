/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Detail_Item : GComponent
    {
        public GLoader icon;
        public GTextField itemName;
        public GTextField count;
        public const string URL = "ui://pux1kqsbpb4z2n";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Detail_Item";

        public static G_OnHookView_Detail_Item CreateInstance()
        {
            return (G_OnHookView_Detail_Item)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = (GLoader)GetChildAt(0);
            itemName = (GTextField)GetChildAt(1);
            count = (GTextField)GetChildAt(2);
        }
    }
}