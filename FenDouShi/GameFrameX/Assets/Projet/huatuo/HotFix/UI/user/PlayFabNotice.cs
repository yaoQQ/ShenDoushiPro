using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayFabNotice
{

	//--登录请求
	public const string LoginComplete = "LoginComplete";//--描述： 登入成功
														 //--获取玩家账号数据
	public const string LoadAccountDataComplete = "LoadAccountDataComplete";
	//--加载服务端配置的字段
	public const string LoadTitleDataComplete = "LoadTitleDataComplete";
	//--加载玩家游戏数据
	public const string LoadUserDataComplete = "LoadUserDataComplete";

	//--更新玩家信息
	public const string UpdateUserInfo = "UpdateUserInfo";
	//--更新宝石
	public const string UpdatePlayFabUserDiamond = "UpdatePlayFabUserDiamond";
	// --更新金币
	public const string UpdatePlayFabUserGold = "UpdatePlayFabUserGold";


	//--设置玩家名字
	public const string SetUserDisplayNameComplete = "SetUserDisplayNameComplete";
	//--更新排行榜
	public const string UpdateStatisticComplete = "UpdateStatisticComplete";
	//--获取排行榜
	public const string GetLeaderboardComplete = "GetLeaderboardComplete";
	//--获取服务端配置的字段
	public const string GetTitleDataValueComplete = "GetTitleDataValueComplete";
	//--获取自己存储的游戏数据
	public const string GetUserDataValueComplete = "GetUserDataValueComplete";
	//--更新保存玩家游戏数据
	public const string UpdateUserGameDataComplete = "UpdateUserGameDataComplete";
	//--获取商店的物品列表
	public const string getStoreItemsInfo = "getStoreItemsInfo";

	//--错误返回
	public const string OnPlayFabError = "OnPlayFabError";


}

