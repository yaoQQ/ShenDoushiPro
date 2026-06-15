/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Tips
{
    public partial class G_ItemGetRewardView : GComponent
    {
        public GButton viewMask;
        public GImage n63;
        public GImage n7;
        public GTextField n8;
        public GImage n10;
        public GImage n11;
        public GImage Image_content;
        public GList list_reward;
        public GImage n41;
        public GImage n42;
        public GImage n43;
        public GImage n44;
        public GImage n46;
        public GImage n47;
        public GImage n48;
        public GImage n49;
        public GImage n50;
        public GImage n52;
        public const string URL = "ui://g2ec8shvixvjo";
        public const string PACKAGE_NAME = "Tips";
        public const string COMPONENT_NAME = "ItemGetRewardView";

        public static G_ItemGetRewardView CreateInstance()
        {
            return (G_ItemGetRewardView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewMask = (GButton)GetChildAt(0);
            n63 = (GImage)GetChildAt(1);
            n7 = (GImage)GetChildAt(2);
            n8 = (GTextField)GetChildAt(3);
            n10 = (GImage)GetChildAt(4);
            n11 = (GImage)GetChildAt(5);
            Image_content = (GImage)GetChildAt(6);
            list_reward = (GList)GetChildAt(7);
            n41 = (GImage)GetChildAt(8);
            n42 = (GImage)GetChildAt(9);
            n43 = (GImage)GetChildAt(10);
            n44 = (GImage)GetChildAt(11);
            n46 = (GImage)GetChildAt(12);
            n47 = (GImage)GetChildAt(13);
            n48 = (GImage)GetChildAt(14);
            n49 = (GImage)GetChildAt(15);
            n50 = (GImage)GetChildAt(16);
            n52 = (GImage)GetChildAt(17);
        }
    }
}