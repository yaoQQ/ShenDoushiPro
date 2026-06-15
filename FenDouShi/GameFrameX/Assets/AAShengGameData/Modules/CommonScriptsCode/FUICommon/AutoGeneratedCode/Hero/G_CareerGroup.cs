/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Hero
{
    public partial class G_CareerGroup : GComponent
    {
        public GImage n1;
        public GImage n2;
        public GImage n3;
        public GGroup n7;
        public const string URL = "ui://0g1kba0btae91k";
        public const string PACKAGE_NAME = "Hero";
        public const string COMPONENT_NAME = "CareerGroup";

        public static G_CareerGroup CreateInstance()
        {
            return (G_CareerGroup)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n1 = (GImage)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            n3 = (GImage)GetChildAt(2);
            n7 = (GGroup)GetChildAt(3);
        }
    }
}