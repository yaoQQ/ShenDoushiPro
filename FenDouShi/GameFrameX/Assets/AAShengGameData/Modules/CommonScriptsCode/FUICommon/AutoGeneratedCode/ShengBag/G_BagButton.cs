/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ShengBag
{
    public partial class G_BagButton : GButton
    {
        public GImage n0;
        public GMovieClip n1;
        public const string URL = "ui://9rz411j4n7cz5";
        public const string PACKAGE_NAME = "ShengBag";
        public const string COMPONENT_NAME = "BagButton";

        public static G_BagButton CreateInstance()
        {
            return (G_BagButton)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GImage)GetChildAt(0);
            n1 = (GMovieClip)GetChildAt(1);
        }
    }
}