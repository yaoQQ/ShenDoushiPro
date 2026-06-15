/// <summary>
/// 好友工具类
/// </summary>

public class FriendTools
{
    //好友是否已满
    public static bool FriendFull()
    {
        var friendMax = ConfigMgr.GetGameConst("friend_max")[0]; //本服好友上限
        var info = FriendControl.Instance.Model.GetFriendData();
        if (info != null)
        {
            friendMax = friendMax - info.friendCount; //好友上限
        }
        return friendMax <= 0;
    }
}