/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Bag
{
    public partial class G_BagSelectionView : GComponent
    {
        public GButton viewMask;
        public GImage n141;
        public G_BagSelectionGroup bagGroup;
        public GGroup n152;
        public const string URL = "ui://iy58luyawdae42";
        public const string PACKAGE_NAME = "Bag";
        public const string COMPONENT_NAME = "BagSelectionView";

        public static G_BagSelectionView CreateInstance()
        {
            return (G_BagSelectionView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            viewMask = (GButton)GetChildAt(0);
            n141 = (GImage)GetChildAt(1);
            bagGroup = (G_BagSelectionGroup)GetChildAt(2);
            n152 = (GGroup)GetChildAt(3);
        }
    }
}