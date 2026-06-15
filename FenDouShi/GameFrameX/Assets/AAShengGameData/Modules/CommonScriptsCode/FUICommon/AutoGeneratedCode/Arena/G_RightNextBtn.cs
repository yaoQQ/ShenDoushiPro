/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_RightNextBtn : GButton
    {
        public GImage n60;
        public GImage n61;
        public const string URL = "ui://n8s4flrye0dt2j";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "RightNextBtn";

        public static G_RightNextBtn CreateInstance()
        {
            return (G_RightNextBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n60 = (GImage)GetChildAt(0);
            n61 = (GImage)GetChildAt(1);
        }
    }
}