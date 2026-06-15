/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace OnHookUI
{
    public partial class G_OnHookView_Detail_Chapter_Line : GComponent
    {
        public GImage n14;
        public const string URL = "ui://pux1kqsbpb4z2q";
        public const string PACKAGE_NAME = "OnHookUI";
        public const string COMPONENT_NAME = "OnHookView_Detail_Chapter_Line";

        public static G_OnHookView_Detail_Chapter_Line CreateInstance()
        {
            return (G_OnHookView_Detail_Chapter_Line)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n14 = (GImage)GetChildAt(0);
        }
    }
}