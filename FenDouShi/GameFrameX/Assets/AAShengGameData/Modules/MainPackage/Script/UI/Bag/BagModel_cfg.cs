

using System.Collections.Generic;

public partial class BagModel
{
    private Dictionary<int, ItemVo> itemCfg;

    private Dictionary<int, JumpVo> jumpCfg;


    private void InitCfg()
    {
        if (itemCfg != null)
        {
            return;
        }
        itemCfg = ConfigMgr.Instance.GetConfig<ItemVo>();
    }

    public ItemVo GetItemCfgById(int itemId)
    {
        if (itemCfg == null)
        {
            InitCfg();
        }
        if (itemCfg.TryGetValue(itemId, out var itemExcel))
        {
            return itemExcel;
        }
        else
        {
            Logger.PrintLog($"GetItemCfgById itemId={itemId} not found!");
            return null;
        }
    }

    /// <summary>
    /// renderType
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    private int GetItemBagType(int itemId)
    {
        var cfg = GetItemCfgById(itemId);
        if (cfg == null)
        {
            return 0;
        }
        return cfg.BagType;
    }

    public string GetItemIconUrlById(int itemId)
    {
        var cfg = GetItemCfgById(itemId);
        if (cfg == null)
        {
            return string.Empty;
        }
        return UIHelper.GetIconUrl(cfg.Icon);
    }


    public JumpVo GetJumpCfgById(int itemId)
    {
        jumpCfg ??= ConfigMgr.Instance.GetConfig<JumpVo>();
        if (jumpCfg.TryGetValue(itemId, out var itemExcel))
        {
            return itemExcel;
        }
        else
        {
            Logger.PrintLog($"GetJumpCfgById itemId={itemId} not found!");
            return null;
        }
    }
}

