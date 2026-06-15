/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GmTabItem : GComponent
    {
        public GButton Button;
        public const string URL = "ui://p65pul3qqa5m7";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GmTabItem";

        public static G_GmTabItem CreateInstance()
        {
            return (G_GmTabItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Button = (GButton)GetChildAt(0);
        }
    }
}