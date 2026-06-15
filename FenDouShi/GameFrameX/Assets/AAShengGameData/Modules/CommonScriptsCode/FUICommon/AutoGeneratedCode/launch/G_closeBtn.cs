/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace launch
{
    public partial class G_closeBtn : GButton
    {
        public GImage n15;
        public GTextField n13;
        public const string URL = "ui://ynb47g4ju5vmi5o";
        public const string PACKAGE_NAME = "launch";
        public const string COMPONENT_NAME = "closeBtn";

        public static G_closeBtn CreateInstance()
        {
            return (G_closeBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n15 = (GImage)GetChildAt(0);
            n13 = (GTextField)GetChildAt(1);
        }
    }
}