/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialChapterProgressItem : GComponent
    {
        public G_AthenaTrialChapterTowerItem towerItem;
        public GList List_star;
        public const string URL = "ui://ooop1fy6gxoq1y";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialChapterProgressItem";

        public static G_AthenaTrialChapterProgressItem CreateInstance()
        {
            return (G_AthenaTrialChapterProgressItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            towerItem = (G_AthenaTrialChapterTowerItem)GetChildAt(0);
            List_star = (GList)GetChildAt(1);
        }
    }
}