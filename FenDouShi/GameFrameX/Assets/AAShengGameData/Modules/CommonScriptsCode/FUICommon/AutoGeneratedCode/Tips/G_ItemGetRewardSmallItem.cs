/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemGetRewardSmallItem : GComponent
    {
        public GImage n8;
        public GTextField Text_name;
        public GTextField Text_count;
        public GComponent goodItem;
        public const string URL = "ui://g2ec8shvixvj0";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemGetRewardSmallItem";

        public static G_ItemGetRewardSmallItem CreateInstance()
        {
            return (G_ItemGetRewardSmallItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n8 = (GImage)GetChildAt(0);
            Text_name = (GTextField)GetChildAt(1);
            Text_count = (GTextField)GetChildAt(2);
            goodItem = (GComponent)GetChildAt(3);
        }
    }
}