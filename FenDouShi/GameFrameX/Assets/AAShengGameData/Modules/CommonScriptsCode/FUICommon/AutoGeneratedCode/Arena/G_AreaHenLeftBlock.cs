/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_AreaHenLeftBlock : GComponent
    {
        public G_ArenaHorItem1 Areaitem1;
        public G_ArenaHoritem2 Areaitem2;
        public GLoader leftNPCLoad;
        public const string URL = "ui://n8s4flrye0dt2d";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "AreaHenLeftBlock";

        public static G_AreaHenLeftBlock CreateInstance()
        {
            return (G_AreaHenLeftBlock)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Areaitem1 = (G_ArenaHorItem1)GetChildAt(0);
            Areaitem2 = (G_ArenaHoritem2)GetChildAt(1);
            leftNPCLoad = (GLoader)GetChildAt(2);
        }
    }
}