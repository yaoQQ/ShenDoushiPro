/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_ReceiveReward_Btn : GButton
    {
        public GGraph closeMask;
        public const string URL = "ui://pux1kqsbx27h2y";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_ReceiveReward_Btn";

        public static G_OnHookView_ReceiveReward_Btn CreateInstance()
        {
            return (G_OnHookView_ReceiveReward_Btn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            closeMask = (GGraph)GetChildAt(0);
        }
    }
}