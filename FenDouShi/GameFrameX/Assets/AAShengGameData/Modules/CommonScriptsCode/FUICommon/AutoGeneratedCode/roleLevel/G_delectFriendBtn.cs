/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_delectFriendBtn : GButton
    {
        public GImage n104;
        public GImage n107;
        public GTextField n106;
        public const string URL = "ui://oz2qb5cgkmtss";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "delectFriendBtn";

        public static G_delectFriendBtn CreateInstance()
        {
            return (G_delectFriendBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n104 = (GImage)GetChildAt(0);
            n107 = (GImage)GetChildAt(1);
            n106 = (GTextField)GetChildAt(2);
        }
    }
}