/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Training
{
    public partial class G_YaDianNaDungeon : GComponent
    {
        public GImage n56;
        public GImage n62;
        public GTextField n57;
        public const string URL = "ui://neqo767fq1gdp";
        public const string PACKAGE_NAME = "Training";
        public const string COMPONENT_NAME = "YaDianNaDungeon";

        public static G_YaDianNaDungeon CreateInstance()
        {
            return (G_YaDianNaDungeon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n56 = (GImage)GetChildAt(0);
            n62 = (GImage)GetChildAt(1);
            n57 = (GTextField)GetChildAt(2);
        }
    }
}