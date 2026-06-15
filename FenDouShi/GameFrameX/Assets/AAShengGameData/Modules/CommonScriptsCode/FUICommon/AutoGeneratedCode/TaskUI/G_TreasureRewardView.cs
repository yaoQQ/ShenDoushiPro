/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TreasureRewardView : GComponent
    {
        public GImage n141;
        public GImage n126;
        public GImage n127;
        public GImage n128;
        public GImage n129;
        public GTextField n130;
        public GImage n132;
        public GImage n133;
        public GImage n134;
        public GTextField n135;
        public GImage n137;
        public GImage n138;
        public GImage n139;
        public GTextField n140;
        public const string URL = "ui://a26huicljkkf1c";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TreasureRewardView";

        public static G_TreasureRewardView CreateInstance()
        {
            return (G_TreasureRewardView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n141 = (GImage)GetChildAt(0);
            n126 = (GImage)GetChildAt(1);
            n127 = (GImage)GetChildAt(2);
            n128 = (GImage)GetChildAt(3);
            n129 = (GImage)GetChildAt(4);
            n130 = (GTextField)GetChildAt(5);
            n132 = (GImage)GetChildAt(6);
            n133 = (GImage)GetChildAt(7);
            n134 = (GImage)GetChildAt(8);
            n135 = (GTextField)GetChildAt(9);
            n137 = (GImage)GetChildAt(10);
            n138 = (GImage)GetChildAt(11);
            n139 = (GImage)GetChildAt(12);
            n140 = (GTextField)GetChildAt(13);
        }
    }
}