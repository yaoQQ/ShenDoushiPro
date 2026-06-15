/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_ArowBtn : GButton
    {
        public GImage rightArow;
        public const string URL = "ui://yizyzd76mv0c32";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "ArowBtn";

        public static G_ArowBtn CreateInstance()
        {
            return (G_ArowBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            rightArow = (GImage)GetChildAt(0);
        }
    }
}