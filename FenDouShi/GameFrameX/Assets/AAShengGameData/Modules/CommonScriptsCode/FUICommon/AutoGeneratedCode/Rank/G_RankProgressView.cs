/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankProgressView : GComponent
    {
        public GImage n2;
        public GImage n3;
        public G_common_btn_yeqian03_weixuandi_1 n130;
        public GImage n5;
        public GButton Button_close;
        public GImage n94;
        public GList List_tabs;
        public GList list_reward;
        public const string URL = "ui://fxqdojw7hn6j1o";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankProgressView";

        public static G_RankProgressView CreateInstance()
        {
            return (G_RankProgressView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            n3 = (GImage)GetChildAt(1);
            n130 = (G_common_btn_yeqian03_weixuandi_1)GetChildAt(2);
            n5 = (GImage)GetChildAt(3);
            Button_close = (GButton)GetChildAt(4);
            n94 = (GImage)GetChildAt(5);
            List_tabs = (GList)GetChildAt(6);
            list_reward = (GList)GetChildAt(7);
        }
    }
}