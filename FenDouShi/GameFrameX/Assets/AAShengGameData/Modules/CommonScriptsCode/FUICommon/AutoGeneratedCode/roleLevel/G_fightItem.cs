/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_fightItem : GComponent
    {
        public GImage n4;
        public GImage n5;
        public GTextField n6;
        public GImage n7;
        public GImage n10;
        public GImage n8;
        public GTextField n9;
        public GImage n11;
        public GTextField n12;
        public G_detailBtn n13;
        public const string URL = "ui://oz2qb5cgegpfa";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "fightItem";

        public static G_fightItem CreateInstance()
        {
            return (G_fightItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GImage)GetChildAt(0);
            n5 = (GImage)GetChildAt(1);
            n6 = (GTextField)GetChildAt(2);
            n7 = (GImage)GetChildAt(3);
            n10 = (GImage)GetChildAt(4);
            n8 = (GImage)GetChildAt(5);
            n9 = (GTextField)GetChildAt(6);
            n11 = (GImage)GetChildAt(7);
            n12 = (GTextField)GetChildAt(8);
            n13 = (G_detailBtn)GetChildAt(9);
        }
    }
}