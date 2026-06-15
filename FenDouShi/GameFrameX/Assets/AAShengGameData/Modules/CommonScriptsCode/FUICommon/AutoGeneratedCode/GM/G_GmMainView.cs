/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GmMainView : GComponent
    {
        public GLoader bg;
        public GComponent viewParent;
        public G_GMTabListCompoment List_tab;
        public GButton closoButton;
        public GGroup n9;
        public const string URL = "ui://p65pul3qn5xh0";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GmMainView";

        public static G_GmMainView CreateInstance()
        {
            return (G_GmMainView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GLoader)GetChildAt(0);
            viewParent = (GComponent)GetChildAt(1);
            List_tab = (G_GMTabListCompoment)GetChildAt(2);
            closoButton = (GButton)GetChildAt(3);
            n9 = (GGroup)GetChildAt(4);
        }
    }
}