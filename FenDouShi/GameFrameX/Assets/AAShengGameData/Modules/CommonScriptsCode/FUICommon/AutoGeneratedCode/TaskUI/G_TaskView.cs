/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TaskView : GComponent
    {
        public GImage bg;
        public GImage n6;
        public GImage n7;
        public GImage SliderBg;
        public GImage SliderImg;
        public GImage n13;
        public GTextField CurrentPoint;
        public GList TreasureList;
        public GGroup ProgressPart;
        public GList TaskList;
        public GTextField n28;
        public GTextField RemainTime;
        public GGroup RefreshTime;
        public GGroup Frame;
        public GGraph CloseTreasureMask;
        public GImage n186;
        public GImage n187;
        public GList TreasureItemList;
        public GGroup TreasureView;
        public const string URL = "ui://a26huicllf0527";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TaskView";

        public static G_TaskView CreateInstance()
        {
            return (G_TaskView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            n6 = (GImage)GetChildAt(1);
            n7 = (GImage)GetChildAt(2);
            SliderBg = (GImage)GetChildAt(3);
            SliderImg = (GImage)GetChildAt(4);
            n13 = (GImage)GetChildAt(5);
            CurrentPoint = (GTextField)GetChildAt(6);
            TreasureList = (GList)GetChildAt(7);
            ProgressPart = (GGroup)GetChildAt(8);
            TaskList = (GList)GetChildAt(9);
            n28 = (GTextField)GetChildAt(10);
            RemainTime = (GTextField)GetChildAt(11);
            RefreshTime = (GGroup)GetChildAt(12);
            Frame = (GGroup)GetChildAt(13);
            CloseTreasureMask = (GGraph)GetChildAt(14);
            n186 = (GImage)GetChildAt(15);
            n187 = (GImage)GetChildAt(16);
            TreasureItemList = (GList)GetChildAt(17);
            TreasureView = (GGroup)GetChildAt(18);
        }
    }
}