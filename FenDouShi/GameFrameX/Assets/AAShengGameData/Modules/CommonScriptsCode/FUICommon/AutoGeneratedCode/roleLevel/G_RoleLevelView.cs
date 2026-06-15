/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelView : GComponent
    {
        public GImage n3;
        public GLoader n4;
        public GLoader n8;
        public GImage n9;
        public GImage n10;
        public GImage n11;
        public GImage n13;
        public GImage n14;
        public GImage n15;
        public GImage n16;
        public GImage n17;
        public GImage n12;
        public GGroup bg;
        public GImage n20;
        public GImage n21;
        public GTextField level;
        public GImage leftBg;
        public GImage n34;
        public GImage n69;
        public GTextField n36;
        public GList rewardList;
        public GGroup reward;
        public GTextField n30;
        public GImage n31;
        public GImage n71;
        public GList funcList;
        public GGroup func;
        public GGroup content;
        public GGroup n83;
        public const string URL = "ui://oz2qb5cgzbyuz";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelView";

        public static G_RoleLevelView CreateInstance()
        {
            return (G_RoleLevelView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n3 = (GImage)GetChildAt(0);
            n4 = (GLoader)GetChildAt(1);
            n8 = (GLoader)GetChildAt(2);
            n9 = (GImage)GetChildAt(3);
            n10 = (GImage)GetChildAt(4);
            n11 = (GImage)GetChildAt(5);
            n13 = (GImage)GetChildAt(6);
            n14 = (GImage)GetChildAt(7);
            n15 = (GImage)GetChildAt(8);
            n16 = (GImage)GetChildAt(9);
            n17 = (GImage)GetChildAt(10);
            n12 = (GImage)GetChildAt(11);
            bg = (GGroup)GetChildAt(12);
            n20 = (GImage)GetChildAt(13);
            n21 = (GImage)GetChildAt(14);
            level = (GTextField)GetChildAt(15);
            leftBg = (GImage)GetChildAt(16);
            n34 = (GImage)GetChildAt(17);
            n69 = (GImage)GetChildAt(18);
            n36 = (GTextField)GetChildAt(19);
            rewardList = (GList)GetChildAt(20);
            reward = (GGroup)GetChildAt(21);
            n30 = (GTextField)GetChildAt(22);
            n31 = (GImage)GetChildAt(23);
            n71 = (GImage)GetChildAt(24);
            funcList = (GList)GetChildAt(25);
            func = (GGroup)GetChildAt(26);
            content = (GGroup)GetChildAt(27);
            n83 = (GGroup)GetChildAt(28);
        }
    }
}