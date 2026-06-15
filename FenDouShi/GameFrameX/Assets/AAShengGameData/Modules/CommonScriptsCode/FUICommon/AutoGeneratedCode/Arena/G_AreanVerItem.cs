/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_AreanVerItem : GComponent
    {
        public GImage n34;
        public GImage n35;
        public GImage n36;
        public GImage n37;
        public GImage n38;
        public GImage n39;
        public GImage n40;
        public GTextField n41;
        public const string URL = "ui://n8s4flrye0dt2f";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "AreanVerItem";

        public static G_AreanVerItem CreateInstance()
        {
            return (G_AreanVerItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n34 = (GImage)GetChildAt(0);
            n35 = (GImage)GetChildAt(1);
            n36 = (GImage)GetChildAt(2);
            n37 = (GImage)GetChildAt(3);
            n38 = (GImage)GetChildAt(4);
            n39 = (GImage)GetChildAt(5);
            n40 = (GImage)GetChildAt(6);
            n41 = (GTextField)GetChildAt(7);
        }
    }
}