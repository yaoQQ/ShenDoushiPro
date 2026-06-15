/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FightDailyUI
{
    public partial class G_FightDailyItem : GComponent
    {
        public Controller c1;
        public GImage bg0;
        public GImage bg1;
        public GImage bg2;
        public GLoader bgLoad;
        public GImage n38;
        public GImage nameBg0;
        public GImage nameBg1;
        public GImage nameBg2;
        public GImage nameBg3;
        public GLoader nameBgLoad;
        public GImage n20;
        public GTextField levelTitle;
        public GTextField levelDetailText;
        public GTextField dailyTitle;
        public GTextField dailyText;
        public GList itemList;
        public GImage n33;
        public GTextField unLockText;
        public GImage lockBg;
        public GGroup isLock;
        public const string URL = "ui://k0wbd4rbteump";
        public const string PACKAGE_NAME = "FightDailyUI";
        public const string COMPONENT_NAME = "FightDailyItem";

        public static G_FightDailyItem CreateInstance()
        {
            return (G_FightDailyItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            bg0 = (GImage)GetChildAt(0);
            bg1 = (GImage)GetChildAt(1);
            bg2 = (GImage)GetChildAt(2);
            bgLoad = (GLoader)GetChildAt(3);
            n38 = (GImage)GetChildAt(4);
            nameBg0 = (GImage)GetChildAt(5);
            nameBg1 = (GImage)GetChildAt(6);
            nameBg2 = (GImage)GetChildAt(7);
            nameBg3 = (GImage)GetChildAt(8);
            nameBgLoad = (GLoader)GetChildAt(9);
            n20 = (GImage)GetChildAt(10);
            levelTitle = (GTextField)GetChildAt(11);
            levelDetailText = (GTextField)GetChildAt(12);
            dailyTitle = (GTextField)GetChildAt(13);
            dailyText = (GTextField)GetChildAt(14);
            itemList = (GList)GetChildAt(15);
            n33 = (GImage)GetChildAt(16);
            unLockText = (GTextField)GetChildAt(17);
            lockBg = (GImage)GetChildAt(18);
            isLock = (GGroup)GetChildAt(19);
        }
    }
}