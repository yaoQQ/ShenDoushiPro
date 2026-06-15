/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_AthenaTrialPlayBackItem : GButton
    {
        public Controller button;
        public GImage Image_bg;
        public GImage Image_select;
        public GComponent headItem;
        public GTextField Text_name;
        public GImage n61;
        public GImage n62;
        public GTextField Text_fight;
        public GLoader Image_flag;
        public GTextField Text_flag;
        public GGroup group_flag;
        public const string URL = "ui://ooop1fy6gxoq1m";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "AthenaTrialPlayBackItem";

        public static G_AthenaTrialPlayBackItem CreateInstance()
        {
            return (G_AthenaTrialPlayBackItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_bg = (GImage)GetChildAt(0);
            Image_select = (GImage)GetChildAt(1);
            headItem = (GComponent)GetChildAt(2);
            Text_name = (GTextField)GetChildAt(3);
            n61 = (GImage)GetChildAt(4);
            n62 = (GImage)GetChildAt(5);
            Text_fight = (GTextField)GetChildAt(6);
            Image_flag = (GLoader)GetChildAt(7);
            Text_flag = (GTextField)GetChildAt(8);
            group_flag = (GGroup)GetChildAt(9);
        }
    }
}