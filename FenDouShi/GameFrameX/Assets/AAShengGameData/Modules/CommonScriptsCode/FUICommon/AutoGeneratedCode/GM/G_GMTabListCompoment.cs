/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GMTabListCompoment : GComponent
    {
        public GList List_tab;
        public const string URL = "ui://p65pul3qhskua";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GMTabListCompoment";

        public static G_GMTabListCompoment CreateInstance()
        {
            return (G_GMTabListCompoment)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            List_tab = (GList)GetChildAt(0);
        }
    }
}