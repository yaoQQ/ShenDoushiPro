/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_costContent : GComponent
    {
        public GTextField n18;
        public GTextField n19;
        public GTextField n20;
        public GButton item;
        public const string URL = "ui://l64dumk9mm2ii8o";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "costContent";

        public static G_costContent CreateInstance()
        {
            return (G_costContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n18 = (GTextField)GetChildAt(0);
            n19 = (GTextField)GetChildAt(1);
            n20 = (GTextField)GetChildAt(2);
            item = (GButton)GetChildAt(3);
        }
    }
}