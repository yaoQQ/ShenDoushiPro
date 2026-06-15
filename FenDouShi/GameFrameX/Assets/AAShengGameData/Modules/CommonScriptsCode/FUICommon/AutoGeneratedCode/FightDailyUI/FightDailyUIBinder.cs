/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace FightDailyUI
{
    [UIBinder(PackageName = "FightDailyUI")]
    public class FightDailyUIBinder
    {
        public const string PACKAGE_NAME = "FightDailyUI";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_FightDailyModeItem.URL, typeof(G_FightDailyModeItem));
            UIObjectFactory.SetPackageItemExtension(G_GoBtn.URL, typeof(G_GoBtn));
            UIObjectFactory.SetPackageItemExtension(G_TrainingEnterView.URL, typeof(G_TrainingEnterView));
            UIObjectFactory.SetPackageItemExtension(G_FightDailyItem.URL, typeof(G_FightDailyItem));
            UIObjectFactory.SetPackageItemExtension(G_FightDailyView.URL, typeof(G_FightDailyView));
            UIObjectFactory.SetPackageItemExtension(G_FightDailyModeListView.URL, typeof(G_FightDailyModeListView));
        }
    }
}