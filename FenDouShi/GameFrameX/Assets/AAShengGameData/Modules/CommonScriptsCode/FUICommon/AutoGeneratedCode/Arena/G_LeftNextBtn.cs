/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_LeftNextBtn : GButton
    {
        public GImage n62;
        public GImage n63;
        public const string URL = "ui://n8s4flrye0dt2i";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "LeftNextBtn";

        public static G_LeftNextBtn CreateInstance()
        {
            return (G_LeftNextBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n62 = (GImage)GetChildAt(0);
            n63 = (GImage)GetChildAt(1);
        }
    }
}