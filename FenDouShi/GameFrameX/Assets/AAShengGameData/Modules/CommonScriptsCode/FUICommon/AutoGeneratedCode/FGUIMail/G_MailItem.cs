/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_MailItem : GButton
    {
        public Controller button;
        public GImage n57;
        public GImage n62;
        public GGroup BG_Read;
        public GImage n46;
        public GImage n55;
        public GGroup BG_Unread;
        public GImage New;
        public GImage LightIconBg;
        public GImage ML_ReadIcon;
        public GImage ML_UnReadIcon;
        public GGroup Light;
        public GImage n60;
        public GImage MD_ReadIcon;
        public GGroup Drak;
        public GTextField MailTitle_Unread;
        public GTextField MailTitle_Read;
        public GTextField SendTime_NotSelect_Unread;
        public GTextField SendTime_NotSelect_Read;
        public GImage n34;
        public GGroup RewardDark;
        public GImage n63;
        public GImage n64;
        public GImage RL_Receive;
        public GImage RL_UnReceive;
        public GGroup RewardLight;
        public GGroup HasRewardIcon;
        public GGroup NotSelect;
        public GImage n27;
        public GImage n33;
        public GImage Select_ReadIcon_BG;
        public GImage Select_ReadIcon_Read;
        public GTextField Select_MailTitle;
        public GTextField Select_SendTime;
        public GImage RewardBg;
        public GImage Receive1;
        public GImage UnReceive1;
        public GGroup Select_HasRewardIcon;
        public GGroup SelectStatus;
        public GGroup MailItem;
        public const string URL = "ui://p4ru1eiciljg9t";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "MailItem";

        public static G_MailItem CreateInstance()
        {
            return (G_MailItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n57 = (GImage)GetChildAt(0);
            n62 = (GImage)GetChildAt(1);
            BG_Read = (GGroup)GetChildAt(2);
            n46 = (GImage)GetChildAt(3);
            n55 = (GImage)GetChildAt(4);
            BG_Unread = (GGroup)GetChildAt(5);
            New = (GImage)GetChildAt(6);
            LightIconBg = (GImage)GetChildAt(7);
            ML_ReadIcon = (GImage)GetChildAt(8);
            ML_UnReadIcon = (GImage)GetChildAt(9);
            Light = (GGroup)GetChildAt(10);
            n60 = (GImage)GetChildAt(11);
            MD_ReadIcon = (GImage)GetChildAt(12);
            Drak = (GGroup)GetChildAt(13);
            MailTitle_Unread = (GTextField)GetChildAt(14);
            MailTitle_Read = (GTextField)GetChildAt(15);
            SendTime_NotSelect_Unread = (GTextField)GetChildAt(16);
            SendTime_NotSelect_Read = (GTextField)GetChildAt(17);
            n34 = (GImage)GetChildAt(18);
            RewardDark = (GGroup)GetChildAt(19);
            n63 = (GImage)GetChildAt(20);
            n64 = (GImage)GetChildAt(21);
            RL_Receive = (GImage)GetChildAt(22);
            RL_UnReceive = (GImage)GetChildAt(23);
            RewardLight = (GGroup)GetChildAt(24);
            HasRewardIcon = (GGroup)GetChildAt(25);
            NotSelect = (GGroup)GetChildAt(26);
            n27 = (GImage)GetChildAt(27);
            n33 = (GImage)GetChildAt(28);
            Select_ReadIcon_BG = (GImage)GetChildAt(29);
            Select_ReadIcon_Read = (GImage)GetChildAt(30);
            Select_MailTitle = (GTextField)GetChildAt(31);
            Select_SendTime = (GTextField)GetChildAt(32);
            RewardBg = (GImage)GetChildAt(33);
            Receive1 = (GImage)GetChildAt(34);
            UnReceive1 = (GImage)GetChildAt(35);
            Select_HasRewardIcon = (GGroup)GetChildAt(36);
            SelectStatus = (GGroup)GetChildAt(37);
            MailItem = (GGroup)GetChildAt(38);
        }
    }
}