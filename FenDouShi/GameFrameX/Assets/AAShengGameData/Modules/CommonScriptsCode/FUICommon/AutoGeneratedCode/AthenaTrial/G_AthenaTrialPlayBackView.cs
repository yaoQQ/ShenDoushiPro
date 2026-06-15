/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialPlayBackView : GComponent
    {
        public GImage n4;
        public GImage n5;
        public GTextField n6;
        public GImage n84;
        public GImage n85;
        public GImage n86;
        public GImage n87;
        public GImage n89;
        public GImage n91;
        public GButton closeButton;
        public GImage n22;
        public G_Button_vedio Button_vedio;
        public GImage n92;
        public GImage n77;
        public GTextField Text_fight;
        public GList List_note;
        public GGroup frame;
        public const string URL = "ui://ooop1fy6lj8c1k";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialPlayBackView";

        public static G_AthenaTrialPlayBackView CreateInstance()
        {
            return (G_AthenaTrialPlayBackView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GImage)GetChildAt(0);
            n5 = (GImage)GetChildAt(1);
            n6 = (GTextField)GetChildAt(2);
            n84 = (GImage)GetChildAt(3);
            n85 = (GImage)GetChildAt(4);
            n86 = (GImage)GetChildAt(5);
            n87 = (GImage)GetChildAt(6);
            n89 = (GImage)GetChildAt(7);
            n91 = (GImage)GetChildAt(8);
            closeButton = (GButton)GetChildAt(9);
            n22 = (GImage)GetChildAt(10);
            Button_vedio = (G_Button_vedio)GetChildAt(11);
            n92 = (GImage)GetChildAt(12);
            n77 = (GImage)GetChildAt(13);
            Text_fight = (GTextField)GetChildAt(14);
            List_note = (GList)GetChildAt(15);
            frame = (GGroup)GetChildAt(16);
        }
    }
}