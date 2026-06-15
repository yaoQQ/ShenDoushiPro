/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace Training
{
    [UIBinder(PackageName = "Training")]
    public class TrainingBinder
    {
        public const string PACKAGE_NAME = "Training";

        public static void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(G_TrainingEnterView.URL, typeof(G_TrainingEnterView));
            UIObjectFactory.SetPackageItemExtension(G_YaDianNaDungeon.URL, typeof(G_YaDianNaDungeon));
        }
    }
}