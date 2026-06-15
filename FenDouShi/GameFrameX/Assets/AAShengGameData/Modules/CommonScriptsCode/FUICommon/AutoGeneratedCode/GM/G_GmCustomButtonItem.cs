/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GmCustomButtonItem : GComponent
    {
        public GGraph bg2;
        public GButton Button_click;
        public GLoader bg;
        public GTextInput Text_input;
        public const string URL = "ui://p65pul3qw0vob";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GmCustomButtonItem";

        public static G_GmCustomButtonItem CreateInstance()
        {
            return (G_GmCustomButtonItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            bg2 = (GGraph)GetChildAt(0);
            Button_click = (GButton)GetChildAt(1);
            bg = (GLoader)GetChildAt(2);
            Text_input = (GTextInput)GetChildAt(3);
        }
    }
}