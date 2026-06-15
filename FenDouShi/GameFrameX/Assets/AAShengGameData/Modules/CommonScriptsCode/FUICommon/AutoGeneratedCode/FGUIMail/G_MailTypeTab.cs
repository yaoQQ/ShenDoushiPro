/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_MailTypeTab : GButton
    {
        public Controller button;
        public GTextField title1;
        public GImage selectimg;
        public GTextField title2;
        public GGroup Select;
        public const string URL = "ui://p4ru1eiciljg9s";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "MailTypeTab";

        public static G_MailTypeTab CreateInstance()
        {
            return (G_MailTypeTab)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            title1 = (GTextField)GetChildAt(0);
            selectimg = (GImage)GetChildAt(1);
            title2 = (GTextField)GetChildAt(2);
            Select = (GGroup)GetChildAt(3);
        }
    }
}