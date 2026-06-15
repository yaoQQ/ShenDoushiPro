/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialMainView : GComponent
    {
        public GImage n2;
        public GGroup frame;
        public GImage n4;
        public GImage n19;
        public GTextField Text_title1;
        public GLoader Image_type;
        public GButton Button_1;
        public GTextField Text_desc1;
        public GComponent redPoint1;
        public GGroup Item1;
        public GImage n11;
        public GTextField Text_title2;
        public GTextField Text_desc2;
        public GButton Button_2;
        public GComponent redPoint2;
        public GGroup Item2;
        public GImage n7;
        public GTextField Text_title3;
        public GTextField Text_desc3;
        public GButton Button_3;
        public GComponent redPoint3;
        public GGroup Item3;
        public GImage n15;
        public GTextField Text_title4;
        public GTextField Text_desc4;
        public GButton Button_4;
        public GComponent redPoint4;
        public GImage n24;
        public GTextField Text_title5;
        public GTextField Text_desc5;
        public GButton Button_5;
        public GComponent redPoint5;
        public const string URL = "ui://ooop1fy6mcdlk";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialMainView";

        public static G_AthenaTrialMainView CreateInstance()
        {
            return (G_AthenaTrialMainView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            frame = (GGroup)GetChildAt(1);
            n4 = (GImage)GetChildAt(2);
            n19 = (GImage)GetChildAt(3);
            Text_title1 = (GTextField)GetChildAt(4);
            Image_type = (GLoader)GetChildAt(5);
            Button_1 = (GButton)GetChildAt(6);
            Text_desc1 = (GTextField)GetChildAt(7);
            redPoint1 = (GComponent)GetChildAt(8);
            Item1 = (GGroup)GetChildAt(9);
            n11 = (GImage)GetChildAt(10);
            Text_title2 = (GTextField)GetChildAt(11);
            Text_desc2 = (GTextField)GetChildAt(12);
            Button_2 = (GButton)GetChildAt(13);
            redPoint2 = (GComponent)GetChildAt(14);
            Item2 = (GGroup)GetChildAt(15);
            n7 = (GImage)GetChildAt(16);
            Text_title3 = (GTextField)GetChildAt(17);
            Text_desc3 = (GTextField)GetChildAt(18);
            Button_3 = (GButton)GetChildAt(19);
            redPoint3 = (GComponent)GetChildAt(20);
            Item3 = (GGroup)GetChildAt(21);
            n15 = (GImage)GetChildAt(22);
            Text_title4 = (GTextField)GetChildAt(23);
            Text_desc4 = (GTextField)GetChildAt(24);
            Button_4 = (GButton)GetChildAt(25);
            redPoint4 = (GComponent)GetChildAt(26);
            n24 = (GImage)GetChildAt(27);
            Text_title5 = (GTextField)GetChildAt(28);
            Text_desc5 = (GTextField)GetChildAt(29);
            Button_5 = (GButton)GetChildAt(30);
            redPoint5 = (GComponent)GetChildAt(31);
        }
    }
}