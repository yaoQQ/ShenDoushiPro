/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Rank
{
    public partial class G_RankHeadItem : GComponent
    {
        public GLoader Image_frame;
        public GLoader Image_head;
        public GImage n13;
        public GTextField Text_level;
        public GGroup group_level;
        public const string URL = "ui://fxqdojw7kc3p23";
        public const string PACKAGE_NAME = "Rank";
        public const string COMPONENT_NAME = "RankHeadItem";

        public static G_RankHeadItem CreateInstance()
        {
            return (G_RankHeadItem)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            Image_frame = (GLoader)GetChildAt(0);
            Image_head = (GLoader)GetChildAt(1);
            n13 = (GImage)GetChildAt(2);
            Text_level = (GTextField)GetChildAt(3);
            group_level = (GGroup)GetChildAt(4);
        }
    }
}