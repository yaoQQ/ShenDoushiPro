/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendRedBtn : GButton
    {
        public Controller button;
        public GImage n1;
        public GLoader icon;
        public const string URL = "ui://jyya7ryuvliv1v";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendRedBtn";

        public static G_FriendRedBtn CreateInstance()
        {
            return (G_FriendRedBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n1 = (GImage)GetChildAt(0);
            icon = (GLoader)GetChildAt(1);
        }
    }
}