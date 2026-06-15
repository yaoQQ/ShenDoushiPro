/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialStageItem : GComponent
    {
        public GImage n15;
        public GImage n16;
        public GTextField Text_desc;
        public GList List_reward;
        public GButton Button_get;
        public GButton Button_jump;
        public GImage Image_get;
        public GTextField n30;
        public GGroup group_get;
        public const string URL = "ui://ooop1fy6gxoqo";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialStageItem";

        public static G_AthenaTrialStageItem CreateInstance()
        {
            return (G_AthenaTrialStageItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n15 = (GImage)GetChildAt(0);
            n16 = (GImage)GetChildAt(1);
            Text_desc = (GTextField)GetChildAt(2);
            List_reward = (GList)GetChildAt(3);
            Button_get = (GButton)GetChildAt(4);
            Button_jump = (GButton)GetChildAt(5);
            Image_get = (GImage)GetChildAt(6);
            n30 = (GTextField)GetChildAt(7);
            group_get = (GGroup)GetChildAt(8);
        }
    }
}