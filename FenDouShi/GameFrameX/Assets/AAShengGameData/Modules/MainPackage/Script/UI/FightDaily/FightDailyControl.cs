

using msg.fightdaily;
using System.Collections.Generic;


[ControlAttribute]
public class FightDailyControl : BaseControl<FightDailyControl>
{
    public FightDailyModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Model = new FightDailyModel();
    }

    protected override void onLoginSuccess()
    {
        // 登录成功后请求每日副本数据
        ReqFightDailyInfoReq();
    }

    // 事件监听处理
    protected override void onEventListener()
    {
        //每日副本数据回调
        on<FightDailyInfoResp>((uint)Cmd.FightDailyInfoResp, FightDailyInfoResp);
        // 每日副本扫荡回调
        on<FightDailySweepResp>((uint)Cmd.FightDailySweepResp, FightDailySweepResp);

    }

    /// <summary>
    /// 请求每日副本数据
    /// </summary>
    public void ReqFightDailyInfoReq()
    {
        var req = new FightDailyInfoReq();
        SendNetMsg((uint)Cmd.FightDailyInfoReq, req);
    }

    /// <summary>
    /// 请求每日副本扫荡
    /// </summary>
    /// <param name="times">扫荡次数字典，key=类型 value=扫荡次数</param>
    /// <param name="isFree">是否是免费扫荡</param>
    public void ReqFightDailySweepReq(Dictionary<int, int> times, bool isFree)
    {
        var req = new FightDailySweepReq
        {
            isFree = isFree
        };
        
        // 添加扫荡次数数据
        foreach (var kvp in times)
        {
            req.Times[kvp.Key] = kvp.Value;
        }
        
        SendNetMsg((uint)Cmd.FightDailySweepReq, req);
    }
    
    ///------------------------服务返回----------------

    /// <summary>
    /// 每日副本数据回调
    /// </summary>
    /// <param name="resp"></param>
    private void FightDailyInfoResp(FightDailyInfoResp resp)
    {
        Logger.PrintDebug("每日副本数据回调 resp=" + resp);
        
        // 设置每日副本信息到模型
        if (resp.infoLists != null && resp.infoLists.Count > 0)
        {
            Model.SetFightDailyInfo(resp.infoLists);
            Logger.PrintDebug("每日副本数据发送 resp.infoLists=" + resp.infoLists);
            if (resp.infoLists != null)
            {
                foreach (var info in resp.infoLists)
                {
                    string objJson = Newtonsoft.Json.JsonConvert.SerializeObject(info);
                    Logger.PrintDebug("每日副本数据发送 info=" + objJson);
                    
                }
            }
            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventFightDailyInfoUpdate, resp.infoLists);
        }
    }

    /// <summary>
    /// 每日副本扫荡回调
    /// </summary>
    /// <param name="resp"></param>
    private void FightDailySweepResp(FightDailySweepResp resp)
    {
        Logger.PrintDebug("每日副本扫荡回调 resp=" + resp);
        // 更新扫荡次数
        if (resp.Times != null && resp.Times.Count > 0)
        {
            foreach (var kvp in resp.Times)
            {
                // 获取当前副本信息
                FightDaily daily = Model.GetFightDailyByType(kvp.Key);
                if (daily != null)
                {
                    // 更新扫荡次数
                    if (resp.isFree)
                    {
                        Model.UpdateSweepTimes(kvp.Key, kvp.Value, daily.buySweepTimes);
                    }
                    else
                    {
                        Model.UpdateSweepTimes(kvp.Key, daily.freeSweepTimes, kvp.Value);
                    }
                }
            }
            string objJson = Newtonsoft.Json.JsonConvert.SerializeObject(resp);
            Logger.PrintDebug("EventFightDailySweepUpdate objJson=" + objJson);
            // 分发事件通知UI更新
            EventManager.Instance.Dispatch(EEventType.EventFightDailySweepUpdate, resp);
        }
    }
    public FightDailyVo GetFightDailyByIndex(int index)
    {
       var dic= ConfigMgr.Instance.GetConfig<FightDailyVo>();
        foreach (var kvp in dic)
        {
            if(kvp.Value.Index== index)
            {
                return kvp.Value;
            }
            if(kvp.Value.Index> index)
            {
                return null;
            }
        }
        return null;
    }

    // 清理数据调用
    protected override void onClear()
    {
        // 清理相关数据
    }


}
