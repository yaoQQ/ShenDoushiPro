/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_HeroIcon : GComponent
    {
        public Controller c1;
        public GImage n81;
        public GImage n82;
        public GImage n94;
        public GImage n93;
        public GImage n83;
        public GImage n84;
        public GImage startContent;
        public G_startContent starProgress;
        public GTextField levelText;
        public GImage campIcon;
        public const string URL = "ui://oz2qb5cgkmts2v";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "HeroIcon";

        public static G_HeroIcon CreateInstance()
        {
            return (G_HeroIcon)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            n81 = (GImage)GetChildAt(0);
            n82 = (GImage)GetChildAt(1);
            n94 = (GImage)GetChildAt(2);
            n93 = (GImage)GetChildAt(3);
            n83 = (GImage)GetChildAt(4);
            n84 = (GImage)GetChildAt(5);
            startContent = (GImage)GetChildAt(6);
            starProgress = (G_startContent)GetChildAt(7);
            levelText = (GTextField)GetChildAt(8);
            campIcon = (GImage)GetChildAt(9);
        }
    }
}