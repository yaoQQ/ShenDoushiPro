/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Recharge
{
    public partial class G_BgLogHuaWen : GComponent
    {
        public GImage n21;
        public const string URL = "ui://ynu7b0nvmv0c14";
        public const string PACKAGE_NAME = "Recharge";
        public const string COMPONENT_NAME = "BgLogHuaWen";

        public static G_BgLogHuaWen CreateInstance()
        {
            return (G_BgLogHuaWen)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n21 = (GImage)GetChildAt(0);
        }
    }
}