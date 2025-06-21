using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShopItemData
{
    public string catalogVersion;//所属cataLog
    public string storeId;//商店Id
    public string itemId;//物品id
    public string itemName;//物品名 =物品id
    public int price;//物品价格
    public ShopItemType virtualCurrency;//花费的虚拟金币类型
}

