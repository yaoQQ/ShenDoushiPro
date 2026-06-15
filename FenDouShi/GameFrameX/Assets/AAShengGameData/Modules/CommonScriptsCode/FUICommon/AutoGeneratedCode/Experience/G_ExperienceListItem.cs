/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Experience
{
    public partial class G_ExperienceListItem : GButton
    {
        public G_InstanceBg InstanceBg;
        public GImage n6;
        public GImage rewardBg;
        public G_TitleBgCrl nameBgControl;
        public GLoader imgBg;
        public GLoader imgTitle;
        public G_InstanceLogo InstanceLogo;
        public GTextField levelTitle;
        public GTextField LevelNum;
        public GList itemList;
        public GTextField rewardTitle;
        public GTextField timeInfo;
        public const string URL = "ui://neqo767fq1gdo";
        public const string PACKAGE_NAME = "Experience";
        public const string COMPONENT_NAME = "ExperienceListItem";

        public static G_ExperienceListItem CreateInstance()
        {
            return (G_ExperienceListItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            InstanceBg = (G_InstanceBg)GetChildAt(0);
            n6 = (GImage)GetChildAt(1);
            rewardBg = (GImage)GetChildAt(2);
            nameBgControl = (G_TitleBgCrl)GetChildAt(3);
            imgBg = (GLoader)GetChildAt(4);
            imgTitle = (GLoader)GetChildAt(5);
            InstanceLogo = (G_InstanceLogo)GetChildAt(6);
            levelTitle = (GTextField)GetChildAt(7);
            LevelNum = (GTextField)GetChildAt(8);
            itemList = (GList)GetChildAt(9);
            rewardTitle = (GTextField)GetChildAt(10);
            timeInfo = (GTextField)GetChildAt(11);
        }
    }
}