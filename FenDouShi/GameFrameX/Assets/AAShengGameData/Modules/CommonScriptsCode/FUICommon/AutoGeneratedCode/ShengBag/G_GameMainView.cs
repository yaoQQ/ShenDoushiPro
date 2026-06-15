/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace ShengBag
{
    public partial class G_GameMainView : GComponent
    {
        public Controller button;
        public GButton roleButton;
        public G_BagButton BagBtn;
        public G_FightButton fightBtn;
        public GButton heroButton;
        public GButton bagButton;
        public GGroup n2;
        public GButton returnBtn;
        public GTextField title;
        public GButton HookBtn;
        public const string URL = "ui://9rz411j4n7cz4";
        public const string PACKAGE_NAME = "ShengBag";
        public const string COMPONENT_NAME = "GameMainView";

        public static G_GameMainView CreateInstance()
        {
            return (G_GameMainView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            roleButton = (GButton)GetChildAt(0);
            BagBtn = (G_BagButton)GetChildAt(1);
            fightBtn = (G_FightButton)GetChildAt(2);
            heroButton = (GButton)GetChildAt(3);
            bagButton = (GButton)GetChildAt(4);
            n2 = (GGroup)GetChildAt(5);
            returnBtn = (GButton)GetChildAt(6);
            title = (GTextField)GetChildAt(7);
            HookBtn = (GButton)GetChildAt(8);
        }
    }
}