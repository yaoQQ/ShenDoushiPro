/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace TaskUI
{
    [UIBinder(PackageName = "TaskUI")]
    public class TaskUIBinder
    {
        public const string PACKAGE_NAME = "TaskUI";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_TreasureBox.URL, typeof(G_TreasureBox));
            UIObjectFactory.SetPackageItemExtension(G_TaskItem.URL, typeof(G_TaskItem));
            UIObjectFactory.SetPackageItemExtension(G_TaskTips.URL, typeof(G_TaskTips));
            UIObjectFactory.SetPackageItemExtension(G_TreasureRewardView.URL, typeof(G_TreasureRewardView));
            UIObjectFactory.SetPackageItemExtension(G_TaskTipsView.URL, typeof(G_TaskTipsView));
            UIObjectFactory.SetPackageItemExtension(G_TaskView.URL, typeof(G_TaskView));
        }
    }
}