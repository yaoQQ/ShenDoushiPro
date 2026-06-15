
public interface IBagModel
{
    /// <summary>
    /// 获取背包中某个道具的数量
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public long GetItemCountByItemId(int itemId);
}