/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Detail : GComponent
    {
        public GImage n4;
        public GImage n5;
        public GImage n25;
        public GTextField n29;
        public GImage n45;
        public GImage n24;
        public GButton closeBtn;
        public GList ChapterItemList;
        public GGroup Window;
        public const string URL = "ui://pux1kqsbpb4z27";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Detail";

        public static G_OnHookView_Detail CreateInstance()
        {
            return (G_OnHookView_Detail)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GImage)GetChildAt(0);
            n5 = (GImage)GetChildAt(1);
            n25 = (GImage)GetChildAt(2);
            n29 = (GTextField)GetChildAt(3);
            n45 = (GImage)GetChildAt(4);
            n24 = (GImage)GetChildAt(5);
            closeBtn = (GButton)GetChildAt(6);
            ChapterItemList = (GList)GetChildAt(7);
            Window = (GGroup)GetChildAt(8);
        }
    }
}