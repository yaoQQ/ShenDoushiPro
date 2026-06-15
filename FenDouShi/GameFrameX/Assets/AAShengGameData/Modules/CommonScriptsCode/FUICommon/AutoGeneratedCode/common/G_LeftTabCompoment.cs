/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_LeftTabCompoment : GComponent
    {
        public GList tabList;
        public const string URL = "ui://y4b7yuunfp972i92";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "LeftTabCompoment";

        public static G_LeftTabCompoment CreateInstance()
        {
            return (G_LeftTabCompoment)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tabList = (GList)GetChildAt(0);
        }
    }
}