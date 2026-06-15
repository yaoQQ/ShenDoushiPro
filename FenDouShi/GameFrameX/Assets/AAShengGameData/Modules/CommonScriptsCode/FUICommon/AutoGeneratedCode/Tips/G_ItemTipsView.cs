/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemTipsView : GComponent
    {
        public GButton viewMask;
        public GImage Image_content;
        public GRichTextField Text_desc;
        public GList List_getWay;
        public GGroup group_getWay;
        public GLoader Image_quality;
        public GTextField Text_num;
        public GRichTextField Text_name;
        public GTextField Text0;
        public GButton Button_getWay;
        public GLoader Image_icon;
        public GGroup group_content;
        public const string URL = "ui://g2ec8shvkc3p2ib4";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemTipsView";

        public static G_ItemTipsView CreateInstance()
        {
            return (G_ItemTipsView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewMask = (GButton)GetChildAt(0);
            Image_content = (GImage)GetChildAt(1);
            Text_desc = (GRichTextField)GetChildAt(2);
            List_getWay = (GList)GetChildAt(3);
            group_getWay = (GGroup)GetChildAt(4);
            Image_quality = (GLoader)GetChildAt(5);
            Text_num = (GTextField)GetChildAt(6);
            Text_name = (GRichTextField)GetChildAt(7);
            Text0 = (GTextField)GetChildAt(8);
            Button_getWay = (GButton)GetChildAt(9);
            Image_icon = (GLoader)GetChildAt(10);
            group_content = (GGroup)GetChildAt(11);
        }
    }
}