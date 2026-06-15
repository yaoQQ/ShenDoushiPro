/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_RedPoint : GComponent
    {
        public GImage n2;
        public const string URL = "ui://y4b7yuunsiz5i7a";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "RedPoint";

        public static G_RedPoint CreateInstance()
        {
            return (G_RedPoint)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
        }
    }
}