/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_TabList : GComponent
    {
        public GList tabList;
        public const string URL = "ui://oz2qb5cgkmts2s";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "TabList";

        public static G_TabList CreateInstance()
        {
            return (G_TabList)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            tabList = (GList)GetChildAt(0);
        }
    }
}