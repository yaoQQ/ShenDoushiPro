/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CommonGoodItem : GComponent
    {
        public Controller button;
        public GLoader Image_quality;
        public GLoader Image_icon;
        public GImage n128;
        public GImage n129;
        public GGroup group_lock;
        public GImage Image_select;
        public GImage n126;
        public GImage n123;
        public GTextField Text_time;
        public GGroup group_time;
        public GImage Imgae_fragment;
        public GImage Image_up;
        public G_RedPoint redPoint;
        public GImage Image_countBg;
        public GTextField Text_count;
        public GGroup group_count;
        public GGroup Frame;
        public const string URL = "ui://y4b7yuunkiy42i9g";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CommonGoodItem";

        public static G_CommonGoodItem CreateInstance()
        {
            return (G_CommonGoodItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            Image_quality = (GLoader)GetChildAt(0);
            Image_icon = (GLoader)GetChildAt(1);
            n128 = (GImage)GetChildAt(2);
            n129 = (GImage)GetChildAt(3);
            group_lock = (GGroup)GetChildAt(4);
            Image_select = (GImage)GetChildAt(5);
            n126 = (GImage)GetChildAt(6);
            n123 = (GImage)GetChildAt(7);
            Text_time = (GTextField)GetChildAt(8);
            group_time = (GGroup)GetChildAt(9);
            Imgae_fragment = (GImage)GetChildAt(10);
            Image_up = (GImage)GetChildAt(11);
            redPoint = (G_RedPoint)GetChildAt(12);
            Image_countBg = (GImage)GetChildAt(13);
            Text_count = (GTextField)GetChildAt(14);
            group_count = (GGroup)GetChildAt(15);
            Frame = (GGroup)GetChildAt(16);
        }
    }
}