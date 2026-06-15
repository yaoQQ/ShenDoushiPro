/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_common_btn_yeqian03_weixuandi_1 : GButton
    {
        public Controller button;
        public GImage n1;
        public const string URL = "ui://fxqdojw7s2lc5";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "common_btn_yeqian03_weixuandi_1";

        public static G_common_btn_yeqian03_weixuandi_1 CreateInstance()
        {
            return (G_common_btn_yeqian03_weixuandi_1)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
        }
    }
}