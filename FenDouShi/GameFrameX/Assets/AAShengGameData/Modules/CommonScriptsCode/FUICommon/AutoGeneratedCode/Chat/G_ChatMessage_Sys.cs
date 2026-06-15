/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatMessage_Sys : GComponent
    {
        public GImage n15;
        public GTextField title;
        public GImage n17;
        public GTextField sys;
        public const string URL = "ui://gw0bj7t0n0p53t";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatMessage_Sys";

        public static G_ChatMessage_Sys CreateInstance()
        {
            return (G_ChatMessage_Sys)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n15 = (GImage)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
            n17 = (GImage)GetChildAt(2);
            sys = (GTextField)GetChildAt(3);
        }
    }
}