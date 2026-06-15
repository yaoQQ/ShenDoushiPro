/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace vip
{
    public partial class G_RewardBgCompoent : GComponent
    {
        public GImage n8;
        public GImage n61;
        public GGroup bg;
        public GImage n4;
        public GImage n5;
        public GImage n32;
        public GGroup titleBg;
        public const string URL = "ui://yizyzd76mv0c36";
        public const string PACKAGE_NAME = "vip";
        public const string COMPONENT_NAME = "RewardBgCompoent";

        public static G_RewardBgCompoent CreateInstance()
        {
            return (G_RewardBgCompoent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n8 = (GImage)GetChildAt(0);
            n61 = (GImage)GetChildAt(1);
            bg = (GGroup)GetChildAt(2);
            n4 = (GImage)GetChildAt(3);
            n5 = (GImage)GetChildAt(4);
            n32 = (GImage)GetChildAt(5);
            titleBg = (GGroup)GetChildAt(6);
        }
    }
}