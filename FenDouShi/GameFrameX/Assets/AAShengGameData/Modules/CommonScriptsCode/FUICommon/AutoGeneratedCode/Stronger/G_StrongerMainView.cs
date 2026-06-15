/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Stronger
{
    public partial class G_StrongerMainView : GComponent
    {
        public GImage n49;
        public GImage n37;
        public GTextField n38;
        public GImage n39;
        public GImage n51;
        public GImage n41;
        public GLoader n43;
        public GTextField n45;
        public GTextField n46;
        public GTextField n47;
        public GTextField n48;
        public GImage n53;
        public GGroup n50;
        public const string URL = "ui://tybgq1lnhecdj";
        public const string PACKAGE_NAME = "Stronger";
        public const string COMPONENT_NAME = "StrongerMainView";

        public static G_StrongerMainView CreateInstance()
        {
            return (G_StrongerMainView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n49 = (GImage)GetChildAt(0);
            n37 = (GImage)GetChildAt(1);
            n38 = (GTextField)GetChildAt(2);
            n39 = (GImage)GetChildAt(3);
            n51 = (GImage)GetChildAt(4);
            n41 = (GImage)GetChildAt(5);
            n43 = (GLoader)GetChildAt(6);
            n45 = (GTextField)GetChildAt(7);
            n46 = (GTextField)GetChildAt(8);
            n47 = (GTextField)GetChildAt(9);
            n48 = (GTextField)GetChildAt(10);
            n53 = (GImage)GetChildAt(11);
            n50 = (GGroup)GetChildAt(12);
        }
    }
}