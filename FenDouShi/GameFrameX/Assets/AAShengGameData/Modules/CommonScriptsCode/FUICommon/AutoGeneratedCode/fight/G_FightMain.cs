/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace fight
{
    public partial class G_FightMain : GComponent
    {
        public GButton returnBtn;
        public GTextField title;
        public const string URL = "ui://hssxzixiragy0";
        public const string PACKAGE_NAME = "fight";
        public const string COMPONENT_NAME = "FightMain";

        public static G_FightMain CreateInstance()
        {
            return (G_FightMain)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            returnBtn = (GButton)GetChildAt(0);
            title = (GTextField)GetChildAt(1);
        }
    }
}