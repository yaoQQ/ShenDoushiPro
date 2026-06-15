/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_ReconnectView : GComponent
    {
        public GTextField title;
        public GImage n5;
        public GImage n6;
        public const string URL = "ui://l64dumk9mm2ii8z";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "ReconnectView";

        public static G_ReconnectView CreateInstance()
        {
            return (G_ReconnectView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title = (GTextField)GetChildAt(0);
            n5 = (GImage)GetChildAt(1);
            n6 = (GImage)GetChildAt(2);
        }
    }
}