/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace login
{
    public partial class G_AlertWindow : GComponent
    {
        public Controller c1;
        public GImage bg;
        public GImage bgICon;
        public GTextField noticeText;
        public GImage n5;
        public GTextField title;
        public GImage n7;
        public GImage n8;
        public G_costContent costContent;
        public G_costNum costNum;
        public G_okBtn okBtn;
        public G_closeBtn backBtn;
        public G_ageCloseBtn closeBtn;
        public GGroup n52;
        public const string URL = "ui://l64dumk9mm2ii8k";
        public const string PACKAGE_NAME = "login";
        public const string COMPONENT_NAME = "AlertWindow";

        public static G_AlertWindow CreateInstance()
        {
            return (G_AlertWindow)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            bg = (GImage)GetChildAt(0);
            bgICon = (GImage)GetChildAt(1);
            noticeText = (GTextField)GetChildAt(2);
            n5 = (GImage)GetChildAt(3);
            title = (GTextField)GetChildAt(4);
            n7 = (GImage)GetChildAt(5);
            n8 = (GImage)GetChildAt(6);
            costContent = (G_costContent)GetChildAt(7);
            costNum = (G_costNum)GetChildAt(8);
            okBtn = (G_okBtn)GetChildAt(9);
            backBtn = (G_closeBtn)GetChildAt(10);
            closeBtn = (G_ageCloseBtn)GetChildAt(11);
            n52 = (GGroup)GetChildAt(12);
        }
    }
}