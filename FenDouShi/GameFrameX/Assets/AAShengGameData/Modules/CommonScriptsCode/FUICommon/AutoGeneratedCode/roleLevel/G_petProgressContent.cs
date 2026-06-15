/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_petProgressContent : GComponent
    {
        public GImage n25;
        public GTextField n26;
        public GImage n33;
        public GImage n54;
        public GTextField n35;
        public GTextField n36;
        public GImage n28;
        public GTextField n30;
        public GTextField n31;
        public GImage n53;
        public GImage n38;
        public GImage n51;
        public GTextField n40;
        public GTextField n41;
        public GImage n43;
        public GImage n52;
        public GTextField n45;
        public GTextField n46;
        public const string URL = "ui://oz2qb5cgd558v";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "petProgressContent";

        public static G_petProgressContent CreateInstance()
        {
            return (G_petProgressContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n25 = (GImage)GetChildAt(0);
            n26 = (GTextField)GetChildAt(1);
            n33 = (GImage)GetChildAt(2);
            n54 = (GImage)GetChildAt(3);
            n35 = (GTextField)GetChildAt(4);
            n36 = (GTextField)GetChildAt(5);
            n28 = (GImage)GetChildAt(6);
            n30 = (GTextField)GetChildAt(7);
            n31 = (GTextField)GetChildAt(8);
            n53 = (GImage)GetChildAt(9);
            n38 = (GImage)GetChildAt(10);
            n51 = (GImage)GetChildAt(11);
            n40 = (GTextField)GetChildAt(12);
            n41 = (GTextField)GetChildAt(13);
            n43 = (GImage)GetChildAt(14);
            n52 = (GImage)GetChildAt(15);
            n45 = (GTextField)GetChildAt(16);
            n46 = (GTextField)GetChildAt(17);
        }
    }
}