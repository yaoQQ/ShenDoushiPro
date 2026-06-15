/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendBlackPanel : GComponent
    {
        public GTextField blackCountLabel;
        public GList blackList;
        public GImage n57;
        public GTextField n58;
        public GGroup empty;
        public const string URL = "ui://jyya7ryuvliv4l";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendBlackPanel";

        public static G_FriendBlackPanel CreateInstance()
        {
            return (G_FriendBlackPanel)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            blackCountLabel = (GTextField)GetChildAt(0);
            blackList = (GList)GetChildAt(1);
            n57 = (GImage)GetChildAt(2);
            n58 = (GTextField)GetChildAt(3);
            empty = (GGroup)GetChildAt(4);
        }
    }
}