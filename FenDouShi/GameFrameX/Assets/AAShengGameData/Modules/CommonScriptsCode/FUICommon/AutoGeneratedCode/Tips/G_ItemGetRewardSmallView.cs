/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemGetRewardSmallView : GComponent
    {
        public GComponent viewParent;
        public const string URL = "ui://g2ec8shvixvj1";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemGetRewardSmallView";

        public static G_ItemGetRewardSmallView CreateInstance()
        {
            return (G_ItemGetRewardSmallView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewParent = (GComponent)GetChildAt(0);
        }
    }
}