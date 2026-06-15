/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_contentTextScroll : GComponent
    {
        public GRichTextField info;
        public const string URL = "ui://l64dumk9mm2ii9f";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "contentTextScroll";

        public static G_contentTextScroll CreateInstance()
        {
            return (G_contentTextScroll)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            info = (GRichTextField)GetChildAt(0);
        }
    }
}