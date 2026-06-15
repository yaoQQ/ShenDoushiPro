/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_LevelUpFunctionOpenListRender : GComponent
    {
        public GImage n51;
        public GTextField funcName;
        public GTextField contdion;
        public GLoader funcIcon;
        public const string URL = "ui://oz2qb5cgzbyu22";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "LevelUpFunctionOpenListRender";

        public static G_LevelUpFunctionOpenListRender CreateInstance()
        {
            return (G_LevelUpFunctionOpenListRender)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n51 = (GImage)GetChildAt(0);
            funcName = (GTextField)GetChildAt(1);
            contdion = (GTextField)GetChildAt(2);
            funcIcon = (GLoader)GetChildAt(3);
        }
    }
}