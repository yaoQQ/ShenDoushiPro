/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_costNum : GComponent
    {
        public GTextField n37;
        public GTextField n38;
        public GButton item;
        public const string URL = "ui://l64dumk9mm2ii8p";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "costNum";

        public static G_costNum CreateInstance()
        {
            return (G_costNum)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n37 = (GTextField)GetChildAt(0);
            n38 = (GTextField)GetChildAt(1);
            item = (GButton)GetChildAt(2);
        }
    }
}