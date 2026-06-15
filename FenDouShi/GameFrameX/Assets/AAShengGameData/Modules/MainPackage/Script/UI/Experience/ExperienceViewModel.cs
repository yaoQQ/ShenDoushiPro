

using common;
using msg.experience;
using System.Collections.Generic;


//游历Data数据转换
[System.Serializable]
public class ExperienceDataContent
{
    public int maxPassed;
    public long time; // 时间戳，注意单位可能是毫秒
}
public class ExperienceViewModel : BaseModel
{
    // 存储经验信息列表
    private List<ExperienceInfo> _experienceInfoList = new List<ExperienceInfo>();

    // 初始化数据
    protected override void onInit()
    {
        // 初始化数据结构
        _experienceInfoList = new List<ExperienceInfo>();
    }

    // 事件监听
    protected override void onEventListener()
    {
        // 可以在这里添加事件监听
    }

    /// <summary>
    /// 设置经验信息列表
    /// </summary>
    /// <param name="experienceInfoList">经验信息列表</param>
    public void SetExperienceInfoList(List<ExperienceInfo> experienceInfoList)
    {
        if (experienceInfoList == null)
            return;

        _experienceInfoList.Clear();
        _experienceInfoList.AddRange(experienceInfoList);

        // 分发数据更新事件
        EventManager.Instance.Dispatch(EEventType.EventExperienceInfoUpdate, _experienceInfoList);
    }
    /// <summary>
    /// 根据配置获取ExperienceInfo
    /// </summary>
    /// <param name="id">经验ID</param>
    /// <returns>经验信息对象</returns>
    public ExperienceInfo GetExperienceInfoByConfig(ExperienceVo experienceVo)
    {
       for(int i = 0; i < _experienceInfoList.Count; i++)
        {
            if (_experienceInfoList[i].Id == experienceVo.Id)
            {
                return _experienceInfoList[i];
            }
        }
       return null;
    }

    /// <summary>
    /// 获取经验信息列表
    /// </summary>
    /// <returns>经验信息列表</returns>
    public List<ExperienceInfo> GetExperienceInfoList()
    {
        return new List<ExperienceInfo>(_experienceInfoList);
    }

    /// <summary>
    /// 根据ID获取经验信息
    /// </summary>
    /// <param name="id">经验ID</param>
    /// <returns>经验信息对象</returns>
    public ExperienceInfo GetExperienceInfoById(int id)
    {
        return _experienceInfoList.Find(info => info.Id == id);
    }

    /// <summary>
    /// 清理数据
    /// </summary>
    public  void onClear()
    {;
        _experienceInfoList.Clear();
    }
}