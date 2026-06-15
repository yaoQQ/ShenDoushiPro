/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemConsumeRender : GComponent
    {
        public GImage n36;
        public GTextField Text_consume;
        public GLoader Image_icon;
        public const string URL = "ui://g2ec8shvsjcol";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemConsumeRender";

        public static G_ItemConsumeRender CreateInstance()
        {
            return (G_ItemConsumeRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n36 = (GImage)GetChildAt(0);
            Text_consume = (GTextField)GetChildAt(1);
            Image_icon = (GLoader)GetChildAt(2);
        }
    }
}