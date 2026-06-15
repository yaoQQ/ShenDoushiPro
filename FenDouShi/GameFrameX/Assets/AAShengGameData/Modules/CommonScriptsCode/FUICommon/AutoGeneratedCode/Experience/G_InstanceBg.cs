/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Experience
{
    public partial class G_InstanceBg : GComponent
    {
        public Controller c1;
        public GImage n4;
        public GImage n66;
        public const string URL = "ui://neqo767fq1gdr";
        public const string PACKAGE_NAME = "Experience";
        public const string COMPONENT_NAME = "InstanceBg";

        public static G_InstanceBg CreateInstance()
        {
            return (G_InstanceBg)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            n4 = (GImage)GetChildAt(0);
            n66 = (GImage)GetChildAt(1);
        }
    }
}