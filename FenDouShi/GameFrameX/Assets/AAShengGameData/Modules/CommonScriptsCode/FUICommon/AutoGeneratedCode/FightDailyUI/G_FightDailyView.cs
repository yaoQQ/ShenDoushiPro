/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FightDailyUI
{
    public partial class G_FightDailyView : GComponent
    {
        public GImage n69;
        public GList DungeonItemList;
        public GButton tipsBtn;
        public GButton GetAllBtn;
        public GTextField title;
        public G_FightDailyModeListView FightDailyModeListView;
        public GButton closeBtn;
        public GGroup 副本界面_;
        public const string URL = "ui://k0wbd4rbttmuo";
        public const string PACKAGE_NAME = "FightDailyUI";
        public const string COMPONENT_NAME = "FightDailyView";

        public static G_FightDailyView CreateInstance()
        {
            return (G_FightDailyView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n69 = (GImage)GetChildAt(0);
            DungeonItemList = (GList)GetChildAt(1);
            tipsBtn = (GButton)GetChildAt(2);
            GetAllBtn = (GButton)GetChildAt(3);
            title = (GTextField)GetChildAt(4);
            FightDailyModeListView = (G_FightDailyModeListView)GetChildAt(5);
            closeBtn = (GButton)GetChildAt(6);
            副本界面_ = (GGroup)GetChildAt(7);
        }
    }
}