/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Arena
{
    public partial class G_ArenaEntranceTotalItem : GComponent
    {
        public G_AreaHenLeftBlock AreaItemHenLeft;
        public G_AreaHenRightBlock AreaItemHenRight;
        public G_ArenaVerBlocRight ArenaVerBlockRight;
        public G_ArenaVerBlockLeft ArenaVerBlockLeft;
        public const string URL = "ui://n8s4flryrxgo2k";
        public const string PACKAGE_NAME = "Arena";
        public const string COMPONENT_NAME = "ArenaEntranceTotalItem";

        public static G_ArenaEntranceTotalItem CreateInstance()
        {
            return (G_ArenaEntranceTotalItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            AreaItemHenLeft = (G_AreaHenLeftBlock)GetChildAt(0);
            AreaItemHenRight = (G_AreaHenRightBlock)GetChildAt(1);
            ArenaVerBlockRight = (G_ArenaVerBlocRight)GetChildAt(2);
            ArenaVerBlockLeft = (G_ArenaVerBlockLeft)GetChildAt(3);
        }
    }
}