/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Experience
{
    public partial class G_ExperienceView : GComponent
    {
        public GImage n66;
        public GTextField title;
        public GImage n53;
        public G_YaDianNaDungeon yaDianNaInstace;
        public GList experienceItemList;
        public GButton tipsBtn;
        public GButton closeBtn;
        public GGroup n65;
        public const string URL = "ui://neqo767femizn";
        public const string PACKAGE_NAME = "Experience";
        public const string COMPONENT_NAME = "ExperienceView";

        public static G_ExperienceView CreateInstance()
        {
            return (G_ExperienceView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n66 = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
            n53 = (GImage)GetChildAt(2);
            yaDianNaInstace = (G_YaDianNaDungeon)GetChildAt(3);
            experienceItemList = (GList)GetChildAt(4);
            tipsBtn = (GButton)GetChildAt(5);
            closeBtn = (GButton)GetChildAt(6);
            n65 = (GGroup)GetChildAt(7);
        }
    }
}