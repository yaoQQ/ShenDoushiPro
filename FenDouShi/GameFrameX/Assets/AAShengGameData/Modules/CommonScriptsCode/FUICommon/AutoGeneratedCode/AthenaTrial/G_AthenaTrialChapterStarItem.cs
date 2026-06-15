/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialChapterStarItem : GComponent
    {
        public GLoader Image_satr;
        public const string URL = "ui://ooop1fy6gxoq20";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialChapterStarItem";

        public static G_AthenaTrialChapterStarItem CreateInstance()
        {
            return (G_AthenaTrialChapterStarItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_satr = (GLoader)GetChildAt(0);
        }
    }
}