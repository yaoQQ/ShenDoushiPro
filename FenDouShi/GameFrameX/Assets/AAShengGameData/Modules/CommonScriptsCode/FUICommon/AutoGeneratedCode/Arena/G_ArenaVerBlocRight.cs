/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaVerBlocRight : GComponent
    {
        public G_AreanVerItem AreanVerItem;
        public G_AreanVerItem2 AreanVerItem2;
        public GLoader rightNPCLoad;
        public const string URL = "ui://n8s4flrye0dt2h";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaVerBlocRight";

        public static G_ArenaVerBlocRight CreateInstance()
        {
            return (G_ArenaVerBlocRight)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            AreanVerItem = (G_AreanVerItem)GetChildAt(0);
            AreanVerItem2 = (G_AreanVerItem2)GetChildAt(1);
            rightNPCLoad = (GLoader)GetChildAt(2);
        }
    }
}