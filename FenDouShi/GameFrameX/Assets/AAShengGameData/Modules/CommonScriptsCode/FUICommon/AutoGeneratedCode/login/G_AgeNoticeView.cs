/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_AgeNoticeView : GComponent
    {
        public GImage n2;
        public GImage n3;
        public GTextField Title;
        public G_ageCloseBtn closeBtn;
        public G_contentTextScroll contentText;
        public GGroup n9;
        public const string URL = "ui://l64dumk9mm2ii8g";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "AgeNoticeView";

        public static G_AgeNoticeView CreateInstance()
        {
            return (G_AgeNoticeView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            n3 = (GImage)GetChildAt(1);
            Title = (GTextField)GetChildAt(2);
            closeBtn = (G_ageCloseBtn)GetChildAt(3);
            contentText = (G_contentTextScroll)GetChildAt(4);
            n9 = (GGroup)GetChildAt(5);
        }
    }
}