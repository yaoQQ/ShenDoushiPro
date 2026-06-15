/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_FrameComponent : GComponent
    {
        public GImage bg1;
        public GImage bg2;
        public GImage tittle1;
        public GImage tittle2;
        public GImage tittle3;
        public GTextField text;
        public G_common_btn_guanbi_xiao_1 closeBtn;
        public const string URL = "ui://y4b7yuunicym6b";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "FrameComponent";

        public static G_FrameComponent CreateInstance()
        {
            return (G_FrameComponent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg1 = (GImage)GetChildAt(0);
            bg2 = (GImage)GetChildAt(1);
            tittle1 = (GImage)GetChildAt(2);
            tittle2 = (GImage)GetChildAt(3);
            tittle3 = (GImage)GetChildAt(4);
            text = (GTextField)GetChildAt(5);
            closeBtn = (G_common_btn_guanbi_xiao_1)GetChildAt(6);
        }
    }
}