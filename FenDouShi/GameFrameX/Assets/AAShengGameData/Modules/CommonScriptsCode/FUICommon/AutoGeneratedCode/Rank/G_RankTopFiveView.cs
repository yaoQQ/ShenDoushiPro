/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankTopFiveView : GComponent
    {
        public GImage n2;
        public GButton Button_click;
        public GImage n4;
        public GImage n6;
        public GTextField n66;
        public GList List_banner;
        public const string URL = "ui://fxqdojw7ogo410";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankTopFiveView";

        public static G_RankTopFiveView CreateInstance()
        {
            return (G_RankTopFiveView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            Button_click = (GButton)GetChildAt(1);
            n4 = (GImage)GetChildAt(2);
            n6 = (GImage)GetChildAt(3);
            n66 = (GTextField)GetChildAt(4);
            List_banner = (GList)GetChildAt(5);
        }
    }
}