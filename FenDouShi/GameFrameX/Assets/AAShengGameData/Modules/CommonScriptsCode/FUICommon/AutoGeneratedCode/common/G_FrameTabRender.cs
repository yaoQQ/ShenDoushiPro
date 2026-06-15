/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_FrameTabRender : GButton
    {
        public Controller button;
        public GImage n32;
        public GImage n41;
        public GImage n40;
        public GImage n43;
        public GTextField title;
        public GLoader icon;
        public const string URL = "ui://y4b7yuunvliv2iec";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "FrameTabRender";

        public static G_FrameTabRender CreateInstance()
        {
            return (G_FrameTabRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n32 = (GImage)GetChildAt(0);
            n41 = (GImage)GetChildAt(1);
            n40 = (GImage)GetChildAt(2);
            n43 = (GImage)GetChildAt(3);
            title = (GTextField)GetChildAt(4);
            icon = (GLoader)GetChildAt(5);
        }
    }
}