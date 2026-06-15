/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Recharge
{
    public partial class G_RechargeView : GComponent
    {
        public GImage n1;
        public GList leftTabList;
        public GList rechargeList;
        public const string URL = "ui://ynu7b0nvmv0co";
        public const string PACKAGE_NAME = "Recharge";
        public const string COMPONENT_NAME = "RechargeView";

        public static G_RechargeView CreateInstance()
        {
            return (G_RechargeView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n1 = (GImage)GetChildAt(0);
            leftTabList = (GList)GetChildAt(1);
            rechargeList = (GList)GetChildAt(2);
        }
    }
}