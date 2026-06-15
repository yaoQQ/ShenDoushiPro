/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_Button_reward : GButton
    {
        public GImage n115;
        public GImage n94;
        public GImage n95;
        public GComponent redPoint;
        public const string URL = "ui://fxqdojw7hhlq1z";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "Button_reward";

        public static G_Button_reward CreateInstance()
        {
            return (G_Button_reward)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n115 = (GImage)GetChildAt(0);
            n94 = (GImage)GetChildAt(1);
            n95 = (GImage)GetChildAt(2);
            redPoint = (GComponent)GetChildAt(3);
        }
    }
}