/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialSweepView : GComponent
    {
        public GImage n4;
        public GImage n5;
        public GImage n7;
        public GImage n8;
        public GImage n9;
        public GButton closeButton;
        public GTextField n13;
        public GImage n15;
        public GButton Button_sure;
        public GButton Button_cancel;
        public GTextField Text_desc;
        public GGroup frame;
        public GList List_reward;
        public const string URL = "ui://ooop1fy6pto8q";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialSweepView";

        public static G_AthenaTrialSweepView CreateInstance()
        {
            return (G_AthenaTrialSweepView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
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
            n13 = (GTextField)GetChildAt(6);
            n15 = (GImage)GetChildAt(7);
            Button_sure = (GButton)GetChildAt(8);
            Button_cancel = (GButton)GetChildAt(9);
            Text_desc = (GTextField)GetChildAt(10);
            frame = (GGroup)GetChildAt(11);
            List_reward = (GList)GetChildAt(12);
        }
    }
}