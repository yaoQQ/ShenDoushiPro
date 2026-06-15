/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GmCustomPageView : GComponent
    {
        public GList List_content;
        public const string URL = "ui://p65pul3qytao1";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GmCustomPageView";

        public static G_GmCustomPageView CreateInstance()
        {
            return (G_GmCustomPageView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            List_content = (GList)GetChildAt(0);
        }
    }
}