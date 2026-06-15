/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankView : GComponent
    {
        public GButton n83;
        public GList List_tabs;
        public GList List_banner;
        public const string URL = "ui://fxqdojw7djyn19";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankView";

        public static G_RankView CreateInstance()
        {
            return (G_RankView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n83 = (GButton)GetChildAt(0);
            List_tabs = (GList)GetChildAt(1);
            List_banner = (GList)GetChildAt(2);
        }
    }
}