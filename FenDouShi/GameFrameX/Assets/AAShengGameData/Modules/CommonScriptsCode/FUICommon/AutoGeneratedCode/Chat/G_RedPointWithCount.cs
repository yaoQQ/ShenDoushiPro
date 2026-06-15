/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Chat
{
    public partial class G_RedPointWithCount : GComponent
    {
        public GImage bg;
        public GTextField count;
        public const string URL = "ui://gw0bj7t0n0p53e";
        public const string PACKAGE_NAME = "Chat";
        public const string COMPONENT_NAME = "RedPointWithCount";

        public static G_RedPointWithCount CreateInstance()
        {
            return (G_RedPointWithCount)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            count = (GTextField)GetChildAt(1);
        }
    }
}