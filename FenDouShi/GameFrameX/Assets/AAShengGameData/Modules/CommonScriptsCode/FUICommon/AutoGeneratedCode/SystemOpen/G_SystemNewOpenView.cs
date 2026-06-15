/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace SystemOpen
{
    public partial class G_SystemNewOpenView : GComponent
    {
        public GButton viewMask;
        public GImage n2;
        public GLoader image_icon;
        public GImage n4;
        public GTextField Text_name;
        public const string URL = "ui://i3fvjlvy105b2p";
        public const string PACKAGE_NAME = "SystemOpen";
        public const string COMPONENT_NAME = "SystemNewOpenView";

        public static G_SystemNewOpenView CreateInstance()
        {
            return (G_SystemNewOpenView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewMask = (GButton)GetChildAt(0);
            n2 = (GImage)GetChildAt(1);
            image_icon = (GLoader)GetChildAt(2);
            n4 = (GImage)GetChildAt(3);
            Text_name = (GTextField)GetChildAt(4);
        }
    }
}