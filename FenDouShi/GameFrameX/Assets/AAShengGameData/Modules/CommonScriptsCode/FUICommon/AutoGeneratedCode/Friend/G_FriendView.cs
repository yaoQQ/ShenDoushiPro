/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_FriendView : GComponent
    {
        public GComponent frameBg;
        public GComponent content;
        public GGroup n207;
        public const string URL = "ui://jyya7ryuvliv4e";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "FriendView";

        public static G_FriendView CreateInstance()
        {
            return (G_FriendView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            frameBg = (GComponent)GetChildAt(0);
            content = (GComponent)GetChildAt(1);
            n207 = (GGroup)GetChildAt(2);
        }
    }
}