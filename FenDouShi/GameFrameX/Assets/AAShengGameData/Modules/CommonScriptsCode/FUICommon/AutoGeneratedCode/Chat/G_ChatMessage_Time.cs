/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatMessage_Time : GComponent
    {
        public GImage n45;
        public GTextField time;
        public GImage n44;
        public GGroup Time;
        public const string URL = "ui://gw0bj7t0xf693y";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatMessage_Time";

        public static G_ChatMessage_Time CreateInstance()
        {
            return (G_ChatMessage_Time)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n45 = (GImage)GetChildAt(0);
            time = (GTextField)GetChildAt(1);
            n44 = (GImage)GetChildAt(2);
            Time = (GGroup)GetChildAt(3);
        }
    }
}