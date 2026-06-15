/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaHoritem3 : GComponent
    {
        public GImage n20;
        public GLoader BgLoad;
        public GImage n47;
        public GImage n23;
        public GImage n24;
        public GLoader titleBgLoad;
        public GImage n46;
        public GList goodList;
        public GTextField infoText;
        public G_FightLog FightLog;
        public GImage maskBg;
        public GImage n49;
        public GImage n50;
        public GTextField lockInfo;
        public GGroup maskGroup;
        public const string URL = "ui://n8s4flryq19v2b";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaHoritem3";

        public static G_ArenaHoritem3 CreateInstance()
        {
            return (G_ArenaHoritem3)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n20 = (GImage)GetChildAt(0);
            BgLoad = (GLoader)GetChildAt(1);
            n47 = (GImage)GetChildAt(2);
            n23 = (GImage)GetChildAt(3);
            n24 = (GImage)GetChildAt(4);
            titleBgLoad = (GLoader)GetChildAt(5);
            n46 = (GImage)GetChildAt(6);
            goodList = (GList)GetChildAt(7);
            infoText = (GTextField)GetChildAt(8);
            FightLog = (G_FightLog)GetChildAt(9);
            maskBg = (GImage)GetChildAt(10);
            n49 = (GImage)GetChildAt(11);
            n50 = (GImage)GetChildAt(12);
            lockInfo = (GTextField)GetChildAt(13);
            maskGroup = (GGroup)GetChildAt(14);
        }
    }
}