/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace launch
{
    public partial class G_LaunchPage : GComponent
    {
        public GImage Bg;
        public GImage n2;
        public GTextField updateTxt;
        public GTextField text_notice;
        public G_LaunchProgress updateProgress;
        public G_LauchNoticeView LauchNoticeView;
        public const string URL = "ui://ynb47g4jb2eri5c";
        public const string PACKAGE_NAME = "launch";
        public const string COMPONENT_NAME = "LaunchPage";

        public static G_LaunchPage CreateInstance()
        {
            return (G_LaunchPage)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Bg = (GImage)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            updateTxt = (GTextField)GetChildAt(2);
            text_notice = (GTextField)GetChildAt(3);
            updateProgress = (G_LaunchProgress)GetChildAt(4);
            LauchNoticeView = (G_LauchNoticeView)GetChildAt(5);
        }
    }
}