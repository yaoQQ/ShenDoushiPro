/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaHorItem1 : GComponent
    {
        public GImage AreaitemBg;
        public GLoader BgLoad;
        public GImage n4;
        public GList goodList;
        public GImage n8;
        public GImage titleBgPic;
        public GLoader titleBgLoad;
        public GImage n11;
        public GTextField infoText;
        public G_FightLog FightLog;
        public GGroup n65;
        public GImage maskBg;
        public GImage n68;
        public GImage n69;
        public GTextField lockInfo;
        public GGroup maskGroup;
        public const string URL = "ui://n8s4flryq19v28";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaHorItem1";

        public static G_ArenaHorItem1 CreateInstance()
        {
            return (G_ArenaHorItem1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            AreaitemBg = (GImage)GetChildAt(0);
            BgLoad = (GLoader)GetChildAt(1);
            n4 = (GImage)GetChildAt(2);
            goodList = (GList)GetChildAt(3);
            n8 = (GImage)GetChildAt(4);
            titleBgPic = (GImage)GetChildAt(5);
            titleBgLoad = (GLoader)GetChildAt(6);
            n11 = (GImage)GetChildAt(7);
            infoText = (GTextField)GetChildAt(8);
            FightLog = (G_FightLog)GetChildAt(9);
            n65 = (GGroup)GetChildAt(10);
            maskBg = (GImage)GetChildAt(11);
            n68 = (GImage)GetChildAt(12);
            n69 = (GImage)GetChildAt(13);
            lockInfo = (GTextField)GetChildAt(14);
            maskGroup = (GGroup)GetChildAt(15);
        }
    }
}