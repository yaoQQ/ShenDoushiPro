/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_AreanVerItem2 : GComponent
    {
        public GImage n43;
        public GImage n44;
        public GImage n45;
        public GImage n46;
        public GImage n47;
        public GImage n48;
        public GTextField n49;
        public GImage n50;
        public GImage n68;
        public GImage n52;
        public GTextField n53;
        public const string URL = "ui://n8s4flrye0dt2g";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "AreanVerItem2";

        public static G_AreanVerItem2 CreateInstance()
        {
            return (G_AreanVerItem2)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n43 = (GImage)GetChildAt(0);
            n44 = (GImage)GetChildAt(1);
            n45 = (GImage)GetChildAt(2);
            n46 = (GImage)GetChildAt(3);
            n47 = (GImage)GetChildAt(4);
            n48 = (GImage)GetChildAt(5);
            n49 = (GTextField)GetChildAt(6);
            n50 = (GImage)GetChildAt(7);
            n68 = (GImage)GetChildAt(8);
            n52 = (GImage)GetChildAt(9);
            n53 = (GTextField)GetChildAt(10);
        }
    }
}