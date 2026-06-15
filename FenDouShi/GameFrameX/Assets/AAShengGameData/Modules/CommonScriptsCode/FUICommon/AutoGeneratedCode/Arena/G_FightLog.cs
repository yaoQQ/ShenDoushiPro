/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_FightLog : GComponent
    {
        public GImage n60;
        public GImage n61;
        public GTextField fightingLogInfo;
        public const string URL = "ui://n8s4flrye0dt2c";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "FightLog";

        public static G_FightLog CreateInstance()
        {
            return (G_FightLog)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n60 = (GImage)GetChildAt(0);
            n61 = (GImage)GetChildAt(1);
            fightingLogInfo = (GTextField)GetChildAt(2);
        }
    }
}