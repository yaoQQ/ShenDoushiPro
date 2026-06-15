/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace AthenaTrial
{
    public partial class G_Button_rank : GButton
    {
        public GImage n12;
        public GImage n13;
        public GTextField n14;
        public const string URL = "ui://ooop1fy6gxoq1x";
        public const string PACKAGE_NAME = "AthenaTrial";
        public const string COMPONENT_NAME = "Button_rank";

        public static G_Button_rank CreateInstance()
        {
            return (G_Button_rank)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n12 = (GImage)GetChildAt(0);
            n13 = (GImage)GetChildAt(1);
            n14 = (GTextField)GetChildAt(2);
        }
    }
}