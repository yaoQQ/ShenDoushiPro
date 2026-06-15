/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_roleInfoBtnContent : GComponent
    {
        public G_adviceBtn adviceBtn;
        public G_setBtn setBtn;
        public G_roleStyleBtn roleStyleBtn;
        public const string URL = "ui://oz2qb5cgd558w";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "roleInfoBtnContent";

        public static G_roleInfoBtnContent CreateInstance()
        {
            return (G_roleInfoBtnContent)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            adviceBtn = (G_adviceBtn)GetChildAt(0);
            setBtn = (G_setBtn)GetChildAt(1);
            roleStyleBtn = (G_roleStyleBtn)GetChildAt(2);
        }
    }
}