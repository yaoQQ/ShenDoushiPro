/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_MonthRewardRender : GComponent
    {
        public GImage n100;
        public GButton itemRender;
        public const string URL = "ui://yizyzd76mv0c31";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "MonthRewardRender";

        public static G_MonthRewardRender CreateInstance()
        {
            return (G_MonthRewardRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n100 = (GImage)GetChildAt(0);
            itemRender = (GButton)GetChildAt(1);
        }
    }
}