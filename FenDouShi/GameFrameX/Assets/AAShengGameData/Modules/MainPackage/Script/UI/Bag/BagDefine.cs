

/// <summary>
/// 道具render类型  对应bagType
/// </summary>
public enum eItemRenderType
{
    Null = -1,

    Normal = 0,  //不在背包

    BagItem = 1,//道具

    FragmentList = 2,//碎片
}

/// <summary>
/// 对应 type
/// </summary>
public enum eItemType
{
    Null = 0,

    UseItem = 101,//直接使用

    BoxItem = 102,//宝箱

    HangUpItem = 103,//挂机道具
}

/// <summary>
/// 宝箱类型 subType
/// </summary>
public enum eBoxSubType
{
    Null = 0,

    AllGet = 101,//全获得宝箱

    SelectOneFromMulti = 102,//多选一宝箱（参数：下标1, 数量1, 下标2, 数量2）

    RandomOneFromMulti = 103,//多随一宝箱

    RandomMultiFromMulti = 104,//多随多宝箱
}

/// <summary>
/// 背包类型
/// </summary>
public enum eBagType
{
    all = 0, // 后端说是请求背包 1  2  3
    Currency = 1, //代币
    Exp = 2, //经验
    Material = 3, //材料
}

public enum eItemSource
{
    Null = 0,
    Bag = 1,//背包界面
    RewardShow = 2, //恭喜获得弹窗
}


