/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace common
{
    public partial class G_CommonGoodItem2 : GComponent
    {
        public G_CommonGoodItem commonGoodItem;
        public GTextField Text_name;
        public GGroup Frame;
        public const string URL = "ui://y4b7yuunixvj2ica";
        public const string PACKAGE_NAME = "common";
        public const string COMPONENT_NAME = "CommonGoodItem2";

        public static G_CommonGoodItem2 CreateInstance()
        {
            return (G_CommonGoodItem2)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            commonGoodItem = (G_CommonGoodItem)GetChildAt(0);
            Text_name = (GTextField)GetChildAt(1);
            Frame = (GGroup)GetChildAt(2);
        }
    }
}