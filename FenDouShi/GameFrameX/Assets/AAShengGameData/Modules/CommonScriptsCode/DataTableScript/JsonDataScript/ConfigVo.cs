using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AbstractionVo{
	[JsonProperty("id")]
	public int Id;  //提炼的道具id
	[JsonProperty("costItemId")]
	public int CostItemId;  //消耗的道具id
	[JsonProperty("costItemNum")]
	public int CostItemNum;  //消耗的道具数量
	[JsonProperty("costTime")]
	public int CostTime;  //消耗时长
}

public class ChatVo{
	[JsonProperty("id")]
	public int Id;  //频道id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("functionId")]
	public int FunctionId;  //功能开启
	[JsonProperty("canSpeak")]
	public int CanSpeak;  //玩家是否可发言
	[JsonProperty("speakCD")]
	public int SpeakCD;  //发言CD
	[JsonProperty("recordSaveDay")]
	public int RecordSaveDay;  //聊天记录保留天数
	[JsonProperty("recordSaveNum")]
	public int RecordSaveNum;  //聊天记录保留条数
	[JsonProperty("icon")]
	public string Icon;  //频道图标
}

public class ChatMsgVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("channelId")]
	public int ChannelId;  //频道id
	[JsonProperty("content")]
	public string Content;  //内容
}

public class CurrencySystemVo{
	[JsonProperty("id")]
	public int Id;  //系统ID
	[JsonProperty("name")]
	public string Name;  //描述
	[JsonProperty("itemsLink")]
	public List<CurrencySystemItemsLinkVo> ItemsLink;  //道具列表
}

public class CurrencySystemItemsLinkVo{
	[JsonProperty("refId")]
	public int RefId;  //唯一id
	[JsonProperty("itemId")]
	public int ItemId;  //道具id
	[JsonProperty("buy")]
	public int Buy;  //是否可以购买
	[JsonProperty("buyView")]
	public string BuyView;  //购买视图
	[JsonProperty("viewParams")]
	public List<string> ViewParams;  //视图参数
	[JsonProperty("functionId")]
	public int FunctionId;  //功能开放ID
}

public class ExperienceVo{
	[JsonProperty("id")]
	public int Id;  //id
	[JsonProperty("group")]
	public int Group;  //分组
	[JsonProperty("type")]
	public int Type;  //玩法分类
	[JsonProperty("name")]
	public string Name;  //玩法名
	[JsonProperty("info")]
	public string Info;  //玩法重要信息
	[JsonProperty("bg")]
	public string Bg;  //背景
	[JsonProperty("icon")]
	public string Icon;  //图标
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
	[JsonProperty("functionid")]
	public int Functionid;  //关联功能
}

public class GuessLevelVo{
	[JsonProperty("level")]
	public int Level;  //ID
	[JsonProperty("exp")]
	public int Exp;  //升到下级需要的经验值
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
}

public class ItemVo{
	[JsonProperty("id")]
	public int Id;  //id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("quality")]
	public int Quality;  //品质
	[JsonProperty("icon")]
	public string Icon;  //图标
	[JsonProperty("describe")]
	public string Describe;  //描述
	[JsonProperty("bagType")]
	public int BagType;  //背包类型
	[JsonProperty("type")]
	public int Type;  //类型
	[JsonProperty("autoUse")]
	public int AutoUse;  //获得自动使用
	[JsonProperty("subType")]
	public int SubType;  //子类型
	[JsonProperty("useParam")]
	public List<List<int>> UseParam;  //使用参数
	[JsonProperty("useJump")]
	public List<int> UseJump;  //使用跳转
	[JsonProperty("useNum")]
	public int UseNum;  //默认使用数量
	[JsonProperty("playerLevelLimit")]
	public int PlayerLevelLimit;  //玩家等级限制
	[JsonProperty("openDayLimit")]
	public int OpenDayLimit;  //开服天数限制
	[JsonProperty("timeLimitLink")]
	public List<ItemTimeLimitLinkVo> TimeLimitLink;  //使用期限限制
	[JsonProperty("reclaim")]
	public List<List<int>> Reclaim;  //过期回收
	[JsonProperty("stackLimit")]
	public int StackLimit;  //堆叠上限
	[JsonProperty("holdLimit")]
	public int HoldLimit;  //持有上限
	[JsonProperty("jumpIds")]
	public List<int> JumpIds;  //获取途径id列表
	[JsonProperty("qualityEffect")]
	public string QualityEffect;  //品质特效
	[JsonProperty("sort")]
	public int Sort;  //排序值
	[JsonProperty("quickBuy")]
	public int QuickBuy;  //快捷购买
	[JsonProperty("event")]
	public string Event;  //获得物品触发事件
	[JsonProperty("redDot")]
	public int RedDot;  //红点
}

public class ItemTimeLimitLinkVo{
	[JsonProperty("id")]
	public int Id;  //id
	[JsonProperty("desc")]
	public string Desc;  //限制描述
	[JsonProperty("startTime")]
	public string StartTime;  //开始时间
	[JsonProperty("endTime")]
	public string EndTime;  //结束时间
	[JsonProperty("activityId")]
	public string ActivityId;  //关联活动id
}

public class JumpVo{
	[JsonProperty("id")]
	public int Id;  //id
	[JsonProperty("name")]
	public string Name;  //跳转名称
	[JsonProperty("icon")]
	public string Icon;  //图标
	[JsonProperty("viewName")]
	public string ViewName;  //界面名称
	[JsonProperty("viewParams")]
	public List<string> ViewParams;  //界面名称
	[JsonProperty("funOpen")]
	public int FunOpen;  //功能开启
}

public class LocationVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("countryCode")]
	public int CountryCode;  //国家编码
	[JsonProperty("countryName")]
	public string CountryName;  //国家名称
	[JsonProperty("provinceCode")]
	public int ProvinceCode;  //省编码
	[JsonProperty("provinceName")]
	public string ProvinceName;  //省名称
	[JsonProperty("cityCode")]
	public int CityCode;  //市编码
	[JsonProperty("cityName")]
	public string CityName;  //市名称
}

public class MailVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("type")]
	public int Type;  //分类
	[JsonProperty("title")]
	public string Title;  //标题
	[JsonProperty("content")]
	public string Content;  //邮件内容
	[JsonProperty("sender")]
	public string Sender;  //发件人
	[JsonProperty("expire")]
	public int Expire;  //有效时长
	[JsonProperty("forbidAutoRead")]
	public int ForbidAutoRead;  //不可一键领取
}

public class MiscVo{
	[JsonProperty("id")]
	public string Id;  //ID
	[JsonProperty("value")]
	public List<int> Value;  //值
	[JsonProperty("str")]
	public List<string> Str;  //值
	[JsonProperty("desc")]
	public string Desc;  //描述
}

public class OnHookRewardVo{
	[JsonProperty("dungeonId")]
	public int DungeonId;  //副本id
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //挂机奖励
	[JsonProperty("highRewards")]
	public List<int> HighRewards;  //高级奖励
}

public class PayVo{
	[JsonProperty("id")]
	public int Id;  //充值ID
	[JsonProperty("type")]
	public int Type;  //充值类型
	[JsonProperty("icon")]
	public string Icon;  //显示图标
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("money")]
	public int Money;  //金额
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
	[JsonProperty("firstRewards")]
	public List<List<int>> FirstRewards;  //首充奖励
	[JsonProperty("vipExp")]
	public int VipExp;  //vip经验奖励
	[JsonProperty("refresh")]
	public int Refresh;  //刷新类型
	[JsonProperty("limit")]
	public int Limit;  //刷新周期内限购次数
	[JsonProperty("ignoreAcc")]
	public int IgnoreAcc;  //是否忽略累充
	[JsonProperty("disableVoucher")]
	public int DisableVoucher;  //是否禁用代金券
}

public class PlayernameVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("part")]
	public int Part;  //昵称分段
	[JsonProperty("text")]
	public string Text;  //文本
}

public class RedDotVo{
	[JsonProperty("id")]
	public int Id;  //红点id
	[JsonProperty("parentId")]
	public int ParentId;  //父红点
	[JsonProperty("desc")]
	public string Desc;  //描述
	[JsonProperty("functionIds")]
	public List<int> FunctionIds;  //功能id
	[JsonProperty("type")]
	public int Type;  //类型
	[JsonProperty("params")]
	public List<int> Params;  //参数
}

public class ResultVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("notice")]
	public string Notice;  //提示内容
}

public class RoleLevelVo{
	[JsonProperty("level")]
	public int Level;  //id
	[JsonProperty("exp")]
	public int Exp;  //经验
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //奖励
}

public class SensitiveWordVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("str")]
	public string Str;  //字符串测试
}

public class StoreVo{
	[JsonProperty("id")]
	public int Id;  //商店ID
	[JsonProperty("name")]
	public string Name;  //商店名称
	[JsonProperty("type")]
	public int Type;  //商店类型
	[JsonProperty("funOpen")]
	public int FunOpen;  //功能开启
	[JsonProperty("openDay")]
	public int OpenDay;  //开服天数
	[JsonProperty("icon")]
	public string Icon;  //tab图标
	[JsonProperty("storeRefreshType")]
	public int StoreRefreshType;  //商店刷新类型
	[JsonProperty("freeRefreshType")]
	public int FreeRefreshType;  //免费次数刷新类型
	[JsonProperty("freeRefreshParams")]
	public List<int> FreeRefreshParams;  //免费次数刷新参数
	[JsonProperty("itemRefreshType")]
	public int ItemRefreshType;  //道具次数刷新类型
	[JsonProperty("itemRefreshParams")]
	public List<int> ItemRefreshParams;  //道具次数刷新参数
	[JsonProperty("itemRefreshCost")]
	public List<List<int>> ItemRefreshCost;  //道具刷新消耗
	[JsonProperty("product")]
	public List<List<int>> Product;  //商品
	[JsonProperty("productSort")]
	public int ProductSort;  //商品是否排序
	[JsonProperty("currencyId")]
	public int CurrencyId;  //资源栏显示id
	[JsonProperty("autoBuy")]
	public int AutoBuy;  //自动购买
}

public class StoreGroupVo{
	[JsonProperty("id")]
	public int Id;  //商店ID
	[JsonProperty("name")]
	public string Name;  //商店名称
	[JsonProperty("icon")]
	public string Icon;  //商店图标
	[JsonProperty("title")]
	public string Title;  //商店标题
	[JsonProperty("types")]
	public List<int> Types;  //商店类型
}

public class StoreProductVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("groupId")]
	public int GroupId;  //商品库ID
	[JsonProperty("groupIndex")]
	public int GroupIndex;  //商品索引
	[JsonProperty("buyType")]
	public int BuyType;  //限购类型
	[JsonProperty("buyCount")]
	public int BuyCount;  //限购次数
	[JsonProperty("cost")]
	public List<int> Cost;  //购买消耗
	[JsonProperty("rewards")]
	public List<int> Rewards;  //商品奖励
	[JsonProperty("discount")]
	public int Discount;  //折扣显示
	[JsonProperty("weight")]
	public int Weight;  //权重
	[JsonProperty("condition")]
	public List<List<int>> Condition;  //购买限制
	[JsonProperty("autoBuy")]
	public int AutoBuy;  //自动购买
	[JsonProperty("isRecommend")]
	public int IsRecommend;  //是否为推荐
	[JsonProperty("isMustBuy")]
	public int IsMustBuy;  //是否为必买
}

public class StoreQuickBuyVo{
	[JsonProperty("id")]
	public int Id;  //快速购买id
	[JsonProperty("cost")]
	public List<int> Cost;  //购买消耗
	[JsonProperty("rewards")]
	public List<int> Rewards;  //购买获得
}

public class TreasureVo{
	[JsonProperty("id")]
	public int Id;  //id
	[JsonProperty("name")]
	public string Name;  //奖励组名称
	[JsonProperty("content")]
	public List<List<int>> Content;  //奖励列表
}

public class UnitsVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("name")]
	public string Name;  //系统名称
	[JsonProperty("unitLink")]
	public List<UnitsUnitLinkVo> UnitLink;  //单位配置（注意排序）
}

public class UnitsUnitLinkVo{
	[JsonProperty("refId")]
	public int RefId;  //档位编号
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("triggest")]
	public int Triggest;  //触发位数
	[JsonProperty("unit")]
	public int Unit;  //单位位数
	[JsonProperty("decimals")]
	public int Decimals;  //小数点后位数
}

public class ArenaLevelVo{
	[JsonProperty("level")]
	public int Level;  //ID
	[JsonProperty("name")]
	public string Name;  //段位名称
	[JsonProperty("icon")]
	public string Icon;  //段位图标资源
	[JsonProperty("score")]
	public List<int> Score;  //积分范围
	[JsonProperty("seasonFall")]
	public int SeasonFall;  //赛季结算积分回退比例
	[JsonProperty("regex")]
	public string Regex;  //公式
	[JsonProperty("robotScore")]
	public List<int> RobotScore;  //机器人积分范围
	[JsonProperty("lineupGroup")]
	public int LineupGroup;  //阵容组ID
}

public class ArenaTimesRewardVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("times")]
	public int Times;  //需要的挑战次数
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
}

public class AttrVo{
	[JsonProperty("id")]
	public int Id;  //效果ID
	[JsonProperty("name")]
	public string Name;  //属性名称
	[JsonProperty("type")]
	public string Type;  //对应类型名称
}

public class BuffVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("type")]
	public int Type;  //效果类型
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("des")]
	public string Des;  //描述
	[JsonProperty("icon")]
	public string Icon;  //图标
	[JsonProperty("buffShow")]
	public string BuffShow;  //buff的展示效果
	[JsonProperty("group")]
	public int Group;  //组
	[JsonProperty("priority")]
	public int Priority;  //组内优先级
	[JsonProperty("compositionType")]
	public int CompositionType;  //叠加类型
	[JsonProperty("compositionLimit")]
	public int CompositionLimit;  //叠加次数
	[JsonProperty("param")]
	public List<int> Param;  //效果参数
	[JsonProperty("formula")]
	public int Formula;  //效果计算公式
	[JsonProperty("calcType")]
	public int CalcType;  //结算类型
	[JsonProperty("calcTiming")]
	public int CalcTiming;  //结算时机
	[JsonProperty("duration")]
	public int Duration;  //持续回合数
	[JsonProperty("isNegativity")]
	public int IsNegativity;  //是否负面buff
	[JsonProperty("isCanClean")]
	public int IsCanClean;  //是否能被驱散
	[JsonProperty("initiativeType")]
	public int InitiativeType;  //主动技能类型
}

public class FightVo{
	[JsonProperty("id")]
	public int Id;  //战斗id
	[JsonProperty("fightType")]
	public int FightType;  //类型
	[JsonProperty("name")]
	public string Name;  //队伍名称
	[JsonProperty("groupId")]
	public int GroupId;  //怪物组id
}

public class FightRatingVo{
	[JsonProperty("id")]
	public int Id;  //方案ID
	[JsonProperty("type")]
	public int Type;  //评分类型
	[JsonProperty("condition")]
	public List<int> Condition;  //条件数值
	[JsonProperty("desc")]
	public string Desc;  //方案描述
}

public class FightTypeVo{
	[JsonProperty("type")]
	public int Type;  //战斗类型
	[JsonProperty("desc")]
	public string Desc;  //描述
	[JsonProperty("canSkip")]
	public int CanSkip;  //是否可以跳过战斗
	[JsonProperty("formationid")]
	public int Formationid;  //布阵种类id
	[JsonProperty("maxRound")]
	public int MaxRound;  //最大回合数
	[JsonProperty("background")]
	public string Background;  //默认战斗场景
	[JsonProperty("isPvp")]
	public int IsPvp;  //是否玩家间的战斗
	[JsonProperty("cleanBuffAfterSection")]
	public int CleanBuffAfterSection;  //波次间是否清理buff
	[JsonProperty("intervalInMillis")]
	public int IntervalInMillis;  //战斗间隔毫秒数
}

public class FormationVo{
	[JsonProperty("id")]
	public int Id;  //布阵种类id
	[JsonProperty("name")]
	public string Name;  //按钮名称
	[JsonProperty("icon")]
	public string Icon;  //tab图标
	[JsonProperty("functionid")]
	public int Functionid;  //功能开启id
}

public class FormulaVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("name")]
	public string Name;  //描述
	[JsonProperty("regex")]
	public string Regex;  //公式
}

public class MonsterVo{
	[JsonProperty("id")]
	public int Id;  //怪物ID
	[JsonProperty("name")]
	public string Name;  //名字
	[JsonProperty("templateId")]
	public int TemplateId;  //模版id
	[JsonProperty("attr")]
	public List<List<int>> Attr;  //怪物属性
	[JsonProperty("passiveIds")]
	public List<int> PassiveIds;  //特殊被动技能id
}

public class MonsterGroupVo{
	[JsonProperty("id")]
	public int Id;  //敌方阵容id
	[JsonProperty("wellen")]
	public List<MonsterGroupWellenVo> Wellen;  //战斗波次
}

public class MonsterGroupWellenVo{
	[JsonProperty("wellenid")]
	public int Wellenid;  //战斗波次id
	[JsonProperty("pos1")]
	public int Pos1;  //站位1怪物
	[JsonProperty("pos2")]
	public int Pos2;  //站位2怪物
	[JsonProperty("pos3")]
	public int Pos3;  //站位3怪物
	[JsonProperty("pos4")]
	public int Pos4;  //站位4怪物
	[JsonProperty("pos5")]
	public int Pos5;  //站位5怪物
}

public class MonsterTemplateVo{
	[JsonProperty("id")]
	public int Id;  //怪物ID
	[JsonProperty("name")]
	public string Name;  //名字
	[JsonProperty("modelId")]
	public int ModelId;  //模型id
	[JsonProperty("star")]
	public int Star;  //星级
	[JsonProperty("level")]
	public int Level;  //等级
	[JsonProperty("attackId")]
	public int AttackId;  //普攻
	[JsonProperty("skillId")]
	public int SkillId;  //怒攻技能id
	[JsonProperty("passiveIds")]
	public List<int> PassiveIds;  //默认被动技能id
}

public class RobotVo{
	[JsonProperty("id")]
	public int Id;  //机器人ID
	[JsonProperty("heroId")]
	public int HeroId;  //武将表ID
	[JsonProperty("attrs")]
	public List<List<int>> Attrs;  //属性
	[JsonProperty("level")]
	public int Level;  //等级
	[JsonProperty("degree")]
	public int Degree;  //阶级
	[JsonProperty("star")]
	public int Star;  //星级
	[JsonProperty("levelRange")]
	public List<int> LevelRange;  //等级范围
}

public class RobotLineupVo{
	[JsonProperty("id")]
	public int Id;  //ID
	[JsonProperty("group")]
	public int Group;  //阵容组
	[JsonProperty("desc")]
	public string Desc;  //描述
	[JsonProperty("lineup")]
	public List<List<int>> Lineup;  //阵容配置
}

public class SkillVo{
	[JsonProperty("id")]
	public int Id;  //效果ID
	[JsonProperty("name")]
	public string Name;  //技能名称
	[JsonProperty("describe")]
	public string Describe;  //技能描述
	[JsonProperty("icon")]
	public string Icon;  //技能图标
	[JsonProperty("skillShow")]
	public string SkillShow;  //技能表现ID
	[JsonProperty("cd")]
	public int Cd;  //技能冷却
	[JsonProperty("showtype")]
	public string Showtype;  //外显技能类型
	[JsonProperty("showtips")]
	public string Showtips;  //外显技能标签
	[JsonProperty("type")]
	public int Type;  //技能类型
	[JsonProperty("initiativeSkillType")]
	public int InitiativeSkillType;  //主动技能类型
	[JsonProperty("limit")]
	public List<int> Limit;  //限制
	[JsonProperty("resource")]
	public List<int> Resource;  //资源条件
	[JsonProperty("condition")]
	public List<int> Condition;  //触发条件及参数（被动）
	[JsonProperty("effects")]
	public List<int> Effects;  //技能效果列表
	[JsonProperty("priority")]
	public int Priority;  //优先级（主动/被动优先级）
}

public class SkillEffectVo{
	[JsonProperty("id")]
	public int Id;  //效果ID
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("condition")]
	public List<int> Condition;  //效果生效条件
	[JsonProperty("skillsign")]
	public string Skillsign;  //触发标识
	[JsonProperty("type")]
	public int Type;  //效果类型
	[JsonProperty("target")]
	public List<int> Target;  //目标选择
	[JsonProperty("probability")]
	public int Probability;  //发动概率
	[JsonProperty("param")]
	public List<int> Param;  //效果参数
	[JsonProperty("formula")]
	public int Formula;  //效果计算公式
}

public class TestFightVo{
	[JsonProperty("id")]
	public int Id;  //测试战斗ID
	[JsonProperty("atkSide")]
	public List<List<int>> AtkSide;  //攻击发阵容
	[JsonProperty("defSide")]
	public List<List<int>> DefSide;  //防守方阵容
}

public class FightDailyVo{
	[JsonProperty("id")]
	public int Id;  //每日副本id
	[JsonProperty("type")]
	public int Type;  //类型
	[JsonProperty("index")]
	public int Index;  //序号
	[JsonProperty("difficulty")]
	public int Difficulty;  //难度
	[JsonProperty("name")]
	public string Name;  //副本名称
	[JsonProperty("fightId")]
	public int FightId;  //战斗id
	[JsonProperty("recommendPower")]
	public int RecommendPower;  //推荐战斗力
	[JsonProperty("firstRewards")]
	public List<List<int>> FirstRewards;  //首通奖励
	[JsonProperty("sweepRewards")]
	public List<List<int>> SweepRewards;  //扫荡奖励
}

public class FightDailyTypeVo{
	[JsonProperty("type")]
	public int Type;  //类型
	[JsonProperty("name")]
	public string Name;  //类型名称
	[JsonProperty("nameRes")]
	public string NameRes;  //名称资源
	[JsonProperty("funcId")]
	public int FuncId;  //功能开启ID
	[JsonProperty("tabIcon")]
	public string TabIcon;  //tab图标
	[JsonProperty("freeSweep")]
	public int FreeSweep;  //免费扫荡次数
	[JsonProperty("buySweep")]
	public int BuySweep;  //付费扫荡次数
	[JsonProperty("buySweepCost")]
	public List<List<int>> BuySweepCost;  //付费扫荡单次消耗
}

public class FuncVo{
	[JsonProperty("id")]
	public int Id;  //功能ID
	[JsonProperty("linkId")]
	public List<int> LinkId;  //关联功能ID
	[JsonProperty("jumpId")]
	public int JumpId;  //功能跳转id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("icon")]
	public string Icon;  //图标
	[JsonProperty("desc")]
	public string Desc;  //描述
	[JsonProperty("sort")]
	public int Sort;  //开启优先级
	[JsonProperty("showConditionType")]
	public int ShowConditionType;  //显示条件类型
	[JsonProperty("showCondition")]
	public int ShowCondition;  //显示条件
	[JsonProperty("openConditionType")]
	public int OpenConditionType;  //开启条件类型
	[JsonProperty("openCondition")]
	public int OpenCondition;  //开启条件
	[JsonProperty("isNotice")]
	public int IsNotice;  //功能手册是否显示
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //功能手册开启奖励
	[JsonProperty("noticeTask")]
	public List<int> NoticeTask;  //功能手册任务列表
}

public class FuncConditionVo{
	[JsonProperty("id")]
	public int Id;  //条件ID
	[JsonProperty("level")]
	public int Level;  //等级
	[JsonProperty("vipLevel")]
	public int VipLevel;  //vip等级
	[JsonProperty("dungeonId")]
	public int DungeonId;  //副本ID
	[JsonProperty("openDay")]
	public int OpenDay;  //开服天数
}

public class FuncTaskVo{
	[JsonProperty("id")]
	public int Id;  //任务id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
}

public class HeroVo{
	[JsonProperty("id")]
	public int Id;  //圣斗士id
	[JsonProperty("online")]
	public int Online;  //是否上架
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("camp")]
	public int Camp;  //阵营
	[JsonProperty("occupation")]
	public int Occupation;  //职业
	[JsonProperty("coreState")]
	public int CoreState;  //核心输出
	[JsonProperty("quality")]
	public int Quality;  //品质
	[JsonProperty("aptitude")]
	public int Aptitude;  //资质
	[JsonProperty("positionings")]
	public List<int> Positionings;  //定位
	[JsonProperty("appearance")]
	public int Appearance;  //外观
	[JsonProperty("description")]
	public string Description;  //描述
	[JsonProperty("levelPlan")]
	public int LevelPlan;  //升级方案
	[JsonProperty("degreePlan")]
	public int DegreePlan;  //进阶方案
	[JsonProperty("initStar")]
	public int InitStar;  //初始星级
	[JsonProperty("starPlan")]
	public int StarPlan;  //升星方案
	[JsonProperty("skills")]
	public List<int> Skills;  //技能列表
	[JsonProperty("talentPlan")]
	public int TalentPlan;  //天赋方案
	[JsonProperty("illustratedReward")]
	public List<List<int>> IllustratedReward;  //图鉴奖励
	[JsonProperty("universeLevelPlan")]
	public int UniverseLevelPlan;  //小宇宙升级方案
}

public class HeroAppearanceVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("fightModel")]
	public int FightModel;  //战斗模型
	[JsonProperty("nurturanceModel")]
	public int NurturanceModel;  //养成界面模型
	[JsonProperty("bagModel")]
	public int BagModel;  //背包卡面
	[JsonProperty("itemModel")]
	public int ItemModel;  //道具图标
}

public class HeroBackVo{
	[JsonProperty("id")]
	public int Id;  //目标星数
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //回退消耗
}

public class HeroBagVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("numOfPurchases")]
	public List<int> NumOfPurchases;  //第几次购买
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //道具消耗
	[JsonProperty("capacity")]
	public int Capacity;  //容量提升
}

public class HeroBrokenVo{
	[JsonProperty("id")]
	public int Id;  //破境等级
	[JsonProperty("itemCost")]
	public List<List<int>> ItemCost;  //升下一级道具消耗
	[JsonProperty("heroCost")]
	public List<int> HeroCost;  //升下一星圣斗士消耗
	[JsonProperty("heroNum")]
	public int HeroNum;  //>=X星圣斗士数量
	[JsonProperty("levelLimit")]
	public int LevelLimit;  //等级上限
}

public class HeroCostVo{
	[JsonProperty("id")]
	public int Id;  //唯一ID
	[JsonProperty("type")]
	public int Type;  //圣斗士类型
	[JsonProperty("params")]
	public List<int> Params;  //参数
	[JsonProperty("star")]
	public int Star;  //星级
	[JsonProperty("num")]
	public int Num;  //数量
	[JsonProperty("replaceItems")]
	public List<int> ReplaceItems;  //替代道具
}

public class HeroDegreeVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //进阶方案
	[JsonProperty("degree")]
	public int Degree;  //阶级
	[JsonProperty("levelLimit")]
	public int LevelLimit;  //等级限制
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //升下一阶消耗
	[JsonProperty("attrAdd")]
	public int AttrAdd;  //属性加成
}

public class HeroIncreaseLevelVo{
	[JsonProperty("id")]
	public int Id;  //增幅等级
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //升下一级消耗
	[JsonProperty("attrAdd")]
	public List<List<int>> AttrAdd;  //属性加成
	[JsonProperty("abstractionNum")]
	public int AbstractionNum;  //提炼数量
}

public class HeroIncreaseStarVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("maxStar")]
	public int MaxStar;  //主斗士历史最高总星数
	[JsonProperty("rate")]
	public int Rate;  //属性加成万分比
}

public class HeroInheritanceSlotVo{
	[JsonProperty("id")]
	public int Id;  //传承槽位id
	[JsonProperty("costA")]
	public List<int> CostA;  //解锁消耗A
	[JsonProperty("costB")]
	public List<int> CostB;  //解锁消耗B
}

public class HeroLevelVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //升级方案
	[JsonProperty("level")]
	public int Level;  //等级
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //升下一级消耗
	[JsonProperty("breakCost")]
	public List<List<int>> BreakCost;  //升下一级突破消耗
	[JsonProperty("attrAdd")]
	public int AttrAdd;  //属性加成
}

public class HeroQualityVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("name")]
	public string Name;  //品质名称
	[JsonProperty("icon")]
	public int Icon;  //品质图标
	[JsonProperty("dismissGain")]
	public List<List<int>> DismissGain;  //遣散获得
}

public class HeroSkillVo{
	[JsonProperty("id")]
	public int Id;  //唯一ID
	[JsonProperty("level")]
	public int Level;  //技能解锁等级
	[JsonProperty("star")]
	public int Star;  //技能解锁星级
}

public class HeroStarVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //升星方案
	[JsonProperty("star")]
	public int Star;  //星级
	[JsonProperty("levelLimit")]
	public int LevelLimit;  //等级限制
	[JsonProperty("itemCost")]
	public List<List<int>> ItemCost;  //升下一星道具消耗
	[JsonProperty("heroCost")]
	public List<int> HeroCost;  //升下一星圣斗士消耗
	[JsonProperty("attrAdd")]
	public int AttrAdd;  //属性加成
}

public class HeroTalentVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //方案id
	[JsonProperty("star")]
	public int Star;  //天赋解锁星级
	[JsonProperty("talentIds")]
	public List<int> TalentIds;  //天赋列表
}

public class HeroUniverseArousalVo{
	[JsonProperty("id")]
	public int Id;  //圣斗士id
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //觉醒消耗
	[JsonProperty("attrAdd")]
	public List<List<int>> AttrAdd;  //属性加成
	[JsonProperty("skill")]
	public int Skill;  //觉醒技能
}

public class HeroUniverseLevelVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //方案id
	[JsonProperty("level")]
	public int Level;  //小宇宙等级
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //升下一级消耗
	[JsonProperty("attrAdd")]
	public List<List<int>> AttrAdd;  //属性加成
	[JsonProperty("skill")]
	public int Skill;  //等级效果
}

public class HolyLandChapterVo{
	[JsonProperty("id")]
	public int Id;  //章节ID
	[JsonProperty("name")]
	public string Name;  //章节名称
	[JsonProperty("passStar")]
	public int PassStar;  //开启下一章所需星数
	[JsonProperty("star1")]
	public int Star1;  //1档奖励星数
	[JsonProperty("starReward1")]
	public List<List<int>> StarReward1;  //1档奖励
	[JsonProperty("star2")]
	public int Star2;  //2档奖励星数
	[JsonProperty("starReward2")]
	public List<List<int>> StarReward2;  //2档奖励
	[JsonProperty("star3")]
	public int Star3;  //3档奖励星数
	[JsonProperty("starReward3")]
	public List<List<int>> StarReward3;  //3档奖励
	[JsonProperty("sweepStar1")]
	public int SweepStar1;  //1档扫荡星数要求
	[JsonProperty("sweepReward1")]
	public List<List<int>> SweepReward1;  //1档扫荡奖励
	[JsonProperty("sweepStar2")]
	public int SweepStar2;  //2档扫荡星数要求
	[JsonProperty("sweepReward2")]
	public List<List<int>> SweepReward2;  //2档扫荡奖励
	[JsonProperty("sweepStar3")]
	public int SweepStar3;  //3档扫荡星数要求
	[JsonProperty("sweepReward3")]
	public List<List<int>> SweepReward3;  //3档扫荡奖励
}

public class HolyLandLevelVo{
	[JsonProperty("id")]
	public int Id;  //关卡ID
	[JsonProperty("fightId")]
	public int FightId;  //战斗ID
	[JsonProperty("rating")]
	public List<int> Rating;  //通关条件
	[JsonProperty("firstReward")]
	public List<List<int>> FirstReward;  //首通奖励
	[JsonProperty("power")]
	public int Power;  //推荐战力
}

public class LikeContentVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("content")]
	public string Content;  //文本
}

public class LikeGiftVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("plan")]
	public int Plan;  //回礼方案
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //等级奖励
}

public class LikeHeroLevelVo{
	[JsonProperty("level")]
	public int Level;  //好感度等级
	[JsonProperty("desc")]
	public string Desc;  //描述
	[JsonProperty("exp")]
	public int Exp;  //升到下一等级所需经验
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //等级奖励
	[JsonProperty("giftPlan")]
	public int GiftPlan;  //回礼方案
	[JsonProperty("giftCount")]
	public int GiftCount;  //每日回礼次数
	[JsonProperty("giftRate")]
	public int GiftRate;  //回礼概率
}

public class LikeHeroRewardVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("heroId")]
	public int HeroId;  //圣斗士id
	[JsonProperty("level")]
	public int Level;  //好感度等级
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //等级奖励
}

public class LikeItemVo{
	[JsonProperty("id")]
	public int Id;  //物品id
	[JsonProperty("exp")]
	public int Exp;  //好感经验
	[JsonProperty("count")]
	public List<int> Count;  //赠送次数
	[JsonProperty("attrAdd")]
	public List<List<int>> AttrAdd;  //属性加成
}

public class LikeLevelVo{
	[JsonProperty("level")]
	public int Level;  //好感度等级
	[JsonProperty("exp")]
	public int Exp;  //升到下一等级所需经验
	[JsonProperty("baseAttrAdd")]
	public List<List<int>> BaseAttrAdd;  //基础属性加成
	[JsonProperty("specialAttrAdd")]
	public List<List<int>> SpecialAttrAdd;  //特殊属性加成
	[JsonProperty("specialShow")]
	public int SpecialShow;  //展示属性加成
}

public class LikeStoryVo{
	[JsonProperty("id")]
	public int Id;  //唯一id
	[JsonProperty("heroId")]
	public int HeroId;  //圣斗士id
	[JsonProperty("level")]
	public int Level;  //好感度等级
	[JsonProperty("content")]
	public string Content;  //故事内容
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //等级奖励
}

public class RankVo{
	[JsonProperty("id")]
	public int Id;  //排行榜id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("style")]
	public int Style;  //样式
	[JsonProperty("rankTitle")]
	public string RankTitle;  //标题图片资源
	[JsonProperty("rankIcon")]
	public string RankIcon;  //排行图标类型资源
	[JsonProperty("rankBg")]
	public string RankBg;  //背景
	[JsonProperty("icon")]
	public string Icon;  //详情页图标
	[JsonProperty("scoretag")]
	public string Scoretag;  //榜首进度标题
	[JsonProperty("ranktag")]
	public string Ranktag;  //排行榜进度标题
	[JsonProperty("showNum")]
	public int ShowNum;  //展示容量
	[JsonProperty("capacity")]
	public int Capacity;  //实际容量
	[JsonProperty("minValue")]
	public int MinValue;  //上榜限制
	[JsonProperty("isCross")]
	public int IsCross;  //是否跨服
	[JsonProperty("isReverse")]
	public int IsReverse;  //是否反转
	[JsonProperty("system")]
	public int System;  //开启条件
	[JsonProperty("activity")]
	public int Activity;  //活动id
	[JsonProperty("calcType")]
	public List<List<int>> CalcType;  //结算类型
	[JsonProperty("showInPanel")]
	public int ShowInPanel;  //是否在玩法界面显示
	[JsonProperty("likeReset")]
	public int LikeReset;  //点赞重置类型
}

public class RankGroupVo{
	[JsonProperty("id")]
	public int Id;  //组id
	[JsonProperty("name")]
	public string Name;  //组名
	[JsonProperty("icon")]
	public string Icon;  //显示图标
	[JsonProperty("ranks")]
	public List<int> Ranks;  //排行榜列表
}

public class RankProgressVo{
	[JsonProperty("id")]
	public int Id;  //进度id
	[JsonProperty("rankId")]
	public int RankId;  //排行榜id
	[JsonProperty("desc")]
	public string Desc;  //组名
	[JsonProperty("value")]
	public int Value;  //进度目标值
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //进度奖励
}

public class RankRewardVo{
	[JsonProperty("id")]
	public int Id;  //奖励id
	[JsonProperty("group")]
	public int Group;  //组id
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("rankRange")]
	public List<int> RankRange;  //排名范围
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
}

public class StarMapVo{
	[JsonProperty("id")]
	public int Id;  //星座id
	[JsonProperty("name")]
	public string Name;  //星座名称
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //星座奖励
}

public class StarMapTaskVo{
	[JsonProperty("id")]
	public int Id;  //任务id
	[JsonProperty("name")]
	public string Name;  //任务名称
	[JsonProperty("note")]
	public string Note;  //任务描述
	[JsonProperty("starMapId")]
	public int StarMapId;  //星座id
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //任务奖励
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
}

public class TaskDailyVo{
	[JsonProperty("id")]
	public int Id;  //任务id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("takeLimit")]
	public int TakeLimit;  //接取限制
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //奖励
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //领取消耗
	[JsonProperty("isActive")]
	public int IsActive;  //是否是活跃任务
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
}

public class TaskGuessVo{
	[JsonProperty("id")]
	public int Id;  //任务id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("tab")]
	public int Tab;  //分类
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //奖励
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
}

public class TaskTypeVo{
	[JsonProperty("id")]
	public int Id;  //条件类型
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("judge")]
	public int Judge;  //判断方式
	[JsonProperty("valueType")]
	public int ValueType;  //值计算方式
	[JsonProperty("paramType")]
	public int ParamType;  //参数筛选类型
	[JsonProperty("memo")]
	public string Memo;  //备注
}

public class TaskWeeklyVo{
	[JsonProperty("id")]
	public int Id;  //任务id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("takeLimit")]
	public int TakeLimit;  //接取限制
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //奖励
	[JsonProperty("cost")]
	public List<List<int>> Cost;  //领取消耗
	[JsonProperty("isActive")]
	public int IsActive;  //是否是活跃任务
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
}

public class TravelLevelVo{
	[JsonProperty("id")]
	public int Id;  //等级id
	[JsonProperty("note")]
	public string Note;  //等级描述
	[JsonProperty("qualityWeights")]
	public List<int> QualityWeights;  //委托任务品质权重
	[JsonProperty("effect")]
	public string Effect;  //委托等级增益效果
}

public class TravelLevelTaskVo{
	[JsonProperty("id")]
	public int Id;  //委托等级任务id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("desc")]
	public string Desc;  //说明
	[JsonProperty("type")]
	public int Type;  //任务类型
	[JsonProperty("param")]
	public List<int> Param;  //参数值
	[JsonProperty("value")]
	public int Value;  //目标值
	[JsonProperty("jump")]
	public int Jump;  //跳转链接
	[JsonProperty("level")]
	public int Level;  //委托等级id
	[JsonProperty("rewards")]
	public List<List<int>> Rewards;  //奖励
}

public class TravelTaskVo{
	[JsonProperty("id")]
	public int Id;  //委托任务id
	[JsonProperty("quality")]
	public int Quality;  //任务品质
	[JsonProperty("weights")]
	public int Weights;  //品质权重
	[JsonProperty("general2")]
	public List<int> General2;  //派出斗士阵营要求
	[JsonProperty("general1")]
	public List<int> General1;  //派出斗士星级要求
	[JsonProperty("times")]
	public int Times;  //执行时间（秒）
	[JsonProperty("reward")]
	public List<int> Reward;  //奖励
	[JsonProperty("needItem")]
	public List<int> NeedItem;  //接取消耗
	[JsonProperty("heroNum")]
	public int HeroNum;  //派遣斗士数量
}

public class TrialDegreeVo{
	[JsonProperty("id")]
	public int Id;  //阶段id
	[JsonProperty("typeId")]
	public int TypeId;  //试炼类型id
	[JsonProperty("level")]
	public int Level;  //通关层数
	[JsonProperty("degreeReward")]
	public List<List<int>> DegreeReward;  //阶段奖励
	[JsonProperty("dailyReward")]
	public List<List<int>> DailyReward;  //每日扫荡奖励
}

public class TrialLevelVo{
	[JsonProperty("id")]
	public int Id;  //关卡id
	[JsonProperty("level")]
	public int Level;  //层数
	[JsonProperty("typeId")]
	public int TypeId;  //试炼分类id
	[JsonProperty("name")]
	public string Name;  //关卡名称
	[JsonProperty("power")]
	public int Power;  //推荐战力
	[JsonProperty("fightId")]
	public int FightId;  //战斗id
	[JsonProperty("reward")]
	public List<List<int>> Reward;  //通关奖励
}

public class TrialTypeVo{
	[JsonProperty("id")]
	public int Id;  //试炼分类id
	[JsonProperty("name")]
	public string Name;  //名称
	[JsonProperty("funcId")]
	public int FuncId;  //功能开启ID
	[JsonProperty("openTime")]
	public List<int> OpenTime;  //开放时间
	[JsonProperty("camp")]
	public List<int> Camp;  //英雄阵营id
	[JsonProperty("openSweep")]
	public int OpenSweep;  //是否开启扫荡
	[JsonProperty("freeSweepCnt")]
	public int FreeSweepCnt;  //每日免费扫荡次数
	[JsonProperty("paySweepCnt")]
	public int PaySweepCnt;  //每日可购买扫荡次数
	[JsonProperty("paySweepCost")]
	public List<List<int>> PaySweepCost;  //购买消耗
}

public class VipVo{
	[JsonProperty("level")]
	public int Level;  //vip等级
	[JsonProperty("exp")]
	public int Exp;  //需要的vip经验
	[JsonProperty("privileges")]
	public List<List<int>> Privileges;  //特权列表
	[JsonProperty("giftRewards")]
	public List<List<int>> GiftRewards;  //特权礼包奖励
	[JsonProperty("giftCost")]
	public List<List<int>> GiftCost;  //特权礼包消耗
	[JsonProperty("originalPrice")]
	public List<List<int>> OriginalPrice;  //特权礼包原价
	[JsonProperty("dailyRewards")]
	public List<List<int>> DailyRewards;  //每日专属奖励
	[JsonProperty("luxuryGift")]
	public List<List<int>> LuxuryGift;  //豪华礼包奖励
	[JsonProperty("luxuryGiftCost")]
	public List<List<int>> LuxuryGiftCost;  //豪华礼包消耗
	[JsonProperty("exclusiveGiftPayId")]
	public int ExclusiveGiftPayId;  //专享礼包奖励payId
	[JsonProperty("iconType")]
	public int IconType;  //图标类型
	[JsonProperty("icon")]
	public string Icon;  //图标
}

public class VipPrivilegesTextVo{
	[JsonProperty("id")]
	public int Id;  //特权类型id
	[JsonProperty("privilegesText")]
	public string PrivilegesText;  //需要的vip经验
}



public class ConfigConst{
	public static string Abstraction = "abstraction";
	public static string Chat = "chat";
	public static string ChatMsg = "chat_msg";
	public static string CurrencySystem = "currency_system";
	public static string Experience = "experience";
	public static string GuessLevel = "guess_level";
	public static string Item = "item";
	public static string Jump = "jump";
	public static string Location = "location";
	public static string Mail = "mail";
	public static string Misc = "misc";
	public static string OnHookReward = "on_hook_reward";
	public static string Pay = "pay";
	public static string Playername = "playername";
	public static string RedDot = "red_dot";
	public static string Result = "result";
	public static string RoleLevel = "role_level";
	public static string SensitiveWord = "sensitive_word";
	public static string Store = "store";
	public static string StoreGroup = "store_group";
	public static string StoreProduct = "store_product";
	public static string StoreQuickBuy = "store_quick_buy";
	public static string Treasure = "treasure";
	public static string Units = "units";
	public static string ArenaLevel = "arena_level";
	public static string ArenaTimesReward = "arena_times_reward";
	public static string Attr = "attr";
	public static string Buff = "buff";
	public static string Fight = "fight";
	public static string FightRating = "fight_rating";
	public static string FightType = "fight_type";
	public static string Formation = "formation";
	public static string Formula = "formula";
	public static string Monster = "monster";
	public static string MonsterGroup = "monster_group";
	public static string MonsterTemplate = "monster_template";
	public static string Robot = "robot";
	public static string RobotLineup = "robot_lineup";
	public static string Skill = "skill";
	public static string SkillEffect = "skill_effect";
	public static string TestFight = "test_fight";
	public static string FightDaily = "fight_daily";
	public static string FightDailyType = "fight_daily_type";
	public static string Func = "func";
	public static string FuncCondition = "func_condition";
	public static string FuncTask = "func_task";
	public static string Hero = "hero";
	public static string HeroAppearance = "hero_appearance";
	public static string HeroBack = "hero_back";
	public static string HeroBag = "hero_bag";
	public static string HeroBroken = "hero_broken";
	public static string HeroCost = "hero_cost";
	public static string HeroDegree = "hero_degree";
	public static string HeroIncreaseLevel = "hero_increase_level";
	public static string HeroIncreaseStar = "hero_increase_star";
	public static string HeroInheritanceSlot = "hero_inheritance_slot";
	public static string HeroLevel = "hero_level";
	public static string HeroQuality = "hero_quality";
	public static string HeroSkill = "hero_skill";
	public static string HeroStar = "hero_star";
	public static string HeroTalent = "hero_talent";
	public static string HeroUniverseArousal = "hero_universe_arousal";
	public static string HeroUniverseLevel = "hero_universe_level";
	public static string HolyLandChapter = "holy_land_chapter";
	public static string HolyLandLevel = "holy_land_level";
	public static string LikeContent = "like_content";
	public static string LikeGift = "like_gift";
	public static string LikeHeroLevel = "like_hero_level";
	public static string LikeHeroReward = "like_hero_reward";
	public static string LikeItem = "like_item";
	public static string LikeLevel = "like_level";
	public static string LikeStory = "like_story";
	public static string Rank = "rank";
	public static string RankGroup = "rank_group";
	public static string RankProgress = "rank_progress";
	public static string RankReward = "rank_reward";
	public static string StarMap = "star_map";
	public static string StarMapTask = "star_map_task";
	public static string TaskDaily = "task_daily";
	public static string TaskGuess = "task_guess";
	public static string TaskType = "task_type";
	public static string TaskWeekly = "task_weekly";
	public static string TravelLevel = "travel_level";
	public static string TravelLevelTask = "travel_level_task";
	public static string TravelTask = "travel_task";
	public static string TrialDegree = "trial_degree";
	public static string TrialLevel = "trial_level";
	public static string TrialType = "trial_type";
	public static string Vip = "vip";
	public static string VipPrivilegesText = "vip_privileges_text";
	public static List<string> ConfigList = new List<string>()
	{
		"abstraction",
		"chat",
		"chat_msg",
		"currency_system",
		"experience",
		"guess_level",
		"item",
		"jump",
		"location",
		"mail",
		"misc",
		"on_hook_reward",
		"pay",
		"playername",
		"red_dot",
		"result",
		"role_level",
		"sensitive_word",
		"store",
		"store_group",
		"store_product",
		"store_quick_buy",
		"treasure",
		"units",
		"arena_level",
		"arena_times_reward",
		"attr",
		"buff",
		"fight",
		"fight_rating",
		"fight_type",
		"formation",
		"formula",
		"monster",
		"monster_group",
		"monster_template",
		"robot",
		"robot_lineup",
		"skill",
		"skill_effect",
		"test_fight",
		"fight_daily",
		"fight_daily_type",
		"func",
		"func_condition",
		"func_task",
		"hero",
		"hero_appearance",
		"hero_back",
		"hero_bag",
		"hero_broken",
		"hero_cost",
		"hero_degree",
		"hero_increase_level",
		"hero_increase_star",
		"hero_inheritance_slot",
		"hero_level",
		"hero_quality",
		"hero_skill",
		"hero_star",
		"hero_talent",
		"hero_universe_arousal",
		"hero_universe_level",
		"holy_land_chapter",
		"holy_land_level",
		"like_content",
		"like_gift",
		"like_hero_level",
		"like_hero_reward",
		"like_item",
		"like_level",
		"like_story",
		"rank",
		"rank_group",
		"rank_progress",
		"rank_reward",
		"star_map",
		"star_map_task",
		"task_daily",
		"task_guess",
		"task_type",
		"task_weekly",
		"travel_level",
		"travel_level_task",
		"travel_task",
		"trial_degree",
		"trial_level",
		"trial_type",
		"vip",
		"vip_privileges_text",
	};
}
