/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_vipExpProgress : GProgressBar
    {
        public GImage n77;
        public GImage bar;
        public const string URL = "ui://yizyzd76mv0c33";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "vipExpProgress";

        public static G_vipExpProgress CreateInstance()
        {
            return (G_vipExpProgress)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n77 = (GImage)GetChildAt(0);
            bar = (GImage)GetChildAt(1);
        }
    }
}