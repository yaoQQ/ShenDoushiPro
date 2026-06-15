/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TaskTips : GComponent
    {
        public GImage n116;
        public GImage n117;
        public GImage n118;
        public GImage n121;
        public GTextField title;
        public GTextField content;
        public const string URL = "ui://a26huicljkkf1b";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TaskTips";

        public static G_TaskTips CreateInstance()
        {
            return (G_TaskTips)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n116 = (GImage)GetChildAt(0);
            n117 = (GImage)GetChildAt(1);
            n118 = (GImage)GetChildAt(2);
            n121 = (GImage)GetChildAt(3);
            title = (GTextField)GetChildAt(4);
            content = (GTextField)GetChildAt(5);
        }
    }
}