/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Recharge
{
    public partial class G_RechargeLeftTab : GButton
    {
        public Controller button;
        public GImage select;
        public GTextField tabname;
        public GLoader tabIcon;
        public GImage n42;
        public GTextField tabname2;
        public const string URL = "ui://ynu7b0nvmv0c12";
        public const string PACKAGE_NAME = "Recharge";
        public const string COMPONENT_NAME = "RechargeLeftTab";

        public static G_RechargeLeftTab CreateInstance()
        {
            return (G_RechargeLeftTab)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            select = (GImage)GetChildAt(0);
            tabname = (GTextField)GetChildAt(1);
            tabIcon = (GLoader)GetChildAt(2);
            n42 = (GImage)GetChildAt(3);
            tabname2 = (GTextField)GetChildAt(4);
        }
    }
}