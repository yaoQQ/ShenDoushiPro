/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_MailView : GComponent
    {
        public Controller MailTab;
        public GImage n3;
        public GImage n4;
        public GTextField n6;
        public GButton CloseBtn;
        public G_MailTypeTab tab1;
        public G_MailTypeTab tab2;
        public GImage n174;
        public GImage n173;
        public GImage n180;
        public GList MailItemList;
        public GTextField n158;
        public GImage n159;
        public GImage n160;
        public GGroup NotMails;
        public GButton DeleteAllBtn;
        public GButton ReceiveAllBtn;
        public GTextField MailTitle;
        public GImage n166;
        public GImage n19;
        public GTextField n14;
        public GTextField MailSender;
        public GImage n167;
        public GImage n69;
        public GTextField MailDeleteTime;
        public GImage n10;
        public G_TextList ScrollText;
        public GGroup MailContentGroup;
        public GList RewardList;
        public GImage n9;
        public G_AlreadyReceive Receive;
        public GButton ReceiveBtn;
        public GGroup UnReceive;
        public GGroup Reward;
        public GImage n162;
        public GTextField n163;
        public GGroup NotSelectMail;
        public GGroup Frame;
        public const string URL = "ui://p4ru1eiciljg9q";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "MailView";

        public static G_MailView CreateInstance()
        {
            return (G_MailView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            MailTab = GetControllerAt(0);
            n3 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n6 = (GTextField)GetChildAt(2);
            CloseBtn = (GButton)GetChildAt(3);
            tab1 = (G_MailTypeTab)GetChildAt(4);
            tab2 = (G_MailTypeTab)GetChildAt(5);
            n174 = (GImage)GetChildAt(6);
            n173 = (GImage)GetChildAt(7);
            n180 = (GImage)GetChildAt(8);
            MailItemList = (GList)GetChildAt(9);
            n158 = (GTextField)GetChildAt(10);
            n159 = (GImage)GetChildAt(11);
            n160 = (GImage)GetChildAt(12);
            NotMails = (GGroup)GetChildAt(13);
            DeleteAllBtn = (GButton)GetChildAt(14);
            ReceiveAllBtn = (GButton)GetChildAt(15);
            MailTitle = (GTextField)GetChildAt(16);
            n166 = (GImage)GetChildAt(17);
            n19 = (GImage)GetChildAt(18);
            n14 = (GTextField)GetChildAt(19);
            MailSender = (GTextField)GetChildAt(20);
            n167 = (GImage)GetChildAt(21);
            n69 = (GImage)GetChildAt(22);
            MailDeleteTime = (GTextField)GetChildAt(23);
            n10 = (GImage)GetChildAt(24);
            ScrollText = (G_TextList)GetChildAt(25);
            MailContentGroup = (GGroup)GetChildAt(26);
            RewardList = (GList)GetChildAt(27);
            n9 = (GImage)GetChildAt(28);
            Receive = (G_AlreadyReceive)GetChildAt(29);
            ReceiveBtn = (GButton)GetChildAt(30);
            UnReceive = (GGroup)GetChildAt(31);
            Reward = (GGroup)GetChildAt(32);
            n162 = (GImage)GetChildAt(33);
            n163 = (GTextField)GetChildAt(34);
            NotSelectMail = (GGroup)GetChildAt(35);
            Frame = (GGroup)GetChildAt(36);
        }
    }
}