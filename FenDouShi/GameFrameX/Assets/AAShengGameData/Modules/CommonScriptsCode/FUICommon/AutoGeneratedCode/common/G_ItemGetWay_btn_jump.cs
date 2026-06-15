/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_ItemGetWay_btn_jump : GButton
    {
        public GLoader Image_icon;
        public const string URL = "ui://y4b7yuunkiy4e";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "ItemGetWay_btn_jump";

        public static G_ItemGetWay_btn_jump CreateInstance()
        {
            return (G_ItemGetWay_btn_jump)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_icon = (GLoader)GetChildAt(0);
        }
    }
}