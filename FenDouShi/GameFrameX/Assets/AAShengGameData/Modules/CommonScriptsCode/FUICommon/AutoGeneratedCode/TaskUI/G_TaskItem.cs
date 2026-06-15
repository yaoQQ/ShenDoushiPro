/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TaskItem : GComponent
    {
        public GImage n31;
        public GImage n32;
        public GImage n37;
        public GTextField TaskName;
        public GTextField TaskDescription;
        public GImage TaskPointIcon;
        public GTextField TaskPointCount;
        public GButton Receive;
        public GButton GoTo;
        public GImage n76;
        public GTextField n77;
        public GGroup AlreadyReceive;
        public GList RewardList;
        public const string URL = "ui://a26huicljkkf1a";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TaskItem";

        public static G_TaskItem CreateInstance()
        {
            return (G_TaskItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n31 = (GImage)GetChildAt(0);
            n32 = (GImage)GetChildAt(1);
            n37 = (GImage)GetChildAt(2);
            TaskName = (GTextField)GetChildAt(3);
            TaskDescription = (GTextField)GetChildAt(4);
            TaskPointIcon = (GImage)GetChildAt(5);
            TaskPointCount = (GTextField)GetChildAt(6);
            Receive = (GButton)GetChildAt(7);
            GoTo = (GButton)GetChildAt(8);
            n76 = (GImage)GetChildAt(9);
            n77 = (GTextField)GetChildAt(10);
            AlreadyReceive = (GGroup)GetChildAt(11);
            RewardList = (GList)GetChildAt(12);
        }
    }
}