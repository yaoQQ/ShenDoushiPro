/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hero
{
    public partial class G_StarGroup : GComponent
    {
        public GImage n0;
        public GImage n1;
        public GImage n2;
        public GImage n3;
        public GImage n4;
        public GGroup n8;
        public const string URL = "ui://0g1kba0btae91l";
        public const string PACKAGE_NAME = "Hero";
        public const string COMPONENT_NAME = "StarGroup";

        public static G_StarGroup CreateInstance()
        {
            return (G_StarGroup)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GImage)GetChildAt(0);
            n1 = (GImage)GetChildAt(1);
            n2 = (GImage)GetChildAt(2);
            n3 = (GImage)GetChildAt(3);
            n4 = (GImage)GetChildAt(4);
            n8 = (GGroup)GetChildAt(5);
        }
    }
}