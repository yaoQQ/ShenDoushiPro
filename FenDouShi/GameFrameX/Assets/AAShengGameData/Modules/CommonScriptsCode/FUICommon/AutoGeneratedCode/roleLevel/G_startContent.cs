/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_startContent : GProgressBar
    {
        public Controller c1;
        public GImage start1;
        public GImage start2;
        public GImage start3;
        public GImage start4;
        public GImage start5;
        public const string URL = "ui://oz2qb5cgkmts2w";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "startContent";

        public static G_startContent CreateInstance()
        {
            return (G_startContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            c1 = GetControllerAt(0);
            start1 = (GImage)GetChildAt(0);
            start2 = (GImage)GetChildAt(1);
            start3 = (GImage)GetChildAt(2);
            start4 = (GImage)GetChildAt(3);
            start5 = (GImage)GetChildAt(4);
        }
    }
}