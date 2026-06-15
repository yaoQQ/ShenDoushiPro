/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_roleStyleBtn : GButton
    {
        public GImage n93;
        public GImage n94;
        public GTextField n95;
        public const string URL = "ui://oz2qb5cgppy63q";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "roleStyleBtn";

        public static G_roleStyleBtn CreateInstance()
        {
            return (G_roleStyleBtn)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n93 = (GImage)GetChildAt(0);
            n94 = (GImage)GetChildAt(1);
            n95 = (GTextField)GetChildAt(2);
        }
    }
}