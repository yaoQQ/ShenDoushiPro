/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_FrameLeftComponent : GComponent
    {
        public GImage n104;
        public GList tabList;
        public const string URL = "ui://y4b7yuunvliv2ieb";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "FrameLeftComponent";

        public static G_FrameLeftComponent CreateInstance()
        {
            return (G_FrameLeftComponent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n104 = (GImage)GetChildAt(0);
            tabList = (GList)GetChildAt(1);
        }
    }
}