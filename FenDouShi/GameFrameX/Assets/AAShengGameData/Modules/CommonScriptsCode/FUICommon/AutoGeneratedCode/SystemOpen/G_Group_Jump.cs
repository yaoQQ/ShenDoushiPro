/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_Group_Jump : GButton
    {
        public G_common_btn_gongnengdi01_1 n33;
        public GTextField n34;
        public G_common_btn_jiantou02_1 n35;
        public GGroup group_jump;
        public const string URL = "ui://i3fvjlvyy01m2r";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "Group_Jump";

        public static G_Group_Jump CreateInstance()
        {
            return (G_Group_Jump)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n33 = (G_common_btn_gongnengdi01_1)GetChildAt(0);
            n34 = (GTextField)GetChildAt(1);
            n35 = (G_common_btn_jiantou02_1)GetChildAt(2);
            group_jump = (GGroup)GetChildAt(3);
        }
    }
}