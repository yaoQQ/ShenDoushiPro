/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Detail_Chapter : GComponent
    {
        public GImage bg;
        public GTextField title;
        public GTextField n23;
        public GImage n19;
        public GLoader ExtensionIcon;
        public GGroup RT;
        public GList itemList;
        public GList lineList;
        public GGroup Item;
        public GImage n49;
        public GImage bar;
        public GImage n52;
        public GImage n51;
        public GGroup Slider;
        public GGroup n55;
        public const string URL = "ui://pux1kqsbpb4z2p";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Detail_Chapter";

        public static G_OnHookView_Detail_Chapter CreateInstance()
        {
            return (G_OnHookView_Detail_Chapter)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
            n23 = (GTextField)GetChildAt(2);
            n19 = (GImage)GetChildAt(3);
            ExtensionIcon = (GLoader)GetChildAt(4);
            RT = (GGroup)GetChildAt(5);
            itemList = (GList)GetChildAt(6);
            lineList = (GList)GetChildAt(7);
            Item = (GGroup)GetChildAt(8);
            n49 = (GImage)GetChildAt(9);
            bar = (GImage)GetChildAt(10);
            n52 = (GImage)GetChildAt(11);
            n51 = (GImage)GetChildAt(12);
            Slider = (GGroup)GetChildAt(13);
            n55 = (GGroup)GetChildAt(14);
        }
    }
}