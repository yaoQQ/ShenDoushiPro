/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_VipDescRender : GComponent
    {
        public GImage n61;
        public GTextField n122;
        public const string URL = "ui://yizyzd76mv0c34";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "VipDescRender";

        public static G_VipDescRender CreateInstance()
        {
            return (G_VipDescRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n61 = (GImage)GetChildAt(0);
            n122 = (GTextField)GetChildAt(1);
        }
    }
}