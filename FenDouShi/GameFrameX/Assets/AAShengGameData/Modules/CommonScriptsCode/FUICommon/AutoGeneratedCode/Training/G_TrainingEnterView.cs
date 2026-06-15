/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Training
{
    public partial class G_TrainingEnterView : GComponent
    {
        public GTextField title;
        public GImage n53;
        public G_YaDianNaDungeon yaDianNaInstace;
        public GList n59;
        public GButton tipsBtn;
        public GButton closeBtn;
        public GGroup n63;
        public const string URL = "ui://neqo767femizn";
        public const string PACKAGE_NAME = "Training";
        public const string COMPONENT_NAME = "TrainingEnterView";

        public static G_TrainingEnterView CreateInstance()
        {
            return (G_TrainingEnterView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            title = (GTextField)GetChildAt(0);
            n53 = (GImage)GetChildAt(1);
            yaDianNaInstace = (G_YaDianNaDungeon)GetChildAt(2);
            n59 = (GList)GetChildAt(3);
            tipsBtn = (GButton)GetChildAt(4);
            closeBtn = (GButton)GetChildAt(5);
            n63 = (GGroup)GetChildAt(6);
        }
    }
}