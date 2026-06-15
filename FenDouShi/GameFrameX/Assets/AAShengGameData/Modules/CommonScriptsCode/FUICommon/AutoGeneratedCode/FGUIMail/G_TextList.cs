/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_TextList : GComponent
    {
        public GTextField title;
        public GRichTextField content;
        public GButton GoTo;
        public const string URL = "ui://p4ru1eicb21v1n";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "TextList";

        public static G_TextList CreateInstance()
        {
            return (G_TextList)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title = (GTextField)GetChildAt(0);
            content = (GRichTextField)GetChildAt(1);
            GoTo = (GButton)GetChildAt(2);
        }
    }
}