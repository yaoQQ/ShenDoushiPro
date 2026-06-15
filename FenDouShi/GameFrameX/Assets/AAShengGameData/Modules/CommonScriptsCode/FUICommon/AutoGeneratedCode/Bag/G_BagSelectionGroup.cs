/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagSelectionGroup : GComponent
    {
        public GList List_select;
        public G_BagSelectionItem Button_all;
        public GGroup BagSelectionGroup;
        public const string URL = "ui://iy58luyawdae44";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagSelectionGroup";

        public static G_BagSelectionGroup CreateInstance()
        {
            return (G_BagSelectionGroup)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            List_select = (GList)GetChildAt(0);
            Button_all = (G_BagSelectionItem)GetChildAt(1);
            BagSelectionGroup = (GGroup)GetChildAt(2);
        }
    }
}