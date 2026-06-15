/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_PlayerHeadIcon : GComponent
    {
        public GImage n44;
        public GLoader playerIcon;
        public GTextField levelText;
        public const string URL = "ui://l64dumk9b2eri8d";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "PlayerHeadIcon";

        public static G_PlayerHeadIcon CreateInstance()
        {
            return (G_PlayerHeadIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n44 = (GImage)GetChildAt(0);
            playerIcon = (GLoader)GetChildAt(1);
            levelText = (GTextField)GetChildAt(2);
        }
    }
}