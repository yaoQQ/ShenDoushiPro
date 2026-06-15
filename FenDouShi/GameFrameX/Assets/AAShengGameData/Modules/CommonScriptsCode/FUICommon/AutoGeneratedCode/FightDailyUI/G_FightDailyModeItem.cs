/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FightDailyUI
{
    public partial class G_FightDailyModeItem : GComponent
    {
        public Controller c1;
        public GImage bg;
        public GImage colorBg0;
        public GImage colorBg1;
        public GImage colorBg2;
        public GImage colorBg3;
        public GImage colorBg4;
        public GImage colorBg5;
        public GImage n10;
        public GImage modeBg0;
        public GImage modeBg1;
        public GImage modeBg2;
        public GImage modeBg3;
        public GImage modeBg4;
        public GImage modeBg5;
        public GTextField modeText;
        public GList itemList;
        public GTextField GetInfoText;
        public G_GoBtn GoBtn;
        public GButton SweepBtn;
        public GButton UnOpenBtn;
        public GButton FightBtn;
        public GGroup TypeBtnGroup;
        public GImage passBg;
        public GTextField passText;
        public GGroup passGroup;
        public GButton costItem;
        public GTextField costNum;
        public GGroup costGroup;
        public const string URL = "ui://k0wbd4rbd0iz10";
        public const string PACKAGE_NAME = "FightDailyUI";
        public const string COMPONENT_NAME = "FightDailyModeItem";

        public static G_FightDailyModeItem CreateInstance()
        {
            return (G_FightDailyModeItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            bg = (GImage)GetChildAt(0);
            colorBg0 = (GImage)GetChildAt(1);
            colorBg1 = (GImage)GetChildAt(2);
            colorBg2 = (GImage)GetChildAt(3);
            colorBg3 = (GImage)GetChildAt(4);
            colorBg4 = (GImage)GetChildAt(5);
            colorBg5 = (GImage)GetChildAt(6);
            n10 = (GImage)GetChildAt(7);
            modeBg0 = (GImage)GetChildAt(8);
            modeBg1 = (GImage)GetChildAt(9);
            modeBg2 = (GImage)GetChildAt(10);
            modeBg3 = (GImage)GetChildAt(11);
            modeBg4 = (GImage)GetChildAt(12);
            modeBg5 = (GImage)GetChildAt(13);
            modeText = (GTextField)GetChildAt(14);
            itemList = (GList)GetChildAt(15);
            GetInfoText = (GTextField)GetChildAt(16);
            GoBtn = (G_GoBtn)GetChildAt(17);
            SweepBtn = (GButton)GetChildAt(18);
            UnOpenBtn = (GButton)GetChildAt(19);
            FightBtn = (GButton)GetChildAt(20);
            TypeBtnGroup = (GGroup)GetChildAt(21);
            passBg = (GImage)GetChildAt(22);
            passText = (GTextField)GetChildAt(23);
            passGroup = (GGroup)GetChildAt(24);
            costItem = (GButton)GetChildAt(25);
            costNum = (GTextField)GetChildAt(26);
            costGroup = (GGroup)GetChildAt(27);
        }
    }
}