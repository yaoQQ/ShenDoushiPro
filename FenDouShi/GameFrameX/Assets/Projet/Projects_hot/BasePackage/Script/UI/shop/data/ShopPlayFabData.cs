using UnityEngine;
using System.Collections.Generic;
using PlayFab.ClientModels;

public class ShopPlayFabData : Singleton<ShopPlayFabData>
{
    private List<CatalogItem> CatalogItemList;
    public void SaveShopItemList(List<CatalogItem> catelists)
    {
        CatalogItemList = catelists;
    }
    public CatalogItem getCataLogItemByIndex(int index)
    {
        if (CatalogItemList == null)
        {
            return null;
        }
        if(index >= 0&&index< CatalogItemList.Count)
        {
            CatalogItem item = CatalogItemList[index];
            return item;
        }
        return null;
    }
}
