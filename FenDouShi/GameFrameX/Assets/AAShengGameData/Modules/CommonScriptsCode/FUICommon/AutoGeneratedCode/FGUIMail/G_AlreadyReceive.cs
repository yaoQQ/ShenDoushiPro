/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace FGUIMail
{
    public partial class G_AlreadyReceive : GComponent
    {
        public GImage n83;
        public GTextField n84;
        public const string URL = "ui://p4ru1eiciljgal";
        public const string PACKAGE_NAME = "FGUIMail";
        public const string COMPONENT_NAME = "AlreadyReceive";

        public static G_AlreadyReceive CreateInstance()
        {
            return (G_AlreadyReceive)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n83 = (GImage)GetChildAt(0);
            n84 = (GTextField)GetChildAt(1);
        }
    }
}