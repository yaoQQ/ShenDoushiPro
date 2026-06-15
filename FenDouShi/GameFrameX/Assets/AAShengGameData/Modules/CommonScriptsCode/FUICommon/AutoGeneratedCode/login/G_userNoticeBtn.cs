/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_userNoticeBtn : GButton
    {
        public GGraph userNotice;
        public const string URL = "ui://l64dumk9mm2ii9d";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "userNoticeBtn";

        public static G_userNoticeBtn CreateInstance()
        {
            return (G_userNoticeBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            userNotice = (GGraph)GetChildAt(0);
        }
    }
}