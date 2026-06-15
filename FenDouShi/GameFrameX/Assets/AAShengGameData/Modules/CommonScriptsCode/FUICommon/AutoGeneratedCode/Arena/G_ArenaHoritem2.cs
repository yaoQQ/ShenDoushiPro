/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaHoritem2 : GComponent
    {
        public GImage n29;
        public GLoader BgLoad;
        public GImage n53;
        public GImage n31;
        public GList goodList;
        public GImage n32;
        public GImage titleBg;
        public GLoader titleBgLoad;
        public GImage n52;
        public GTextField infoText;
        public GImage maskBg;
        public G_FightLog FightLog;
        public GImage maskBg_2;
        public GImage n56;
        public GImage n57;
        public GTextField lockInfo;
        public GGroup maskGroup;
        public const string URL = "ui://n8s4flryq19v29";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaHoritem2";

        public static G_ArenaHoritem2 CreateInstance()
        {
            return (G_ArenaHoritem2)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n29 = (GImage)GetChildAt(0);
            BgLoad = (GLoader)GetChildAt(1);
            n53 = (GImage)GetChildAt(2);
            n31 = (GImage)GetChildAt(3);
            goodList = (GList)GetChildAt(4);
            n32 = (GImage)GetChildAt(5);
            titleBg = (GImage)GetChildAt(6);
            titleBgLoad = (GLoader)GetChildAt(7);
            n52 = (GImage)GetChildAt(8);
            infoText = (GTextField)GetChildAt(9);
            maskBg = (GImage)GetChildAt(10);
            FightLog = (G_FightLog)GetChildAt(11);
            maskBg_2 = (GImage)GetChildAt(12);
            n56 = (GImage)GetChildAt(13);
            n57 = (GImage)GetChildAt(14);
            lockInfo = (GTextField)GetChildAt(15);
            maskGroup = (GGroup)GetChildAt(16);
        }
    }
}