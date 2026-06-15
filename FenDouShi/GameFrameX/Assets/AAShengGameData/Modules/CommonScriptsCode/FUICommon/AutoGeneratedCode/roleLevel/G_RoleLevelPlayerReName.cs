/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelPlayerReName : GComponent
    {
        public GImage n2;
        public GImage n4;
        public GImage n6;
        public GImage n7;
        public GImage n8;
        public GTextField n13;
        public GTextField n15;
        public GTextField n17;
        public GTextField n18;
        public GImage n21;
        public GTextInput InputText;
        public GTextField title;
        public G_costContent coinContent;
        public GButton okBtn;
        public GButton closeBtn;
        public GGroup n31;
        public const string URL = "ui://oz2qb5cgi1yt3";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelPlayerReName";

        public static G_RoleLevelPlayerReName CreateInstance()
        {
            return (G_RoleLevelPlayerReName)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n6 = (GImage)GetChildAt(2);
            n7 = (GImage)GetChildAt(3);
            n8 = (GImage)GetChildAt(4);
            n13 = (GTextField)GetChildAt(5);
            n15 = (GTextField)GetChildAt(6);
            n17 = (GTextField)GetChildAt(7);
            n18 = (GTextField)GetChildAt(8);
            n21 = (GImage)GetChildAt(9);
            InputText = (GTextInput)GetChildAt(10);
            title = (GTextField)GetChildAt(11);
            coinContent = (G_costContent)GetChildAt(12);
            okBtn = (GButton)GetChildAt(13);
            closeBtn = (GButton)GetChildAt(14);
            n31 = (GGroup)GetChildAt(15);
        }
    }
}