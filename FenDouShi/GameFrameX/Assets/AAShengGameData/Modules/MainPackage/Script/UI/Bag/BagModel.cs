

using msg.bag;
using msg.common;
using common;
using System.Collections.Generic;
using UnityEngine;

public partial class BagModel : BaseModel, IBagModel
{
    private Dictionary<int, ItemContainer> _bagTypeMap = new Dictionary<int, ItemContainer>();

    public void InitBagData(List<ResourceInfo> resourceInfos)
    {
        if (resourceInfos == null || resourceInfos.Count == 0)
        {
            return;
        }
        var typeMap = new HashSet<int>();
        var mCount = resourceInfos.Count;
        for (int i = 0; i < mCount; i++)
        {
            var info = resourceInfos[i];
            var renderType = GetItemBagType(info.Id);
            var bagInfo = GetOrCreatContainerByType((eItemRenderType)renderType);
            if (typeMap.Add(renderType))
            {
                bagInfo.Clear();
            }
            bagInfo.Add(info);
        }
    }

    private List<CommonItemData> tempChangeList;

    public void UpDateBagData(BagItemUpdateResp info)
    {
        var bagList = info.resourceInfos;
        if (bagList?.Count <= 0)
        {
            return;
        }
        var mCount = bagList.Count;
        if (tempChangeList == null) tempChangeList = new List<CommonItemData>();
        else tempChangeList.Clear();
        for (int i = 0; i < mCount; i++)
        {
            var resourceInfo = bagList[i];
            var bagType = (eBagType)resourceInfo.Type;
            var renderType = GetItemBagType(resourceInfo.Id);
            var container = GetOrCreatContainerByType((eItemRenderType)renderType);
            var newItem = container?.UpdateItems(resourceInfo);
            if (bagType == eBagType.Currency)
            {
                tempChangeList.Add(newItem);
            }
            //道具更新
            Dispatch(EEventType.EventItemCountChange, resourceInfo);
        }
        EventManager.Instance.Dispatch(EEventType.BagItemChange, eBagType.Material);
        if (tempChangeList?.Count > 0)
        {
            EventManager.Instance.Dispatch(EEventType.MoneyChangeEvent, tempChangeList);
        }
    }

    public List<CommonItemData> GetItemDatas(eItemRenderType type)
    {
        var container = GetContainerByType(type);
        if (container != null)
        {
            return container.items;
        }
        return null;
    }



    public void SetItemRewardDatas(BagItemChangeResp itemChangeList)
    {
        var list = new List<CommonItemData>();
        foreach (var item in itemChangeList.resourceInfos)
        {
            list.Add(new CommonItemData(item));
        }
        //弹窗
        if (itemChangeList.Type == 1)
        {
            var isOpen = UIViewManager.Instance.GetIsShowing(UIViewEnum.ItemGetRewardsView);
            if (isOpen)
            {
                UIViewManager.Instance.Hide(UIViewEnum.ItemGetRewardsView);
            }
            UIViewManager.Instance.Show(UIViewEnum.ItemGetRewardsView, list);
        }
        //飘字
        else if (itemChangeList.Type == 2)
        {
            var isOpen = UIViewManager.Instance.GetIsShowing(UIViewEnum.ItemGetRewardsSmallView);
            if (isOpen)
            {
                EventManager.Instance.Dispatch(EEventType.BagItemChange_Show, list);
            }
            else
            {
                UIViewManager.Instance.Show(UIViewEnum.ItemGetRewardsSmallView, list);
            }
        }
    }

    public void OpenGetRewardView()
    {
    }


    /// <summary>
    /// 背包当前容量
    /// </summary>
    /// <param name="bagType"></param>
    /// <returns></returns>
    public int GetBagCapacity(eItemRenderType bagType)
    {
        if (bagType == eItemRenderType.BagItem || bagType == eItemRenderType.FragmentList)
        {
            var bagDatas = GetItemDatas(eItemRenderType.BagItem);
            var fragmentDatas = GetItemDatas(eItemRenderType.FragmentList);
            return (bagDatas?.Count ?? 0) + (fragmentDatas?.Count ?? 0);
        }
        var container = GetItemDatas(bagType);
        return container?.Count ?? 0;
    }

    /// <summary>
    /// 背包最大容量
    /// </summary>
    public int GetBagMaxCapacity(eItemRenderType bagType)
    {
        var container = GetContainerByType(bagType);
        if (container != null)
        {
            return container.Capacity;
        }
        return 0;
    }


    private ItemContainer GetContainerByType(eItemRenderType bagType)
    {
        if (_bagTypeMap.TryGetValue((int)bagType, out ItemContainer container))
        {
            return container;
        }
        return null;
    }

    private ItemContainer GetOrCreatContainerByType(eItemRenderType bagType)
    {
        if (_bagTypeMap.TryGetValue((int)bagType, out ItemContainer container))
        {
            return container;
        }
        var bagInfo = new ItemContainer();
        bagInfo.Init(bagType, 500);
        _bagTypeMap[(int)bagType] = bagInfo;
        return bagInfo;
    }

    public long GetItemCountByItemId(int itemId)
    {
        var cfg = GetItemCfgById(itemId);
        if (cfg != null)
        {
            var container = GetContainerByType((eItemRenderType)cfg.BagType);
            return container?.GetItemCountByItemId(itemId) ?? 0;
        }
        return 0;
    }

    public long GetItemMinEpireTimeByRenderType(eItemRenderType renderType)
    {
        var itemDatas = GetItemDatas(renderType);
        if (itemDatas == null) return 0;
        var min = long.MaxValue;
        var curTime = TimeManager.GetServerUnixTime();
        foreach (var item in itemDatas)
        {
            var time = item.GetExpireTime;
            if (time <= 0 || time < curTime)
                continue;
            if (time < min)
                min = time;
        }
        return min == long.MaxValue ? 0 : min;
    }

    public long GetItemMinRemianTimeByRenderType(eItemRenderType renderType)
    {
        var min = GetItemMinEpireTimeByRenderType(renderType);
        if (min <= 0) return 0;
        var curTime = TimeManager.GetServerUnixTime();
        return min - curTime;
    }
}


public class ItemContainer
{

    //[id，num]

    public Dictionary<int, CommonItemData> itemsUidMap;

    public List<CommonItemData> items;

    public eItemRenderType BagType { get; private set; }

    public int Capacity { get; private set; }

    private Dictionary<int, long> itemId2CountMap;

    public long GetItemCountByItemId(int itemId)
    {
        if (itemId2CountMap == null)
            return 0;
        if (itemId2CountMap.TryGetValue(itemId, out long count))
            return count;
        return 0;
    }

    public long GetItemCountByUid(int itemId)
    {
        if (itemsUidMap == null)
            return 0;
        if (itemsUidMap.TryGetValue(itemId, out var itemData))
            return itemData.Count;
        return 0;
    }

    public void Init(eItemRenderType bagType, int capacity)
    {
        BagType = bagType;
        Capacity = capacity;
        itemsUidMap = new Dictionary<int, CommonItemData>();
        items = new List<CommonItemData>();
        itemId2CountMap = new Dictionary<int, long>();
    }

    public void Clear()
    {
        itemsUidMap.Clear();
        items.Clear();
        itemId2CountMap.Clear();
    }

    public void Sort()
    {
    }

    private void SyncItemCount(int id, long changeNum)
    {
        if (itemId2CountMap.TryGetValue(id, out var value))
        {
            value += changeNum;
            if (value <= 0)
            {
                itemId2CountMap.Remove(id);
            }
            else
            {
                itemId2CountMap[id] = value;
            }
        }
        else
        {
            if (changeNum > 0)
            {
                itemId2CountMap[id] = changeNum;
            }
        }
    }


    public CommonItemData UpdateItems(ResourceInfo info)
    {
        var uniqueId = GetUniqueId(info);
        var localData = GetItemByUID(uniqueId);
        if (localData != null)
        {
            if (info.Num <= 0)
            {
                Remove(uniqueId, localData);
            }
            else
            {
                SyncItemCount(info.Id, info.Num - localData.Count);
            }
            localData.SetServerInfo(info);
            return localData;
        }
        else
        {
            return Add(info);
        }
    }

    public int GetUniqueId(ResourceInfo info)
    {
        if (info == null)
        {
            return 0;
        }
        if (info.uniqueId > 0)
        {
            return info.uniqueId;
        }
        return info.Id;
    }


    public CommonItemData GetItemByUID(int uid)
    {
        if (itemsUidMap.TryGetValue(uid, out CommonItemData itemData))
        {
            return itemData;
        }
        return null;
    }

    public CommonItemData Add(ResourceInfo info)
    {
        var uniqueId = GetUniqueId(info);
        var newItem = new CommonItemData(info);
        itemsUidMap[uniqueId] = newItem;
        items.Add(newItem);
        SyncItemCount(info.Id, info.Num);
        return newItem;
    }

    public void Remove(int uniqueId, CommonItemData itemData)
    {
        SyncItemCount(itemData.ItemId, -itemData.Count);
        itemsUidMap.Remove(uniqueId);
        items.Remove(itemData);
    }
}



