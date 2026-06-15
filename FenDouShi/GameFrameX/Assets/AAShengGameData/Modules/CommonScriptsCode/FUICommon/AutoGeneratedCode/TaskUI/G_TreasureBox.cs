/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TreasureBox : GButton
    {
        public GImage Complete;
        public GImage Open;
        public GImage Lock;
        public GTextField n25;
        public const string URL = "ui://a26huicljkkf19";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TreasureBox";

        public static G_TreasureBox CreateInstance()
        {
            return (G_TreasureBox)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Complete = (GImage)GetChildAt(0);
            Open = (GImage)GetChildAt(1);
            Lock = (GImage)GetChildAt(2);
            n25 = (GTextField)GetChildAt(3);
        }
    }
}