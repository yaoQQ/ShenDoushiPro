/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_common_btn_yeqian01_weixuan_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://y4b7yuunswrc2i85";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "common_btn_yeqian01_weixuan_1";

        public static G_common_btn_yeqian01_weixuan_1 CreateInstance()
        {
            return (G_common_btn_yeqian01_weixuan_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}