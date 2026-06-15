/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagView : GComponent
    {
        public GButton viewMask;
        public GList List_tabs;
        public GImage n38;
        public GImage n39;
        public GImage n40;
        public GImage n41;
        public GTextField n43;
        public GGroup group_empty;
        public G_common_btn_gongnengdi Button_decompose;
        public G_common_btn_gongnengdi Button_select;
        public GGroup group_button;
        public GList list_bag;
        public G_BagTipsCompoment bagTipsCompoment;
        public GGroup n112;
        public G_BagSelectionView bagSelectView;
        public const string URL = "ui://iy58luyaqxuv1l";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagView";

        public static G_BagView CreateInstance()
        {
            return (G_BagView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewMask = (GButton)GetChildAt(0);
            List_tabs = (GList)GetChildAt(1);
            n38 = (GImage)GetChildAt(2);
            n39 = (GImage)GetChildAt(3);
            n40 = (GImage)GetChildAt(4);
            n41 = (GImage)GetChildAt(5);
            n43 = (GTextField)GetChildAt(6);
            group_empty = (GGroup)GetChildAt(7);
            Button_decompose = (G_common_btn_gongnengdi)GetChildAt(8);
            Button_select = (G_common_btn_gongnengdi)GetChildAt(9);
            group_button = (GGroup)GetChildAt(10);
            list_bag = (GList)GetChildAt(11);
            bagTipsCompoment = (G_BagTipsCompoment)GetChildAt(12);
            n112 = (GGroup)GetChildAt(13);
            bagSelectView = (G_BagSelectionView)GetChildAt(14);
        }
    }
}