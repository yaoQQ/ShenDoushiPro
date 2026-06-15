/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_common_btn_02_1 : GButton
    {
        public Controller button;
        public GLoader Image_button;
        public GTextField Text_draw;
        public GComponent redPoint;
        public const string URL = "ui://i3fvjlvyhsku8";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "common_btn_02_1";

        public static G_common_btn_02_1 CreateInstance()
        {
            return (G_common_btn_02_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_button = (GLoader)GetChildAt(0);
            Text_draw = (GTextField)GetChildAt(1);
            redPoint = (GComponent)GetChildAt(2);
        }
    }
}