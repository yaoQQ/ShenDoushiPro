/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_MsgTipsContent : GComponent
    {
        public GImage n15;
        public GTextField noticeText;
        public const string URL = "ui://y4b7yuunmv0c2ifb";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "MsgTipsContent";

        public static G_MsgTipsContent CreateInstance()
        {
            return (G_MsgTipsContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n15 = (GImage)GetChildAt(0);
            noticeText = (GTextField)GetChildAt(1);
        }
    }
}