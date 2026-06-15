/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_FrameBgComponent : GComponent
    {
        public GImage frameBg;
        public GTextField frameTitle;
        public G_common_btn_guanbi_da_1 closeBtn;
        public G_FrameLeftComponent FrameLeftCompoent;
        public const string URL = "ui://y4b7yuunvliv2iea";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "FrameBgComponent";

        public static G_FrameBgComponent CreateInstance()
        {
            return (G_FrameBgComponent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frameBg = (GImage)GetChildAt(0);
            frameTitle = (GTextField)GetChildAt(1);
            closeBtn = (G_common_btn_guanbi_da_1)GetChildAt(2);
            FrameLeftCompoent = (G_FrameLeftComponent)GetChildAt(3);
        }
    }
}