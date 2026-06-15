using FairyGUI;
using SystemOpen;


public class SystemOpenTaskItemRenderer : BaseItemRenderer
{
    public SystemOpenTaskItemRenderer(GList list) : base(list)
    {
    }
    public SystemOpenTaskItemRenderer()
    {
    }

    public override void Renderer(int index, GObject obj)
    {
        base.Renderer(index, obj);
        var item = obj as G_SystemTaskItem;
        var data = GetData<FuncTaskVo>(index);
        var funcId = (int)tempData;
        obj.data = funcId;
        var cfg = SystemOpenControl.Instance.Model.GetSystemCfgById(funcId);
        var targetValue = SystemOpenControl.Instance.Model.GetTaskValue(funcId, data.Id);
        var maxValue = data.Value;
        var isFinish = targetValue >= maxValue;
        item.Image_slider.fillAmount = targetValue / maxValue;
        item.Text_value.text = Utility.Text.Format("{0}/{1}", targetValue, maxValue);
        item.Text_name.text = data.Name;
        item.group_jump.visible = !isFinish;
        item.group_finish.visible = isFinish;
        item.group_jump.onClick.Add(() =>
        {
            var jump = data.Jump;
            Logger.PrintLog("jumpId" + jump);
        });
    }
}



