/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ShengBag
{
    public partial class G_FightButton : GButton
    {
        public GMovieClip n2;
        public GImage n3;
        public const string URL = "ui://9rz411j4ragy6";
        public const string PACKAGE_NAME = "ShengBag";
        public const string COMPONENT_NAME = "FightButton";

        public static G_FightButton CreateInstance()
        {
            return (G_FightButton)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GMovieClip)GetChildAt(0);
            n3 = (GImage)GetChildAt(1);
        }
    }
}