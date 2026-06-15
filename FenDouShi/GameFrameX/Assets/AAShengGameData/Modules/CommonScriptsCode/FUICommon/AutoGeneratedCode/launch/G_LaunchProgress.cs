/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace launch
{
    public partial class G_LaunchProgress : GProgressBar
    {
        public GImage n0;
        public GImage bar;
        public GImage n8;
        public GImage n9;
        public const string URL = "ui://ynb47g4jpyg64u";
        public const string PACKAGE_NAME = "launch";
        public const string COMPONENT_NAME = "LaunchProgress";

        public static G_LaunchProgress CreateInstance()
        {
            return (G_LaunchProgress)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GImage)GetChildAt(0);
            bar = (GImage)GetChildAt(1);
            n8 = (GImage)GetChildAt(2);
            n9 = (GImage)GetChildAt(3);
        }
    }
}