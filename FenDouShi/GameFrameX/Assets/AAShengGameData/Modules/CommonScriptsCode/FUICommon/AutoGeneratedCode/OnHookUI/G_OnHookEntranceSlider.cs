/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookEntranceSlider : GProgressBar
    {
        public GImage n0;
        public GImage bar;
        public const string URL = "ui://pux1kqsbpb4z2t";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookEntranceSlider";

        public static G_OnHookEntranceSlider CreateInstance()
        {
            return (G_OnHookEntranceSlider)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GImage)GetChildAt(0);
            bar = (GImage)GetChildAt(1);
        }
    }
}