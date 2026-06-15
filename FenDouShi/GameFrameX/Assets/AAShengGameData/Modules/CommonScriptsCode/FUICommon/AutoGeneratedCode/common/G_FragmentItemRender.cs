/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_FragmentItemRender : GComponent
    {
        public G_CommonGoodItem commonGoodItem;
        public GImage Image_sliderBg;
        public GImage Image_slider;
        public GTextField Text_value;
        public GGroup Frame;
        public const string URL = "ui://y4b7yuunqxuv2i92";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "FragmentItemRender";

        public static G_FragmentItemRender CreateInstance()
        {
            return (G_FragmentItemRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            commonGoodItem = (G_CommonGoodItem)GetChildAt(0);
            Image_sliderBg = (GImage)GetChildAt(1);
            Image_slider = (GImage)GetChildAt(2);
            Text_value = (GTextField)GetChildAt(3);
            Frame = (GGroup)GetChildAt(4);
        }
    }
}