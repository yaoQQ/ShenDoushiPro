/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_contentText : GComponent
    {
        public GRichTextField Info;
        public GRichTextField n26;
        public const string URL = "ui://l64dumk9kp7oi9g";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "contentText";

        public static G_contentText CreateInstance()
        {
            return (G_contentText)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Info = (GRichTextField)GetChildAt(0);
            n26 = (GRichTextField)GetChildAt(1);
        }
    }
}