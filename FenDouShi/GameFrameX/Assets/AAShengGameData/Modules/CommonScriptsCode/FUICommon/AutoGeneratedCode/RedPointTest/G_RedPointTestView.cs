/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace RedPointTest
{
    public partial class G_RedPointTestView : GComponent
    {
        public G_button_1 n101;
        public G_button_1 n102;
        public G_button_1 n103;
        public G_button_1 n104;
        public G_button_1 n108;
        public G_button_1 n111;
        public GGroup n113;
        public G_button_1 n115;
        public G_button_1 n116;
        public G_button_1 n117;
        public G_button_1 n118;
        public GButton n114;
        public const string URL = "ui://tpkzz9jlgj9w1k";
        public const string PACKAGE_NAME = "RedPointTest";
        public const string COMPONENT_NAME = "RedPointTestView";

        public static G_RedPointTestView CreateInstance()
        {
            return (G_RedPointTestView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n101 = (G_button_1)GetChildAt(0);
            n102 = (G_button_1)GetChildAt(1);
            n103 = (G_button_1)GetChildAt(2);
            n104 = (G_button_1)GetChildAt(3);
            n108 = (G_button_1)GetChildAt(4);
            n111 = (G_button_1)GetChildAt(5);
            n113 = (GGroup)GetChildAt(6);
            n115 = (G_button_1)GetChildAt(7);
            n116 = (G_button_1)GetChildAt(8);
            n117 = (G_button_1)GetChildAt(9);
            n118 = (G_button_1)GetChildAt(10);
            n114 = (GButton)GetChildAt(11);
        }
    }
}