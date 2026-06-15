/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelPlayerInfoView : GComponent
    {
        public Controller c1;
        public GGraph model;
        public GImage n127;
        public GTextField n5;
        public GButton n116;
        public GButton closeBtn;
        public GTextField n12;
        public GTextField roleId;
        public GImage n15;
        public GImage n131;
        public GImage n24;
        public GTextField n25;
        public GTextField n27;
        public GImage n129;
        public GImage n31;
        public GTextField n32;
        public GList heroTabList;
        public GImage n52;
        public GList heroIconList;
        public GImage n56;
        public GImage n57;
        public GTextField finghtPowerText;
        public GImage n60;
        public GImage n61;
        public GImage n63;
        public GTextField playerLevel;
        public G_SetRoleNameBtn SetRoleNameBtn;
        public GTextField guildName;
        public GTextField campText;
        public G_SetLocationBtn SetLocationBtn;
        public GImage n70;
        public GImage n72;
        public GImage n73;
        public G_LevelProgress LevelProgress;
        public G_fightPowBtn fightPowerBtn;
        public G_chatBtn chatBtn;
        public G_delectFriendBtn delectFriendBtn;
        public G_reportBtn reportBtn;
        public G_blackBtn blackBtn;
        public GGroup 左侧功能按钮状态2;
        public G_petProgressContent petTab;
        public G_roleInfoBtnContent roleInfoBtnContent;
        public GGroup n132;
        public const string URL = "ui://oz2qb5cgzbyu2o";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelPlayerInfoView";

        public static G_RoleLevelPlayerInfoView CreateInstance()
        {
            return (G_RoleLevelPlayerInfoView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            model = (GGraph)GetChildAt(0);
            n127 = (GImage)GetChildAt(1);
            n5 = (GTextField)GetChildAt(2);
            n116 = (GButton)GetChildAt(3);
            closeBtn = (GButton)GetChildAt(4);
            n12 = (GTextField)GetChildAt(5);
            roleId = (GTextField)GetChildAt(6);
            n15 = (GImage)GetChildAt(7);
            n131 = (GImage)GetChildAt(8);
            n24 = (GImage)GetChildAt(9);
            n25 = (GTextField)GetChildAt(10);
            n27 = (GTextField)GetChildAt(11);
            n129 = (GImage)GetChildAt(12);
            n31 = (GImage)GetChildAt(13);
            n32 = (GTextField)GetChildAt(14);
            heroTabList = (GList)GetChildAt(15);
            n52 = (GImage)GetChildAt(16);
            heroIconList = (GList)GetChildAt(17);
            n56 = (GImage)GetChildAt(18);
            n57 = (GImage)GetChildAt(19);
            finghtPowerText = (GTextField)GetChildAt(20);
            n60 = (GImage)GetChildAt(21);
            n61 = (GImage)GetChildAt(22);
            n63 = (GImage)GetChildAt(23);
            playerLevel = (GTextField)GetChildAt(24);
            SetRoleNameBtn = (G_SetRoleNameBtn)GetChildAt(25);
            guildName = (GTextField)GetChildAt(26);
            campText = (GTextField)GetChildAt(27);
            SetLocationBtn = (G_SetLocationBtn)GetChildAt(28);
            n70 = (GImage)GetChildAt(29);
            n72 = (GImage)GetChildAt(30);
            n73 = (GImage)GetChildAt(31);
            LevelProgress = (G_LevelProgress)GetChildAt(32);
            fightPowerBtn = (G_fightPowBtn)GetChildAt(33);
            chatBtn = (G_chatBtn)GetChildAt(34);
            delectFriendBtn = (G_delectFriendBtn)GetChildAt(35);
            reportBtn = (G_reportBtn)GetChildAt(36);
            blackBtn = (G_blackBtn)GetChildAt(37);
            左侧功能按钮状态2 = (GGroup)GetChildAt(38);
            petTab = (G_petProgressContent)GetChildAt(39);
            roleInfoBtnContent = (G_roleInfoBtnContent)GetChildAt(40);
            n132 = (GGroup)GetChildAt(41);
        }
    }
}