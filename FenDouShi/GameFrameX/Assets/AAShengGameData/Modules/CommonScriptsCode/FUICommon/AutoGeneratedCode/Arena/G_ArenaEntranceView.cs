/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaEntranceView : GComponent
    {
        public GImage n2;
        public G_ArenaEntranceTotalItem AreaItemHenLeft;
        public GImage n28;
        public GImage n45;
        public GImage n63;
        public GList ArenaItemList;
        public GLoader leftNPCLoad;
        public GLoader rightNPCLoad;
        public GImage n47;
        public GGroup viewGroup;
        public G_LeftNextBtn LeftNextBtn;
        public G_RightNextBtn RightNextBtn;
        public GTextField titleText;
        public const string URL = "ui://n8s4flryq19v13";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaEntranceView";

        public static G_ArenaEntranceView CreateInstance()
        {
            return (G_ArenaEntranceView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            AreaItemHenLeft = (G_ArenaEntranceTotalItem)GetChildAt(1);
            n28 = (GImage)GetChildAt(2);
            n45 = (GImage)GetChildAt(3);
            n63 = (GImage)GetChildAt(4);
            ArenaItemList = (GList)GetChildAt(5);
            leftNPCLoad = (GLoader)GetChildAt(6);
            rightNPCLoad = (GLoader)GetChildAt(7);
            n47 = (GImage)GetChildAt(8);
            viewGroup = (GGroup)GetChildAt(9);
            LeftNextBtn = (G_LeftNextBtn)GetChildAt(10);
            RightNextBtn = (G_RightNextBtn)GetChildAt(11);
            titleText = (GTextField)GetChildAt(12);
        }
    }
}