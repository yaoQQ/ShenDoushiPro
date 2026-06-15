using common;
using FairyGUI;
using GM;
using msg.system;
using System.Collections.Generic;
using static GM.GmDefine;
public class GmApiPageView : BaseRender
{
    public new G_GmApiPageView mRoot
    {
        get { return (G_GmApiPageView)base.mRoot; }
    }

    private List<GmCommand> mGmData;

    private string gmText;

    private string tempCmd;

    public override string mPackageName => G_GmApiPageView.PACKAGE_NAME;

    public override string mComponentName => G_GmApiPageView.COMPONENT_NAME;

    private TableView<GmTabItem> mTableView;

    protected override void onCreate()
    {
        mTableView = new TableView<GmTabItem>(mRoot.List_content);
        var tabDats = Refs.gmData.GetGmCommands();
        var datas = new List<GMTabItemData>();
        foreach (var item in tabDats)
        {
            var clm = new GMTabItemData();
            clm.name = item.Description;
            datas.Add(clm);
        }
        mTableView.setDatas(datas);
        mTableView.setClickCallBack(OnClickTab);

        mRoot.Text_input.onChanged.Add((context) =>
        {
            gmText = mRoot.Text_input.text;
        });

        mRoot.Button_click.onClick.Add(SendMsg);
        mRoot.Button_click.title = "执行";
    }

    private void SendMsg(EventContext eventContext)
    {
        if (string.IsNullOrEmpty(gmText))
        {
            GmModule.OSendGm(tempCmd);
            return;
        }
        GmModule.OSendGm(Utility.Text.Format("{0} {1}", tempCmd, gmText));
    }

    private void OnClickTab(object obj, int index)
    {
        var tabDats = Refs.gmData.GetGmCommands();
        OnClickTabLogic(tabDats[index]);
    }

    private void ServerItemRenderer(int index, GObject obj)
    {
        var data = mGmData[index];
        var item = obj as G_CommonSelectBtn;
        item.title.text = data.Description;
        item.data = data.Command;
        item.data = data;
    }

    void OnClickTabLogic(GmCommand itemData)
    {
        mRoot.Text_input.text = itemData.Syntax;
        mRoot.Text_gm.text = itemData.Description;
        tempCmd = itemData.Command;
    }
}

