/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Recharge
{
    public partial class G_RechargeListItemRender : GButton
    {
        public Controller control;
        public GImage n2;
        public GTextField money;
        public GTextField itemCount;
        public GLoader rechargeIcon;
        public GImage n3;
        public GTextField n7;
        public GGroup doubleTag;
        public const string URL = "ui://ynu7b0nvmv0c13";
        public const string PACKAGE_NAME = "Recharge";
        public const string COMPONENT_NAME = "RechargeListItemRender";

        public static G_RechargeListItemRender CreateInstance()
        {
            return (G_RechargeListItemRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            control = GetControllerAt(0);
            n2 = (GImage)GetChildAt(0);
            money = (GTextField)GetChildAt(1);
            itemCount = (GTextField)GetChildAt(2);
            rechargeIcon = (GLoader)GetChildAt(3);
            n3 = (GImage)GetChildAt(4);
            n7 = (GTextField)GetChildAt(5);
            doubleTag = (GGroup)GetChildAt(6);
        }
    }
}