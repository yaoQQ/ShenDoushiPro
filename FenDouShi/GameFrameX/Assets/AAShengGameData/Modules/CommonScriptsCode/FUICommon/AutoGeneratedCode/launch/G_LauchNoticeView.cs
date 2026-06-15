/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace launch
{
    public partial class G_LauchNoticeView : GComponent
    {
        public Controller c1;
        public GImage bg;
        public GImage n54;
        public GTextField noticeText;
        public GImage n5;
        public GTextField title;
        public GImage n7;
        public GImage n8;
        public G_okBtn okBtn;
        public G_closeBtn backBtn;
        public G_ageCloseBtn closeBtn;
        public const string URL = "ui://ynb47g4ju5vmi5k";
        public const string PACKAGE_NAME = "launch";
        public const string COMPONENT_NAME = "LauchNoticeView";

        public static G_LauchNoticeView CreateInstance()
        {
            return (G_LauchNoticeView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            bg = (GImage)GetChildAt(0);
            n54 = (GImage)GetChildAt(1);
            noticeText = (GTextField)GetChildAt(2);
            n5 = (GImage)GetChildAt(3);
            title = (GTextField)GetChildAt(4);
            n7 = (GImage)GetChildAt(5);
            n8 = (GImage)GetChildAt(6);
            okBtn = (G_okBtn)GetChildAt(7);
            backBtn = (G_closeBtn)GetChildAt(8);
            closeBtn = (G_ageCloseBtn)GetChildAt(9);
        }
    }
}