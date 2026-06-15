/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatView : GComponent
    {
        public GGraph CloseBtn_;
        public GGraph bgMask;
        public GImage bg;
        public G_CloseBtn CloseBtn;
        public GList ChatChannelList;
        public GList PlayerMessageList;
        public GImage n88;
        public GTextField n89;
        public GGroup Empty;
        public GTextField n124;
        public GButton GoTo;
        public GGroup EmptyGuild;
        public GImage n130;
        public GTree PlayerList;
        public GGroup PrivateChat;
        public GImage InputGroupBg;
        public GImage n80;
        public G_VoiceBtn VoiceBtn;
        public GImage n82;
        public GTextInput InputText;
        public G_EmojiBtn EmojiBtn;
        public GButton SendBtn;
        public GImage CoolDownMask;
        public GTextField CoolDownSecond;
        public GGroup SendBtnGroup;
        public GTextField LevelLimitTxt;
        public GGroup InputGroup;
        public const string URL = "ui://gw0bj7t0ozwm45";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatView";

        public static G_ChatView CreateInstance()
        {
            return (G_ChatView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            CloseBtn_ = (GGraph)GetChildAt(0);
            bgMask = (GGraph)GetChildAt(1);
            bg = (GImage)GetChildAt(2);
            CloseBtn = (G_CloseBtn)GetChildAt(3);
            ChatChannelList = (GList)GetChildAt(4);
            PlayerMessageList = (GList)GetChildAt(5);
            n88 = (GImage)GetChildAt(6);
            n89 = (GTextField)GetChildAt(7);
            Empty = (GGroup)GetChildAt(8);
            n124 = (GTextField)GetChildAt(9);
            GoTo = (GButton)GetChildAt(10);
            EmptyGuild = (GGroup)GetChildAt(11);
            n130 = (GImage)GetChildAt(12);
            PlayerList = (GTree)GetChildAt(13);
            PrivateChat = (GGroup)GetChildAt(14);
            InputGroupBg = (GImage)GetChildAt(15);
            n80 = (GImage)GetChildAt(16);
            VoiceBtn = (G_VoiceBtn)GetChildAt(17);
            n82 = (GImage)GetChildAt(18);
            InputText = (GTextInput)GetChildAt(19);
            EmojiBtn = (G_EmojiBtn)GetChildAt(20);
            SendBtn = (GButton)GetChildAt(21);
            CoolDownMask = (GImage)GetChildAt(22);
            CoolDownSecond = (GTextField)GetChildAt(23);
            SendBtnGroup = (GGroup)GetChildAt(24);
            LevelLimitTxt = (GTextField)GetChildAt(25);
            InputGroup = (GGroup)GetChildAt(26);
        }
    }
}