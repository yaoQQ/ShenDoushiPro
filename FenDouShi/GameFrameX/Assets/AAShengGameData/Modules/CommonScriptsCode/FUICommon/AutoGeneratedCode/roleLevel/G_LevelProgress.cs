/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_LevelProgress : GProgressBar
    {
        public GImage bg;
        public GImage bar;
        public GTextField title;
        public const string URL = "ui://oz2qb5cgppy63c";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "LevelProgress";

        public static G_LevelProgress CreateInstance()
        {
            return (G_LevelProgress)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg = (GImage)GetChildAt(0);
            bar = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}