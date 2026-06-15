using System;
using System.Collections;
using System.Collections.Generic;

/* 事件id范围:
 * 网络:0~999999999
 * 通用:1000000000~∞
 */
public enum EEventType : int
{
    //--登录请求
    LoginComplete = 1000000000,//--描述： 登入成功
    //--获取主服务数据
    LoadAccountDataComplete,
    //--加载服务端列表配置的字段
    LoadServiceZoneDataComplete,
    //--加载玩家游戏数据
    LoginChangeService,
    //--错误返回
    OnLoginError,

    Login_connect_Success,//连接游戏websocket成功
    Login_connect_Out,//客户端主动断开websocket连接
    Load_Success_In_Game,
    Normal_QuitGame,
    ConnectSocket,
    /// <summary>游戏更新进度</summary>
    Game_Update_Progress,
    /// <summary>游戏退出</summary>
    Game_Exit,
    //--------------商店----------------
    //更新商城钻石数据
    Mall_Update_Diamond,
    //更新商城金币数据
    Mall_Update_Money,

    Loading_Bar_Show,
    Loading_Bar_Hide,

    //加载场景完成
    LoadSceneComplete,

    // 测试
    Test_AddItem,

    // 红点
    OnRedPointUpdate,

    // 邮件
    Mail_QueryMailInfoResp,
    Mail_DeleteMailResp,
    Mail_UpdateMail,
    Mail_UpdateMails,

    //背包
    BagItemChange,
    EventItemCountChange, //道具数量
    EventRefreshItemCount, //刷新道具数

    //背包下拉筛选 选中
    BagSelectChangeEvent,

    //Tips
    TipsRewardSelectEvent,

    //获得道具展示
    BagItemChange_Show,

    // 金币
    MoneyChangeEvent,

    //系统开启
    SystemOpen_InfoUpdate,
    SystemOpen_NewSystemOpen_Show,
    SystemOpenEvent,


    // 任务
    EventTaskUpdate, // 任务更新
    EventTaskFinish, // 任务完成
    EventTaskReward, // 任务奖励
    TaskListResp,
    TaskUpdateResp,


    //登陆成功初始化主界面
    MainUI_Login_Enter_Init,

    //角色信息更新
    EventRoleInfoUpdate,
    RolePowerCompareResp,
    RolePowerCompareDetailResp,
    RoleInfoGetLocationResp,
    RoleRenameResp,

    //好友
    UpdateFriendDotInfo, //友情信息更新，
    EventRecommendListUpdate, //推荐列表更新
    EventRecommendApplyedUpdate, //推荐列表申请更新
    EventSerachDataUpdate, //搜索数据更新
    EventFriendListUpdate, //列表更新
    EventFriendDotUpdate, //好友点更新

    // 聊天
    Chat_SetChannelMessage,
    Chat_AddNewMessage,
    ChatReadChannelResp,
    ChatSendMessageResp,

    //排行榜
    RankEvent_RedPoint,
    RankListEvent,
    RankLikeEvent,//点赞回调
    RankTopFiveEvent,
    RankProgressEvent,
    RankBaseInfoEvent,

    Assembly_Invoke,     // 跨程序集事件调用
    Assembly_Invoke_CallBack,     // 跨程序集事件调用回调

    // 挂机
    OnHookGetInfoResp,
    OnHookSpeedUpResp,
    OnHookGainRewardResp,
    //战斗日常
    EventFightDailyInfoUpdate, //战斗日常信息更新
    EventFightDailySweepUpdate, //战斗日常扫荡更新

    //战斗
    EventBattleInfoUpdate,
    EventBattleRoundUpdate,
    EventBattleResult,
    EventBattleTargetInfoUpdate,
    EventBattleReportListUpdate,


    //充值支付
    EventRecharegeInfoUpdate, //充值信息更新

    //历练
    EventExperienceInfoUpdate,

    //vip
    EventVipInfoUpdate, //vip信息更新
    EventVipGiftUpdate, //vip礼包更新
    EventVipLuxuryGiftUpdate, //vip奢侈礼包更新
    EventVipExclusiveGiftUpdate, //vip独家礼包更新
    EventVipDailyRewardsUpdate, //vip每日奖励更新

    //竞技场
    EventArenaInfoUpdate,
    EventArenaMatchUpdate,
    EventArenaRefreshUpdate,
    EventArenaNewSeason,
    EventArenaTimesReward,

    //雅典娜试炼
    AthenaTrialInfoUpdate,
}

public static class EEventTypeHelper
{
    static HashSet<int> eventTypeNumbers;

    public static bool Contains(int eventType)
    {
        if (eventTypeNumbers == null)
        {
            eventTypeNumbers = new();

            // TODO:不装箱
            foreach (EEventType i in Enum.GetValues(typeof(EEventType)))
            {
                eventTypeNumbers.Add((int)i);
            }
        }
        return eventTypeNumbers.Contains(eventType);
    }
}