/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_CloseBtn : GButton
    {
        public GImage n62;
        public GImage n76;
        public const string URL = "ui://gw0bj7t0n0p52t";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "CloseBtn";

        public static G_CloseBtn CreateInstance()
        {
            return (G_CloseBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n62 = (GImage)GetChildAt(0);
            n76 = (GImage)GetChildAt(1);
        }
    }
}