/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FightDailyUI
{
    public partial class G_FightDailyModeListView : GComponent
    {
        public GGraph ListBg;
        public GList DungeonModeItemList;
        public const string URL = "ui://k0wbd4rbus0l1q";
        public const string PACKAGE_NAME = "FightDailyUI";
        public const string COMPONENT_NAME = "FightDailyModeListView";

        public static G_FightDailyModeListView CreateInstance()
        {
            return (G_FightDailyModeListView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            ListBg = (GGraph)GetChildAt(0);
            DungeonModeItemList = (GList)GetChildAt(1);
        }
    }
}