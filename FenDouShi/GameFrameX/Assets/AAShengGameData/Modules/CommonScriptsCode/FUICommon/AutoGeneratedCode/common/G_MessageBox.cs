/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_MessageBox : GComponent
    {
        public GImage n3;
        public GImage n4;
        public GTextField Title;
        public GImage n8;
        public GImage n9;
        public GImage n6;
        public G_closeBtn CloseBtn;
        public G_common_btn_02_1 OkBtn;
        public G_common_btn_03_1 CancelBtn;
        public GRichTextField Content;
        public GRichTextField rightText;
        public GGroup Frame;
        public G_checkBox todayCheck;
        public GTextField tipsText;
        public GGroup todayTips;
        public const string URL = "ui://y4b7yuunvliv2ier";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "MessageBox";

        public static G_MessageBox CreateInstance()
        {
            return (G_MessageBox)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            Title = (GTextField)GetChildAt(2);
            n8 = (GImage)GetChildAt(3);
            n9 = (GImage)GetChildAt(4);
            n6 = (GImage)GetChildAt(5);
            CloseBtn = (G_closeBtn)GetChildAt(6);
            OkBtn = (G_common_btn_02_1)GetChildAt(7);
            CancelBtn = (G_common_btn_03_1)GetChildAt(8);
            Content = (GRichTextField)GetChildAt(9);
            rightText = (GRichTextField)GetChildAt(10);
            Frame = (GGroup)GetChildAt(11);
            todayCheck = (G_checkBox)GetChildAt(12);
            tipsText = (GTextField)GetChildAt(13);
            todayTips = (GGroup)GetChildAt(14);
        }
    }
}