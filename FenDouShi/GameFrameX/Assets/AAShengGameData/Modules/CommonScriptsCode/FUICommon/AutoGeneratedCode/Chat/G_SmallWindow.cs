/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_SmallWindow : GComponent
    {
        public GList MsgList;
        public const string URL = "ui://gw0bj7t0rk1g48";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "SmallWindow";

        public static G_SmallWindow CreateInstance()
        {
            return (G_SmallWindow)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            MsgList = (GList)GetChildAt(0);
        }
    }
}