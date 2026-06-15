/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GameItemBag
{
    public partial class G_common_btn_1 : GButton
    {
        public Controller button;
        public GGraph n1;
        public const string URL = "ui://c9152aeqgj9w8";

        public static G_common_btn_1 CreateInstance()
        {
            return (G_common_btn_1)UIPackage.CreateObject("GameItemBag", "common_btn_1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GGraph)GetChildAt(0);
        }
    }
}