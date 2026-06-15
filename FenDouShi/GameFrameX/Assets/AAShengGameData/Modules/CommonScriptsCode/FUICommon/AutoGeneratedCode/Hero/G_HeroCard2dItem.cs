/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hero
{
    public partial class G_HeroCard2dItem : GComponent
    {
        public GImage bigBg;
        public GImage qualityBg;
        public GLoader heroImage;
        public GImage levelBg;
        public GTextField level;
        public GTextField name;
        public GImage qualityImage;
        public G_CareerGroup careerGroup;
        public G_StarGroup starGroup;
        public const string URL = "ui://0g1kba0btae91j";
        public const string PACKAGE_NAME = "Hero";
        public const string COMPONENT_NAME = "HeroCard2dItem";

        public static G_HeroCard2dItem CreateInstance()
        {
            return (G_HeroCard2dItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bigBg = (GImage)GetChildAt(0);
            qualityBg = (GImage)GetChildAt(1);
            heroImage = (GLoader)GetChildAt(2);
            levelBg = (GImage)GetChildAt(3);
            level = (GTextField)GetChildAt(4);
            name = (GTextField)GetChildAt(5);
            qualityImage = (GImage)GetChildAt(6);
            careerGroup = (G_CareerGroup)GetChildAt(7);
            starGroup = (G_StarGroup)GetChildAt(8);
        }
    }
}