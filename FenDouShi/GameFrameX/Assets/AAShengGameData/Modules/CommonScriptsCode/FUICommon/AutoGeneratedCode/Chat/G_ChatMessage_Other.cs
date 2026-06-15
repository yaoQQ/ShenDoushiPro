/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_ChatMessage_Other : GComponent
    {
        public GImage n14;
        public GImage n15;
        public GTextField level;
        public GTextField name;
        public GImage n24;
        public GTextField content;
        public GImage n20;
        public GImage woman;
        public GImage man;
        public GTextField n22;
        public GImage n26;
        public GTextField city;
        public GGroup n30;
        public const string URL = "ui://gw0bj7t0n0p53m";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "ChatMessage_Other";

        public static G_ChatMessage_Other CreateInstance()
        {
            return (G_ChatMessage_Other)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n14 = (GImage)GetChildAt(0);
            n15 = (GImage)GetChildAt(1);
            level = (GTextField)GetChildAt(2);
            name = (GTextField)GetChildAt(3);
            n24 = (GImage)GetChildAt(4);
            content = (GTextField)GetChildAt(5);
            n20 = (GImage)GetChildAt(6);
            woman = (GImage)GetChildAt(7);
            man = (GImage)GetChildAt(8);
            n22 = (GTextField)GetChildAt(9);
            n26 = (GImage)GetChildAt(10);
            city = (GTextField)GetChildAt(11);
            n30 = (GGroup)GetChildAt(12);
        }
    }
}