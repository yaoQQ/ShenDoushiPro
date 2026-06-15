/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemSelect : GButton
    {
        public GImage Imgae_selectBg;
        public GImage Imgae_select;
        public const string URL = "ui://g2ec8shvsjcof";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemSelect";

        public static G_ItemSelect CreateInstance()
        {
            return (G_ItemSelect)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Imgae_selectBg = (GImage)GetChildAt(0);
            Imgae_select = (GImage)GetChildAt(1);
        }
    }
}