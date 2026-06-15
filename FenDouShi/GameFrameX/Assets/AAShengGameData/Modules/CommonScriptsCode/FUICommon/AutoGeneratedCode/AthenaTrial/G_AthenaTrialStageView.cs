/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialStageView : GComponent
    {
        public GImage n4;
        public GImage n5;
        public GImage n7;
        public GImage n8;
        public GImage n9;
        public GButton closeButton;
        public GTextField titleText;
        public GList List_reward;
        public GGroup frame;
        public const string URL = "ui://ooop1fy6q28wp";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialStageView";

        public static G_AthenaTrialStageView CreateInstance()
        {
            return (G_AthenaTrialStageView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GImage)GetChildAt(0);
            n5 = (GImage)GetChildAt(1);
            n7 = (GImage)GetChildAt(2);
            n8 = (GImage)GetChildAt(3);
            n9 = (GImage)GetChildAt(4);
            closeButton = (GButton)GetChildAt(5);
            titleText = (GTextField)GetChildAt(6);
            List_reward = (GList)GetChildAt(7);
            frame = (GGroup)GetChildAt(8);
        }
    }
}