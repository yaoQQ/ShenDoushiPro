
using System.Collections.Generic;
using System.Linq;
using msg.common;

public class CommonItemData : BaseItemData
{
    public CommonItemData()
    {
    }
    public CommonItemData(ResourceInfo info)
    {
        ItemId = info.Id;
        ServerInfo = info;
    }

    public CommonItemData(int itemId)
    {
        this.ItemId = itemId;
    }

    public CommonItemData(int itemId, int count)
    {
        this.ItemId = itemId;
        this.ClientShowCount = count;
    }

    /// <summary>
    /// 道具来源
    /// </summary>
    public eItemSource mItemSource { get; set; }

    /// <summary>
    /// 是否已领取
    /// </summary>
    public bool GetIsDraw { get; set; }

    /// <summary>
    /// 空格子
    /// </summary>
    public bool IsEmptyItem { get; set; }

    /// <summary>
    /// 是否显示道具数量
    /// </summary>
    public bool IsShowNum { get; set; } = true;

    /// <summary>
    /// 道具点击
    /// </summary>
    public bool IsEnableClick { get; set; } = true;

    public void SetServerInfo(ResourceInfo info)
    {
        ServerInfo = info;
        if (info.Id != ItemId)
        {
            ItemCfg = null;
        }
    }
    public void SetItemId(int itemId)
    {
        this.ItemId = itemId;
    }

    public ItemVo GetCfg()
    {
        if (ItemCfg != null && ItemCfg.Id == ItemId)
        {
            return ItemCfg;
        }
        ItemCfg = BagControl.Instance.Model.GetItemCfgById(ItemId);
        return ItemCfg;
    }
    public int ItemId { get; private set; }

    public int UniqueId => ServerInfo?.uniqueId ?? 0;

    public string Name => GetCfg()?.Name ?? string.Empty;

    public string NameColorStr => Name;

    public int Quality => GetCfg()?.Quality ?? 0;

    public string IconRes => GetCfg()?.Icon ?? string.Empty;
    public string IconUrl => UIHelper.GetIconUrl(IconRes);

    public string Desc => GetCfg()?.Describe ?? string.Empty;

    public long ClientShowCount { get; private set; }

    public bool ClientShowRedPoint { get; set; }

    public bool ClientIsLock { get; set; }

    /// <summary>
    /// 红点 ERedPointType
    /// </summary>
    public int RedPointType { get; } = -1;

    /// <summary>
    /// todo接入红点系统
    /// </summary>
    public bool GetIsShowRedPoint
    {
        get
        {
            if (ClientShowRedPoint)
            {
                return true;
            }
            return false;
        }
    }

    public long Count => ServerInfo?.Num ?? ClientShowCount;

    /// <summary>
    /// 背包里实时的数量
    /// </summary>
    public long CountImt => BagControl.Instance.Model.GetItemCountByItemId(ItemId);

    public int GetBagType => GetCfg()?.BagType ?? 0;

    public new int GetType => GetCfg()?.Type ?? 0;

    public int GetSubType => GetCfg()?.SubType ?? 0;

    /// <summary>
    /// 到期时间
    /// </summary>
    public long GetExpireTime => ServerInfo?.Expire ?? 0;

    public bool GetIsOutOfDate
    {
        get
        {
            if (GetExpireTime <= 0) return false;
            var curTime = TimeManager.GetServerUnixTime();
            return curTime > GetExpireTime;
        }
    }

    public string GetItemRemainTime
    {
        get
        {
            if (GetExpireTime <= 0 || GetIsOutOfDate)
            {
                return string.Empty;
            }
            var curTime = TimeManager.GetServerUnixTime();
            return DateFormatUtil.FormatLeftTime2(GetExpireTime - curTime);
        }
    }

    public bool GetIsLock
    {
        get
        {
            var playerLevelLimit = GetCfg()?.PlayerLevelLimit ?? 0;
            var playerLevel = RoleControl.Instance.Model.getRoleInfo()?.Level ?? 0;
            if (playerLevel < playerLevelLimit && playerLevelLimit > 0)
            {
                return true;
            }
            var openDay = TimeManager.GetOpenServerDay();
            if (openDay <= 0)
            {
                return false;
            }
            var openDayLimit = GetCfg()?.OpenDayLimit ?? 0;
            if (openDayLimit > 0 && openDay < openDayLimit)
            {
                return true;
            }
            var timeLimit = GetCfg()?.TimeLimitLink;
            if (timeLimit is { Count: > 0 })
            {
                var curTime = TimeManager.GetServerUnixTime();
                var isCanUse = false;
                foreach (var time in timeLimit)
                {
                    var startTime = DateFormatUtil.GetSecondsByDateStr(time.StartTime);
                    var endTime = DateFormatUtil.GetSecondsByDateStr(time.StartTime);
                    if (curTime < startTime || curTime > endTime) continue;
                    isCanUse = true;
                    break;
                }
                return isCanUse == false;
            }
            return false;
        }
    }

    /// <summary>
    /// 锁定文本
    /// </summary>
    public string GetLockStr
    {
        get
        {
            var playerLevelLimit = GetCfg()?.PlayerLevelLimit ?? 0;
            var playerLevel = RoleControl.Instance.Model.getRoleInfo()?.Level ?? 0;
            var limitStr = string.Empty;
            if (playerLevelLimit > 0 && playerLevel < playerLevelLimit)
            {
                limitStr = Utility.Text.Format("玩家{0}级", playerLevelLimit);
            }
            var openDayLimit = GetCfg()?.OpenDayLimit ?? 0;
            var openDay = TimeManager.GetOpenServerDay();
            if (openDayLimit > 0 && openDay < openDayLimit)
            {
                var levelStr = Utility.Text.Format("开服{0}天", playerLevelLimit);
                limitStr = !string.IsNullOrEmpty(limitStr) ? Utility.Text.Format("{0},{1}", limitStr, levelStr) : levelStr;
            }
            var timeLimit = GetCfg()?.TimeLimitLink;
            if (timeLimit != null)
            {
                var curTime = TimeManager.GetServerUnixTime();
                var lockStr = string.Empty;
                foreach (var time in timeLimit)
                {
                    var startTime = DateFormatUtil.GetSecondsByDateStr(time.StartTime);
                    var endTime = DateFormatUtil.GetSecondsByDateStr(time.StartTime);
                    if (curTime < startTime || curTime > endTime)
                    {
                        lockStr = time.Desc;
                        continue;
                    }
                    lockStr = string.Empty;
                    break;
                }
                if (!string.IsNullOrEmpty(lockStr))
                {
                    limitStr = !string.IsNullOrEmpty(limitStr) ? Utility.Text.Format("{0},{1}", limitStr, lockStr) : lockStr;
                }
            }
            if (!string.IsNullOrEmpty(limitStr))
            {
                limitStr = Utility.Text.Format("{0}解锁", limitStr);
            }
            return limitStr;
        }
    }

    /// <summary>
    /// 背包道具
    /// </summary>
    public bool GetIsBagItem => GetBagType == (int)eItemRenderType.BagItem;

    /// <summary>
    /// 碎片道具
    /// </summary>
    public bool GetIsFragmentItem => GetBagType == (int)eItemRenderType.FragmentList;

    /// <summary>
    /// 宝箱道具
    /// </summary>
    public bool GetIsBoxItem => GetType == (int)eItemType.BoxItem;


    /// <summary>
    /// 宝箱礼包  全获得
    /// </summary>
    public bool GetIsAllGetBoxType => GetIsBoxItem && GetSubType == (int)eBoxSubType.AllGet;

    /// <summary>
    /// 自选宝箱  多选一
    /// </summary>
    public bool GetIsOptionalItemGiftType => GetIsBoxItem && GetSubType == (int)eBoxSubType.SelectOneFromMulti;

    /// <summary>
    /// 随机宝箱
    /// </summary>
    public bool GetIsRandomBoxGiftType => GetIsRandomOneBoxType || GetIsRandomMultiBoxType;

    /// <summary>
    /// 多随一宝箱
    /// </summary>
    public bool GetIsRandomOneBoxType => GetIsBoxItem && GetSubType == (int)eBoxSubType.RandomOneFromMulti;

    /// <summary>
    /// 多随多宝箱
    /// </summary>
    public bool GetIsRandomMultiBoxType => GetIsBoxItem && GetSubType == (int)eBoxSubType.RandomMultiFromMulti;

    /// <summary>
    /// 使用类型的道具
    /// </summary>
    public bool GetIsUseTypeItem => GetIsFragmentItem || GetIsUseItem || GetIsHangUpItem || GetIsBoxItem;

    /// <summary>
    /// 直接使用道具
    /// </summary>
    public bool GetIsUseItem => GetType == (int)eItemType.UseItem;

    /// <summary>
    /// 挂机道具
    /// </summary>
    public bool GetIsHangUpItem => GetType == (int)eItemType.HangUpItem;

    /// <summary>
    /// 挂机道具Up角标
    /// </summary>
    public bool IsUp;

    public List<ItemTipsGetWayData> GetGetWay
    {
        get
        {
            var jumpIds = GetCfg()?.JumpIds;
            if (jumpIds is not { Count: > 0 }) return null;
            var getWayDatas = new List<ItemTipsGetWayData>();
            foreach (var jumpId in jumpIds)
            {

                var cfg = BagControl.Instance.Model.GetJumpCfgById(jumpId);
                if (cfg == null) continue;
                var getWay = new ItemTipsGetWayData
                {
                    type = eGetWayType.System,
                    id = jumpId
                };
                getWayDatas.Add(getWay);
            }
            return getWayDatas;
        }
    }

    /// <summary>
    /// 宝箱信息
    /// </summary>
    public List<ItemTipsRateData> GetBoxRewardDatas
    {
        get
        {
            if (!GetIsBoxItem) return null;
            var useParam = GetCfg()?.UseParam;
            if (useParam == null || useParam.Count == 0 || useParam[0] == null || useParam[0].Count == 0)
            {
                return null;
            }
            var treasureCfg = ConfigMgr.Instance.GetConfigVoById<TreasureVo>(useParam[0][0]);
            if (treasureCfg == null || treasureCfg.Content.Count == 0) return null;
            var mDatas = new List<ItemTipsRateData>();
            var weightMax = 0;
            foreach (var content in treasureCfg.Content)
            {
                var id = content[0];
                var count = content[1];
                var weight = content.Count >= 3 ? content[2] : 0;
                var itemCfg = BagControl.Instance.Model.GetItemCfgById(id);
                weightMax += weight;
                var getWay = new ItemTipsRateData
                {
                    ItemId = id,
                    ItemCount = count,
                    Weight = weight,
                    Name = itemCfg?.Name,
                };
                mDatas.Add(getWay);
            }
            mDatas.ForEach(s => { s.WeightMax = weightMax; });
            return mDatas;
        }
    }

    /// <summary>
    /// 使用类型道具
    /// </summary>
    public List<CommonItemData> GetUseRewardDatas
    {
        get
        {
            var boxRewardDatas = GetBoxRewardDatas;
            if (boxRewardDatas != null)
            {
                return boxRewardDatas.Select(item => new CommonItemData(item.ItemId, item.ItemCount)).ToList();
            }
            if (!GetIsUseTypeItem) return null;
            var useParam = GetCfg()?.UseParam;
            if (useParam == null || useParam.Count == 0 || useParam[0] == null || useParam[0].Count == 0)
            {
                return null;
            }
            var mDatas = new List<CommonItemData>();
            if (GetIsUseItem)
            {
                mDatas.AddRange(useParam.Select(item => new CommonItemData(item[0], item[1])));
            }
            else if (GetIsHangUpItem)
            {
                Logger.PrintError("未实现");
            }
            return mDatas;
        }
    }
    /// <summary>
    /// 碎片合成所需的道具
    /// </summary>
    public int GetFragmentSynthesisNum
    {
        get
        {
            if (!GetIsFragmentItem) return 0;
            var useParam = GetCfg()?.UseParam;
            if (useParam == null || useParam.Count == 0 || useParam[0] == null || useParam[0].Count == 0)
            {
                return 0;
            }
            return useParam[0][1];
        }
    }

    public ItemVo ItemCfg { get; private set; }

    public ResourceInfo ServerInfo { get; private set; }
}
