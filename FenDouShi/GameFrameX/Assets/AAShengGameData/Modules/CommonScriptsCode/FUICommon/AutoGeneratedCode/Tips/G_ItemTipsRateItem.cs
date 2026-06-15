/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemTipsRateItem : GComponent
    {
        public GTextField Text_rate;
        public GTextField Text_name;
        public const string URL = "ui://g2ec8shvixvj5";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemTipsRateItem";

        public static G_ItemTipsRateItem CreateInstance()
        {
            return (G_ItemTipsRateItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Text_rate = (GTextField)GetChildAt(0);
            Text_name = (GTextField)GetChildAt(1);
        }
    }
}