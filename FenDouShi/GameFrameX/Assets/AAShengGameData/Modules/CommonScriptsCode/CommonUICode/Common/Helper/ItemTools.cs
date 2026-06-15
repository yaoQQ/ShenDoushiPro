/// <summary>
/// 道具工具类
/// </summary>

public class ItemTools
{
    /// <summary>
    /// 根据道具ID获取道具名称
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public static string GetItemName(int itemId)
    {
        var itemCfg = ConfigMgr.Instance.GetConfigVoById<ItemVo>(itemId);
        if (itemCfg != null)
        {
            return itemCfg.Name;
        }
        return string.Empty;
    }

    //获取道具图标
    public static string GetItemIcon(int itemId)
    {
        var itemCfg = ConfigMgr.Instance.GetConfigVoById<ItemVo>(itemId);
        if (itemCfg != null)
        {
            return UIHelper.GetIconUrl(itemCfg.Icon);
        }
        return "";
    }
}