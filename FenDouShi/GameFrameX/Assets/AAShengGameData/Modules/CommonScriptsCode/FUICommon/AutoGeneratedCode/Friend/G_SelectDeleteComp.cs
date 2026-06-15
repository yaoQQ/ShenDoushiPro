/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Friend
{
    public partial class G_SelectDeleteComp : GButton
    {
        public Controller button;
        public GTextField n180;
        public GImage n181;
        public GImage select;
        public const string URL = "ui://jyya7ryuvliv4q";
        public const string PACKAGE_NAME = "Friend";
        public const string COMPONENT_NAME = "SelectDeleteComp";

        public static G_SelectDeleteComp CreateInstance()
        {
            return (G_SelectDeleteComp)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n180 = (GTextField)GetChildAt(0);
            n181 = (GImage)GetChildAt(1);
            select = (GImage)GetChildAt(2);
        }
    }
}