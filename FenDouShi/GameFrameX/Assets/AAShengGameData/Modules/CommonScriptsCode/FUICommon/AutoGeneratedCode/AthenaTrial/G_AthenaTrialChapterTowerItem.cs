/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialChapterTowerItem : GComponent
    {
        public GImage n47;
        public GLoader Image_icon;
        public GImage n49;
        public GImage n50;
        public GTextField Text_num;
        public const string URL = "ui://ooop1fy6gxoq23";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialChapterTowerItem";

        public static G_AthenaTrialChapterTowerItem CreateInstance()
        {
            return (G_AthenaTrialChapterTowerItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n47 = (GImage)GetChildAt(0);
            Image_icon = (GLoader)GetChildAt(1);
            n49 = (GImage)GetChildAt(2);
            n50 = (GImage)GetChildAt(3);
            Text_num = (GTextField)GetChildAt(4);
        }
    }
}