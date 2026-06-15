using FairyGUI;
using SystemOpen;


public class SystemOpenTabItemRenderer : BaseItemRenderer
{
    public SystemOpenTabItemRenderer(GList list) : base(list)
    {
    }
    public SystemOpenTabItemRenderer()
    {
    }

    public override void Renderer(int index, GObject obj)
    {
        base.Renderer(index, obj);
        var item = obj as G_SystemTabItem;
        var data = GetData<FuncVo>(index);
        var funcId = data.Id;
        obj.data = data;
        var cfg = SystemOpenControl.Instance.Model.GetSystemCfgById(funcId);
        if (cfg != null)
        {
            item.Image_icon.url = UIHelper.GetFguiUrl(G_SystemOpenView.PACKAGE_NAME, cfg.Icon);
            var isOpen = SystemOpenControl.Instance.Model.GetIsSystemOpen(funcId);
            var isGetReward = SystemOpenControl.Instance.Model.GetIsGetSystemOpenReward(funcId);
            item.Text_name.text = cfg.Name;
            item.Image_finish.visible = isGetReward;
            item.Text_limit.text = SystemOpenControl.Instance.Model.GetSystemOpenTipDes(funcId);
            item.Text_limit.visible = !isOpen;
            var isRed = SystemOpenControl.Instance.Model.GetIsHaveReward(funcId);
            item.redPoint.visible = isRed;
        }
    }
}



