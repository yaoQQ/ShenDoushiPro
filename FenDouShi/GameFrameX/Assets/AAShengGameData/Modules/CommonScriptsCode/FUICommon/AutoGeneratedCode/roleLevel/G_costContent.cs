/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_costContent : GComponent
    {
        public GImage n27;
        public GTextField costText;
        public const string URL = "ui://oz2qb5cgqwrq38";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "costContent";

        public static G_costContent CreateInstance()
        {
            return (G_costContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n27 = (GImage)GetChildAt(0);
            costText = (GTextField)GetChildAt(1);
        }
    }
}