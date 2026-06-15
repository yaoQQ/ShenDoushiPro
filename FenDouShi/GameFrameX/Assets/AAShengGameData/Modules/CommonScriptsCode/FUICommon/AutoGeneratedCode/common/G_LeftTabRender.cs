/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_LeftTabRender : GButton
    {
        public Controller button;
        public GImage normal;
        public GImage select1;
        public GLoader tabIcon;
        public GTextField n6;
        public GGroup n9;
        public const string URL = "ui://y4b7yuunfp972i9b";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "LeftTabRender";

        public static G_LeftTabRender CreateInstance()
        {
            return (G_LeftTabRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            normal = (GImage)GetChildAt(0);
            select1 = (GImage)GetChildAt(1);
            tabIcon = (GLoader)GetChildAt(2);
            n6 = (GTextField)GetChildAt(3);
            n9 = (GGroup)GetChildAt(4);
        }
    }
}