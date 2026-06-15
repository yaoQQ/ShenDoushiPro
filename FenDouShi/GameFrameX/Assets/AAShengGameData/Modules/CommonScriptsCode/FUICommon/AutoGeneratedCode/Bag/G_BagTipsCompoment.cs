/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagTipsCompoment : GComponent
    {
        public GImage n118;
        public GImage n119;
        public GLoader Image_quality;
        public GTextField Text_name;
        public GTextField Text_num;
        public GTextField Text_expireTime;
        public GLoader Image_icon;
        public GImage Imgae_fragment;
        public GTextField Text_desc;
        public GList List_reward;
        public GGroup group_content;
        public GTextField Text_limit;
        public GButton Button_blue;
        public GButton Button_yellow;
        public GGroup group_button;
        public GGroup n136;
        public const string URL = "ui://iy58luyakiy43m";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagTipsCompoment";

        public static G_BagTipsCompoment CreateInstance()
        {
            return (G_BagTipsCompoment)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n118 = (GImage)GetChildAt(0);
            n119 = (GImage)GetChildAt(1);
            Image_quality = (GLoader)GetChildAt(2);
            Text_name = (GTextField)GetChildAt(3);
            Text_num = (GTextField)GetChildAt(4);
            Text_expireTime = (GTextField)GetChildAt(5);
            Image_icon = (GLoader)GetChildAt(6);
            Imgae_fragment = (GImage)GetChildAt(7);
            Text_desc = (GTextField)GetChildAt(8);
            List_reward = (GList)GetChildAt(9);
            group_content = (GGroup)GetChildAt(10);
            Text_limit = (GTextField)GetChildAt(11);
            Button_blue = (GButton)GetChildAt(12);
            Button_yellow = (GButton)GetChildAt(13);
            group_button = (GGroup)GetChildAt(14);
            n136 = (GGroup)GetChildAt(15);
        }
    }
}