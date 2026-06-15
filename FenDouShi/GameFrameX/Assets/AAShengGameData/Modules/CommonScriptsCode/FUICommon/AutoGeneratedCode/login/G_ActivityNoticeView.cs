/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ActivityNoticeView : GComponent
    {
        public GImage n3;
        public GImage n4;
        public GImage n48;
        public GImage noticePic;
        public GImage n23;
        public GTextField contentTitle;
        public G_contentText contentText;
        public GTextField empetText;
        public GList activityTabList;
        public G_circleCloseBtn closeBtn;
        public GGroup 公告弹窗;
        public const string URL = "ui://l64dumk9mm2ii95";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ActivityNoticeView";

        public static G_ActivityNoticeView CreateInstance()
        {
            return (G_ActivityNoticeView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n48 = (GImage)GetChildAt(2);
            noticePic = (GImage)GetChildAt(3);
            n23 = (GImage)GetChildAt(4);
            contentTitle = (GTextField)GetChildAt(5);
            contentText = (G_contentText)GetChildAt(6);
            empetText = (GTextField)GetChildAt(7);
            activityTabList = (GList)GetChildAt(8);
            closeBtn = (G_circleCloseBtn)GetChildAt(9);
            公告弹窗 = (GGroup)GetChildAt(10);
        }
    }
}