/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaHoritem4 : GComponent
    {
        public GImage n37;
        public GLoader BgLoad;
        public GImage n61;
        public GList goodList;
        public GImage n41;
        public GLoader titleBgLoad;
        public GImage n45;
        public GTextField infoText;
        public G_FightLog FightLog;
        public GImage n69;
        public GImage n64;
        public GImage n65;
        public GTextField lockInfo;
        public GGroup maskGroup;
        public const string URL = "ui://n8s4flryq19v2a";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaHoritem4";

        public static G_ArenaHoritem4 CreateInstance()
        {
            return (G_ArenaHoritem4)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n37 = (GImage)GetChildAt(0);
            BgLoad = (GLoader)GetChildAt(1);
            n61 = (GImage)GetChildAt(2);
            goodList = (GList)GetChildAt(3);
            n41 = (GImage)GetChildAt(4);
            titleBgLoad = (GLoader)GetChildAt(5);
            n45 = (GImage)GetChildAt(6);
            infoText = (GTextField)GetChildAt(7);
            FightLog = (G_FightLog)GetChildAt(8);
            n69 = (GImage)GetChildAt(9);
            n64 = (GImage)GetChildAt(10);
            n65 = (GImage)GetChildAt(11);
            lockInfo = (GTextField)GetChildAt(12);
            maskGroup = (GGroup)GetChildAt(13);
        }
    }
}