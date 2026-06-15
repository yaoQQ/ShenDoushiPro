/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_SystemTaskItem : GButton
    {
        public GImage n18;
        public GTextField Text_name;
        public GImage n20;
        public GImage Image_slider;
        public GTextField Text_value;
        public GImage n24;
        public GTextField n25;
        public GGroup group_finish;
        public G_Group_Jump group_jump;
        public const string URL = "ui://i3fvjlvy105b2l";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "SystemTaskItem";

        public static G_SystemTaskItem CreateInstance()
        {
            return (G_SystemTaskItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n18 = (GImage)GetChildAt(0);
            Text_name = (GTextField)GetChildAt(1);
            n20 = (GImage)GetChildAt(2);
            Image_slider = (GImage)GetChildAt(3);
            Text_value = (GTextField)GetChildAt(4);
            n24 = (GImage)GetChildAt(5);
            n25 = (GTextField)GetChildAt(6);
            group_finish = (GGroup)GetChildAt(7);
            group_jump = (G_Group_Jump)GetChildAt(8);
        }
    }
}