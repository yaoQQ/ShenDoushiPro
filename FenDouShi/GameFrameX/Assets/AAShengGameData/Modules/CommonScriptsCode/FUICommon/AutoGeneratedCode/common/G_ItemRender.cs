/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_ItemRender : GButton
    {
        public G_viewParent root;
        public GLoader Image_empty;
        public GLoader Editor_preview;
        public G_viewMask Button_empty;
        public const string URL = "ui://y4b7yuunhskui63";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "ItemRender";

        public static G_ItemRender CreateInstance()
        {
            return (G_ItemRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            root = (G_viewParent)GetChildAt(0);
            Image_empty = (GLoader)GetChildAt(1);
            Editor_preview = (GLoader)GetChildAt(2);
            Button_empty = (G_viewMask)GetChildAt(3);
        }
    }
}