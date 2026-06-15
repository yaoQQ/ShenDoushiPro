/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_MailDeleteComfirm : GComponent
    {
        public GImage n3;
        public GImage n4;
        public GTextField Title;
        public GImage n8;
        public GImage n9;
        public GTextField Content;
        public GImage n6;
        public G_common_btn_guanbi_xiao_1 CloseBtn;
        public G_common_btn_03_2 CancelBtn;
        public GTextField n14;
        public G_common_btn_02_2 ComfirmBtn;
        public GTextField n12;
        public GGroup Frame;
        public const string URL = "ui://p4ru1eicswrc16";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "MailDeleteComfirm";

        public static G_MailDeleteComfirm CreateInstance()
        {
            return (G_MailDeleteComfirm)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            Title = (GTextField)GetChildAt(2);
            n8 = (GImage)GetChildAt(3);
            n9 = (GImage)GetChildAt(4);
            Content = (GTextField)GetChildAt(5);
            n6 = (GImage)GetChildAt(6);
            CloseBtn = (G_common_btn_guanbi_xiao_1)GetChildAt(7);
            CancelBtn = (G_common_btn_03_2)GetChildAt(8);
            n14 = (GTextField)GetChildAt(9);
            ComfirmBtn = (G_common_btn_02_2)GetChildAt(10);
            n12 = (GTextField)GetChildAt(11);
            Frame = (GGroup)GetChildAt(12);
        }
    }
}