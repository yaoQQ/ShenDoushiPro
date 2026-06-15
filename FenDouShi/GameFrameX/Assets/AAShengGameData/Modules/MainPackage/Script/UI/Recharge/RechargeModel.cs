

using System.Collections.Generic;
using System.Linq;
using common;
using msg.pay;

public class RechargeModel : BaseModel
{
    /// <summary>
    /// 支付信息
    /// </summary>
    private PayInfoResp mPayInfo;
    //首次充值id
    private int[] mFristPayIds;

    // 初始化调用
    protected override void onInit()
    {

    }

    /// <summary>
    /// 设置支付信息
    /// </summary>
    public void SetPayInfo(PayInfoResp payInfo)
    {
        mPayInfo = payInfo;
        Dispatch(EEventType.EventRecharegeInfoUpdate);
    }

    /// <summary>
    /// 获取支付信息
    /// </summary>
    public PayInfoResp GetPayInfo()
    {
        return mPayInfo;
    }

    /// <summary>
    /// 获取限购信息
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, PayLimit> GetPayLimitsInfo()
    {
        var result = new Dictionary<int, PayLimit>();
        if (mPayInfo != null)
        {
            mPayInfo.Limits.ForEach(element =>
            {
                result[element.payId] = element;
            });
        }
        return result;
    }

    /// <summary>
    /// 获取限购数量
    /// </summary>
    /// <param name="payId"></param>
    /// <returns></returns>
    public int GetBuyCount(int payId)
    {
        var count = 0;
        GetPayLimitsInfo().TryGetValue(payId, out PayLimit limit);
        if (limit != null)
        {
            count = limit.Count;
        }
        return count;
    }

    /// <summary>
    /// 设置直充信息
    /// </summary>
    public void SetDirectPayInfo(int[] firstIds)
    {
        mFristPayIds = firstIds;
        Dispatch(EEventType.EventRecharegeInfoUpdate);
    }

    /// <summary>
    /// 判断是否首充
    /// </summary>
    /// <param name="payId"></param>
    /// <returns></returns>
    public bool IsFristPay(int payId)
    {
        if (mFristPayIds != null && mFristPayIds.Length > 0)
        {
            return !mFristPayIds.Contains(payId);
        }

        return true;
    }
}