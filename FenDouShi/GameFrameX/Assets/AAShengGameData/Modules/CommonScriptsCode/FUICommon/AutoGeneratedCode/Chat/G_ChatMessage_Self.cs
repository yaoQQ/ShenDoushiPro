/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatMessage_Self : GComponent
    {
        public GTextField name;
        public GImage n26;
        public GImage n27;
        public GTextField level;
        public GImage n29;
        public GImage woman;
        public GImage man;
        public GTextField power;
        public GImage n72;
        public GTextField content;
        public GImage n74;
        public GTextField city;
        public GGroup Frame;
        public const string URL = "ui://gw0bj7t0n0p53n";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatMessage_Self";

        public static G_ChatMessage_Self CreateInstance()
        {
            return (G_ChatMessage_Self)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = (GTextField)GetChildAt(0);
            n26 = (GImage)GetChildAt(1);
            n27 = (GImage)GetChildAt(2);
            level = (GTextField)GetChildAt(3);
            n29 = (GImage)GetChildAt(4);
            woman = (GImage)GetChildAt(5);
            man = (GImage)GetChildAt(6);
            power = (GTextField)GetChildAt(7);
            n72 = (GImage)GetChildAt(8);
            content = (GTextField)GetChildAt(9);
            n74 = (GImage)GetChildAt(10);
            city = (GTextField)GetChildAt(11);
            Frame = (GGroup)GetChildAt(12);
        }
    }
}