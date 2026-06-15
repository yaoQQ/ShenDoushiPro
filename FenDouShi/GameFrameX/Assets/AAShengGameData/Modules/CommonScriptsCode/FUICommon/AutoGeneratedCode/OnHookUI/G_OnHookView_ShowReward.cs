/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_ShowReward : GComponent
    {
        public GImage bg;
        public GImage n26;
        public GImage n18;
        public GImage n19;
        public GTextField n20;
        public GTextField Time;
        public GButton CloseBtn;
        public GButton Comfirm;
        public GImage n9;
        public GList RewardList;
        public GGroup Window;
        public const string URL = "ui://pux1kqsbpb4z11";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_ShowReward";

        public static G_OnHookView_ShowReward CreateInstance()
        {
            return (G_OnHookView_ShowReward)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            n26 = (GImage)GetChildAt(1);
            n18 = (GImage)GetChildAt(2);
            n19 = (GImage)GetChildAt(3);
            n20 = (GTextField)GetChildAt(4);
            Time = (GTextField)GetChildAt(5);
            CloseBtn = (GButton)GetChildAt(6);
            Comfirm = (GButton)GetChildAt(7);
            n9 = (GImage)GetChildAt(8);
            RewardList = (GList)GetChildAt(9);
            Window = (GGroup)GetChildAt(10);
        }
    }
}