/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendListRender : GComponent
    {
        public Controller option;
        public GImage n156;
        public GTextField playName;
        public GImage playTitile;
        public GGroup n178;
        public GTextField onlineTime;
        public GTextField unionName;
        public GImage n166;
        public GImage n168;
        public GImage n169;
        public GTextField powerLabel;
        public GImage n157;
        public GImage n158;
        public GImage n160;
        public GTextField levelLabel;
        public G_FriendOptionBtn chatBtn;
        public G_FriendOptionBtn sendPointBtn;
        public G_FriendOptionBtn getPointBtn;
        public G_FriendOptionBtn fightBtn;
        public GGroup friendOpt;
        public G_SelectDeleteComp selectDelte;
        public G_FriendOptionBtn deleteBtn;
        public G_FriendGreenBtn agreeBtn;
        public G_FriendRedBtn refuseBtn;
        public GGroup applyGroup;
        public G_FriendOptionBtn addBtn;
        public G_FriendGreenBtn readyAddBtn;
        public const string URL = "ui://jyya7ryuvliv4o";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendListRender";

        public static G_FriendListRender CreateInstance()
        {
            return (G_FriendListRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            option = GetControllerAt(0);
            n156 = (GImage)GetChildAt(0);
            playName = (GTextField)GetChildAt(1);
            playTitile = (GImage)GetChildAt(2);
            n178 = (GGroup)GetChildAt(3);
            onlineTime = (GTextField)GetChildAt(4);
            unionName = (GTextField)GetChildAt(5);
            n166 = (GImage)GetChildAt(6);
            n168 = (GImage)GetChildAt(7);
            n169 = (GImage)GetChildAt(8);
            powerLabel = (GTextField)GetChildAt(9);
            n157 = (GImage)GetChildAt(10);
            n158 = (GImage)GetChildAt(11);
            n160 = (GImage)GetChildAt(12);
            levelLabel = (GTextField)GetChildAt(13);
            chatBtn = (G_FriendOptionBtn)GetChildAt(14);
            sendPointBtn = (G_FriendOptionBtn)GetChildAt(15);
            getPointBtn = (G_FriendOptionBtn)GetChildAt(16);
            fightBtn = (G_FriendOptionBtn)GetChildAt(17);
            friendOpt = (GGroup)GetChildAt(18);
            selectDelte = (G_SelectDeleteComp)GetChildAt(19);
            deleteBtn = (G_FriendOptionBtn)GetChildAt(20);
            agreeBtn = (G_FriendGreenBtn)GetChildAt(21);
            refuseBtn = (G_FriendRedBtn)GetChildAt(22);
            applyGroup = (GGroup)GetChildAt(23);
            addBtn = (G_FriendOptionBtn)GetChildAt(24);
            readyAddBtn = (G_FriendGreenBtn)GetChildAt(25);
        }
    }
}