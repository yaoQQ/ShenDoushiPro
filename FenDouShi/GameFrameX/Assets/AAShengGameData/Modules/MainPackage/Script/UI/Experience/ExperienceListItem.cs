//------------------------------------------------------------
//------------------------------------------------------------
// 此文件由工具自动生成
// 生成时间：__DATA_TABLE_CREATE_TIME__
//------------------------------------------------------------

using common;
using Experience;
using FairyGUI;
using GM;
using msg.experience;
using System.Collections.Generic;

public class ExperienceListItem : BaseRender
{
    private TableView<ItemRender> mItemRender;
    public new G_ExperienceListItem mRoot
    {
        get { return (G_ExperienceListItem)base.mRoot; }
    }

    public override string mPackageName => G_ExperienceListItem.PACKAGE_NAME;
    public override string mComponentName => G_ExperienceListItem.COMPONENT_NAME;

    /// <summary>
    /// //节点创建子
    /// </summary>
    protected override void OnCreate()
    {
        mItemRender = new TableView<ItemRender>(mRoot.itemList);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected override void InitEventLister()
    {
    }

    /// <summary>
    /// 刷新数据
    /// </summary>
    protected override void DataChanged()
    {

        var item = this.mRoot;
        ExperienceVo experienceVoConfig = this.mData as ExperienceVo;
        if (experienceVoConfig == null)
        {
            Logger.PrintError("experienceVoConfig is null, name = " + experienceVoConfig.Name);
            return;
        }

        if (experienceVoConfig.Type != 1)
        {
            item.levelTitle.text = "玩法未确认";
            item.LevelNum.text = "";
        }
        else//日常副本
        {
            item.levelTitle.text = "当前关卡";
            item.LevelNum.text = "";
            ExperienceInfo experInfo = ExperienceViewControl.Instance.Model.GetExperienceInfoByConfig(experienceVoConfig);
            if (!string.IsNullOrEmpty(experInfo.Data))
            {
                ExperienceDataContent experienceData = Newtonsoft.Json.JsonConvert.DeserializeObject<ExperienceDataContent>(experInfo.Data);
                Logger.PrintToJson(experienceData);
                item.LevelNum.text= experienceData.maxPassed.ToString();
               // item.timeInfo.text = DateFormatUtil.FormatToDayAndSecond((uint)(experienceData.time / 1000)).ToString()+"后重置";
                 var begainTime = TimeManager.GetServerDateTime();
                 var afterTime= TimeManager.GetDateTimeByUnixTime((uint)(experienceData.time / 1000));
                 item.timeInfo.text =  DateFormatUtil.GetLastTimeDesc(afterTime,begainTime)+"重置";
            }
        }
    
        // 设置历练图标
        GLoader imgTitle = item.imgTitle;
        if (imgTitle != null)
        {
            imgTitle.url = UIHelper.GetFguiUrl(mPackageName, experienceVoConfig.Icon);
        }

        // 设置历练背景
        GLoader imgBg = item.imgBg;
        if (imgBg != null)
        {
            imgBg.url = UIHelper.GetFguiUrl(mPackageName, experienceVoConfig.Bg);
        }

        //当前服务端无数据，需确认玩法暂时使用本地数据
        if (experienceVoConfig.Id == (int)ExperienceEnum.FightDaily)
        {
            item.InstanceLogo.visible = false;
        }
        else
        {
            item.InstanceLogo.visible = true;
        }


        // 设置奖励物品
        GList goodList = item.itemList;
        if (goodList != null)
        {
            // goodList.RemoveChildren();
            //  加载奖励物品
            var mLent = experienceVoConfig.Rewards.Count;
            var rewards = new List<CommonItemData>();
            for (int i = 0; i < mLent; i++)
            {
                var goodItem = experienceVoConfig.Rewards[i];
                rewards.Add(new CommonItemData(goodItem[0], goodItem[1]));
            }
            mItemRender.setDatas(rewards);
        }




        //// 设置提示信息
        //GTextField labTips1 = item.GetChild("labTips1").asTextField;
        //GTextField labTips2 = item.GetChild("labTips2").asTextField;
        //if (labTips1 != null && labTips2 != null)
        //{
        //    switch (experienceVo.Type)
        //    {
        //        case (int)eExperienceTypes.DailyCopy:
        //            labTips1.text = "重置时间：";
        //            labTips2.text = "00:00:00";
        //            break;
        //        case (int)eExperienceTypes.Tower:
        //            labTips1.text = "当前层数：";
        //            labTips2.text = "9999层";
        //            break;
        //        case (int)eExperienceTypes.Arena:
        //            labTips1.text = "赛季倒计时：";
        //            labTips2.text = "暂未开启";
        //            break;
        //    }
        //}
    }

}