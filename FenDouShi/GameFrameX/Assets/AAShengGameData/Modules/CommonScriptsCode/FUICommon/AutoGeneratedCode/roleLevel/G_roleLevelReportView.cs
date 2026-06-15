/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace roleLevel
{
    public partial class G_RoleLevelReportView : GComponent
    {
        public GImage n2;
        public GImage n4;
        public GImage n6;
        public GImage n7;
        public GImage n8;
        public GImage bg;
        public GTextField n13;
        public GTextField playerName;
        public G_selectRidoBtn advertiseBtn;
        public G_selectRidoBtn politicalBtn;
        public G_selectRidoBtn scoldBtn;
        public G_selectRidoBtn vulgarBtn;
        public G_selectRidoBtn fraudBtn;
        public G_selectRidoBtn elseBtn;
        public GTextInput ReportInputText;
        public GTextField n36;
        public GButton reportBtn;
        public GButton closeBtn;
        public const string URL = "ui://oz2qb5cgi1yt4";
        public const string PACKAGE_NAME = "roleLevel";
        public const string COMPONENT_NAME = "RoleLevelReportView";

        public static G_RoleLevelReportView CreateInstance()
        {
            return (G_RoleLevelReportView)UIPackage.CreateObject(PACKAGE_NAME, COMPONENT_NAME);
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n2 = (GImage)GetChildAt(0);
            n4 = (GImage)GetChildAt(1);
            n6 = (GImage)GetChildAt(2);
            n7 = (GImage)GetChildAt(3);
            n8 = (GImage)GetChildAt(4);
            bg = (GImage)GetChildAt(5);
            n13 = (GTextField)GetChildAt(6);
            playerName = (GTextField)GetChildAt(7);
            advertiseBtn = (G_selectRidoBtn)GetChildAt(8);
            politicalBtn = (G_selectRidoBtn)GetChildAt(9);
            scoldBtn = (G_selectRidoBtn)GetChildAt(10);
            vulgarBtn = (G_selectRidoBtn)GetChildAt(11);
            fraudBtn = (G_selectRidoBtn)GetChildAt(12);
            elseBtn = (G_selectRidoBtn)GetChildAt(13);
            ReportInputText = (GTextInput)GetChildAt(14);
            n36 = (GTextField)GetChildAt(15);
            reportBtn = (GButton)GetChildAt(16);
            closeBtn = (GButton)GetChildAt(17);
        }
    }
}