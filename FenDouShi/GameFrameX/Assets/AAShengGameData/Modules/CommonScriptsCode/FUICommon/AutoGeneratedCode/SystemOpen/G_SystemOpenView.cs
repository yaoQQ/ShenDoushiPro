/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_SystemOpenView : GComponent
    {
        public GImage n2;
        public GImage n3;
        public GTextField n4;
        public GImage n6;
        public GTextField Text_systemName;
        public GTextField Text_systemDesc;
        public GImage n11;
        public GTextField n12;
        public GTextField n16;
        public GImage n106;
        public GImage n107;
        public GImage n48;
        public GLoader Image_system;
        public GList List_tab;
        public G_common_btn_02_1 Button_draw;
        public G_common_btn_guanbi_da_1 closeButton;
        public GList List_task;
        public GList List_rewardItem;
        public GGraph Image_model;
        public GGroup Frame;
        public const string URL = "ui://i3fvjlvyk00ye";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "SystemOpenView";

        public static G_SystemOpenView CreateInstance()
        {
            return (G_SystemOpenView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            n3 = (GImage)GetChildAt(1);
            n4 = (GTextField)GetChildAt(2);
            n6 = (GImage)GetChildAt(3);
            Text_systemName = (GTextField)GetChildAt(4);
            Text_systemDesc = (GTextField)GetChildAt(5);
            n11 = (GImage)GetChildAt(6);
            n12 = (GTextField)GetChildAt(7);
            n16 = (GTextField)GetChildAt(8);
            n106 = (GImage)GetChildAt(9);
            n107 = (GImage)GetChildAt(10);
            n48 = (GImage)GetChildAt(11);
            Image_system = (GLoader)GetChildAt(12);
            List_tab = (GList)GetChildAt(13);
            Button_draw = (G_common_btn_02_1)GetChildAt(14);
            closeButton = (G_common_btn_guanbi_da_1)GetChildAt(15);
            List_task = (GList)GetChildAt(16);
            List_rewardItem = (GList)GetChildAt(17);
            Image_model = (GGraph)GetChildAt(18);
            Frame = (GGroup)GetChildAt(19);
        }
    }
}