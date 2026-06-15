/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_PanelContent : GComponent
    {
        public GGraph content;
        public const string URL = "ui://y4b7yuunvliv2ieq";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "PanelContent";

        public static G_PanelContent CreateInstance()
        {
            return (G_PanelContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            content = (GGraph)GetChildAt(0);
        }
    }
}