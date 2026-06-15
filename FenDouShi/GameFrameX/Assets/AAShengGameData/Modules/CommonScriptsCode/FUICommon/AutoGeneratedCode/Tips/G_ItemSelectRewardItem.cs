/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemSelectRewardItem : GComponent
    {
        public GImage n58;
        public GTextField Text_name;
        public GTextField Text_desc;
        public GButton goodItem;
        public G_ItemSelect Button_select;
        public const string URL = "ui://g2ec8shvsjcoh";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemSelectRewardItem";

        public static G_ItemSelectRewardItem CreateInstance()
        {
            return (G_ItemSelectRewardItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n58 = (GImage)GetChildAt(0);
            Text_name = (GTextField)GetChildAt(1);
            Text_desc = (GTextField)GetChildAt(2);
            goodItem = (GButton)GetChildAt(3);
            Button_select = (G_ItemSelect)GetChildAt(4);
        }
    }
}