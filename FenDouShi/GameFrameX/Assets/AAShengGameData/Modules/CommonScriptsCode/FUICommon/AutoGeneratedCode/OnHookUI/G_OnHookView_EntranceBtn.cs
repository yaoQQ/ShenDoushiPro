/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_EntranceBtn : GButton
    {
        public GImage state_1;
        public GImage state_2;
        public GImage state_3;
        public GImage n5;
        public G_OnHookEntranceSlider Slider;
        public GTextField time;
        public GTextField full;
        public const string URL = "ui://pux1kqsbpb4z2u";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_EntranceBtn";

        public static G_OnHookView_EntranceBtn CreateInstance()
        {
            return (G_OnHookView_EntranceBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            state_1 = (GImage)GetChildAt(0);
            state_2 = (GImage)GetChildAt(1);
            state_3 = (GImage)GetChildAt(2);
            n5 = (GImage)GetChildAt(3);
            Slider = (G_OnHookEntranceSlider)GetChildAt(4);
            time = (GTextField)GetChildAt(5);
            full = (GTextField)GetChildAt(6);
        }
    }
}