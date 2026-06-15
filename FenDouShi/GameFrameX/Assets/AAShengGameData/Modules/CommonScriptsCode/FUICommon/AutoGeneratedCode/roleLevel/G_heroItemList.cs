/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_heroItemList : GButton
    {
        public Controller button;
        public GImage n120;
        public GImage n46;
        public GTextField title;
        public const string URL = "ui://oz2qb5cgkmtsp";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "heroItemList";

        public static G_heroItemList CreateInstance()
        {
            return (G_heroItemList)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n120 = (GImage)GetChildAt(0);
            n46 = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}