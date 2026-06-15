/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_chatBtn : GButton
    {
        public GImage n100;
        public GImage n101;
        public GTextField n102;
        public const string URL = "ui://oz2qb5cgkmtst";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "chatBtn";

        public static G_chatBtn CreateInstance()
        {
            return (G_chatBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n100 = (GImage)GetChildAt(0);
            n101 = (GImage)GetChildAt(1);
            n102 = (GTextField)GetChildAt(2);
        }
    }
}