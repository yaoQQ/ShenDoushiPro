/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_AreaHenRightBlock : GComponent
    {
        public G_ArenaHoritem3 Areaitem3;
        public G_ArenaHoritem4 Areaitem4;
        public GLoader rightNPCLoad;
        public const string URL = "ui://n8s4flrye0dt2e";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "AreaHenRightBlock";

        public static G_AreaHenRightBlock CreateInstance()
        {
            return (G_AreaHenRightBlock)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Areaitem3 = (G_ArenaHoritem3)GetChildAt(0);
            Areaitem4 = (G_ArenaHoritem4)GetChildAt(1);
            rightNPCLoad = (GLoader)GetChildAt(2);
        }
    }
}