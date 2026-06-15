/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_CreatePlayerView : GComponent
    {
        public GImage bg;
        public G_RandomBtn RandomBtn;
        public GImage bgICon;
        public GImage n18;
        public GImage n12;
        public GTextInput userNameInput;
        public GTextField n14;
        public GTextField title;
        public GImage n20;
        public GImage n21;
        public G_okBtn okBtn;
        public G_ageCloseBtn closeBtn;
        public GGroup n27;
        public const string URL = "ui://l64dumk9mm2ii8t";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "CreatePlayerView";

        public static G_CreatePlayerView CreateInstance()
        {
            return (G_CreatePlayerView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            RandomBtn = (G_RandomBtn)GetChildAt(1);
            bgICon = (GImage)GetChildAt(2);
            n18 = (GImage)GetChildAt(3);
            n12 = (GImage)GetChildAt(4);
            userNameInput = (GTextInput)GetChildAt(5);
            n14 = (GTextField)GetChildAt(6);
            title = (GTextField)GetChildAt(7);
            n20 = (GImage)GetChildAt(8);
            n21 = (GImage)GetChildAt(9);
            okBtn = (G_okBtn)GetChildAt(10);
            closeBtn = (G_ageCloseBtn)GetChildAt(11);
            n27 = (GGroup)GetChildAt(12);
        }
    }
}