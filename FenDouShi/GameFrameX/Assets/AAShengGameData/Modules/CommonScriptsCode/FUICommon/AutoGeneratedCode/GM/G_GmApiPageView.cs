/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace GM
{
    public partial class G_GmApiPageView : GComponent
    {
        public GList List_content;
        public GLoader bg;
        public GTextField Text_gm;
        public GButton Button_click;
        public GTextInput Text_input;
        public const string URL = "ui://p65pul3qqa5m6";
        public const string PACKAGE_NAME = "GM";
        public const string COMPONENT_NAME = "GmApiPageView";

        public static G_GmApiPageView CreateInstance()
        {
            return (G_GmApiPageView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            List_content = (GList)GetChildAt(0);
            bg = (GLoader)GetChildAt(1);
            Text_gm = (GTextField)GetChildAt(2);
            Button_click = (GButton)GetChildAt(3);
            Text_input = (GTextInput)GetChildAt(4);
        }
    }
}