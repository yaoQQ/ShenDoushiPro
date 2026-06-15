/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_SelServerItem : GButton
    {
        public Controller button;
        public GImage n40;
        public GImage n53;
        public G_PlayerHeadIcon PlayerHeadIcon;
        public GTextField title;
        public G_ServerStatusIcon ServerStatusIcon;
        public GGroup playerLocation;
        public GTextField PlayerName;
        public GTextField LevelText;
        public GGroup PlayerTextItem;
        public GGroup n51;
        public const string URL = "ui://l64dumk9b2eri8e";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "SelServerItem";

        public static G_SelServerItem CreateInstance()
        {
            return (G_SelServerItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n40 = (GImage)GetChildAt(0);
            n53 = (GImage)GetChildAt(1);
            PlayerHeadIcon = (G_PlayerHeadIcon)GetChildAt(2);
            title = (GTextField)GetChildAt(3);
            ServerStatusIcon = (G_ServerStatusIcon)GetChildAt(4);
            playerLocation = (GGroup)GetChildAt(5);
            PlayerName = (GTextField)GetChildAt(6);
            LevelText = (GTextField)GetChildAt(7);
            PlayerTextItem = (GGroup)GetChildAt(8);
            n51 = (GGroup)GetChildAt(9);
        }
    }
}