/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_VoiceBtn : GButton
    {
        public GImage VoiceBtn;
        public const string URL = "ui://gw0bj7t0ozwm41";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "VoiceBtn";

        public static G_VoiceBtn CreateInstance()
        {
            return (G_VoiceBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            VoiceBtn = (GImage)GetChildAt(0);
        }
    }
}