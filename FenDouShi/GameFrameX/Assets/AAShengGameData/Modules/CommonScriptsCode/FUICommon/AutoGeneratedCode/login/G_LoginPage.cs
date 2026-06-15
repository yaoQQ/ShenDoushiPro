/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_LoginPage : GComponent
    {
        public GImage Bg;
        public GTextField version;
        public GTextField Tips;
        public G_ageBtn ageBtn;
        public GImage n10;
        public GTextField n12;
        public G_LoginBtn createBtn;
        public GTextInput createAccount;
        public GGroup createPlayerContent;
        public G_GongGaoBtn noticeBtn;
        public G_ChangeUserBtn changeAccountBtn;
        public G_KnowFuBtn UserKnowBtn;
        public GTextField n22;
        public GImage n23;
        public GImage n24;
        public GImage n25;
        public G_userNoticeBtn userNotice;
        public G_userNoticeBtn privacyNoticeBtn;
        public G_selserverBtn selserverBtn;
        public G_LoginBtn loginBtn;
        public GGroup loginContent;
        public G_ClenBtn cleanCache;
        public const string URL = "ui://l64dumk9b2eri80";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "LoginPage";

        public static G_LoginPage CreateInstance()
        {
            return (G_LoginPage)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Bg = (GImage)GetChildAt(0);
            version = (GTextField)GetChildAt(1);
            Tips = (GTextField)GetChildAt(2);
            ageBtn = (G_ageBtn)GetChildAt(3);
            n10 = (GImage)GetChildAt(4);
            n12 = (GTextField)GetChildAt(5);
            createBtn = (G_LoginBtn)GetChildAt(6);
            createAccount = (GTextInput)GetChildAt(7);
            createPlayerContent = (GGroup)GetChildAt(8);
            noticeBtn = (G_GongGaoBtn)GetChildAt(9);
            changeAccountBtn = (G_ChangeUserBtn)GetChildAt(10);
            UserKnowBtn = (G_KnowFuBtn)GetChildAt(11);
            n22 = (GTextField)GetChildAt(12);
            n23 = (GImage)GetChildAt(13);
            n24 = (GImage)GetChildAt(14);
            n25 = (GImage)GetChildAt(15);
            userNotice = (G_userNoticeBtn)GetChildAt(16);
            privacyNoticeBtn = (G_userNoticeBtn)GetChildAt(17);
            selserverBtn = (G_selserverBtn)GetChildAt(18);
            loginBtn = (G_LoginBtn)GetChildAt(19);
            loginContent = (GGroup)GetChildAt(20);
            cleanCache = (G_ClenBtn)GetChildAt(21);
        }
    }
}