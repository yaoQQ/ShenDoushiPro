/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_viewMask : GButton
    {
        public GLoader icon;
        public const string URL = "ui://y4b7yuunagyb2i8d";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "viewMask";

        public static G_viewMask CreateInstance()
        {
            return (G_viewMask)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            icon = (GLoader)GetChildAt(0);
        }
    }
}