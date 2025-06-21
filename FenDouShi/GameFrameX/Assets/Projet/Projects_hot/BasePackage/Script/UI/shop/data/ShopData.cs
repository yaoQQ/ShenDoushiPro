using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum ShopItemType
{
    GD = 1,
    GE = 2,

    None
}
public class ShopData
{
    public ShopItemType shopType;
    public List<ShopItemData> shopItemDataList;
}

