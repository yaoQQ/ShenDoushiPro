/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_VipLevelUpDescListRender : GComponent
    {
        public GImage n34;
        public GRichTextField n48;
        public const string URL = "ui://yizyzd76mv0c38";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "VipLevelUpDescListRender";

        public static G_VipLevelUpDescListRender CreateInstance()
        {
            return (G_VipLevelUpDescListRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n34 = (GImage)GetChildAt(0);
            n48 = (GRichTextField)GetChildAt(1);
        }
    }
}