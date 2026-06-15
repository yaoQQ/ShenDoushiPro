/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace TaskUI
{
    public partial class G_TaskTipsView : GComponent
    {
        public GGraph LeftBottom;
        public GGraph CreatePosition;
        public const string URL = "ui://a26huicljkkf1j";
        public const string PACKAGE_NAME = "TaskUI";
        public const string COMPONENT_NAME = "TaskTipsView";

        public static G_TaskTipsView CreateInstance()
        {
            return (G_TaskTipsView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            LeftBottom = (GGraph)GetChildAt(0);
            CreatePosition = (GGraph)GetChildAt(1);
        }
    }
}