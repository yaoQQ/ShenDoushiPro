using System;
using System.Collections.Generic;

namespace GM
{
    public class GmDefine
    {
        public class GMTabItemData : ChildTabItemData
        {
            public eTabType eTabType;
            public string name;
            public List<ClientGM> clientGMs;
        }

        public enum eTabType
        {
            client,
            custom,
            api,
        }


        public class ClientGM : BaseItemData
        {
            public string name;

            public Action<string> action;
        }

        public static UIViewEnum GetViewEnum(string arg)
        {
            UIViewEnum viewEnum = UIViewEnum.None;
            if (Enum.TryParse<UIViewEnum>(arg, out var result))
            {
                viewEnum = result;
            }
            else if (int.TryParse(arg, out var result1))
            {
                viewEnum = (UIViewEnum)result1;
            }
            return viewEnum;
        }

        public static List<ClientGM> clientGMs = new List<ClientGM>() {

            new ClientGM() { name = "打开界面", action = (arg) => {
                UIViewManager.Instance.Show(GetViewEnum(arg));
            } },
            new ClientGM() { name = "关闭界面", action = (arg) => {
                UIViewManager.Instance.Hide(GetViewEnum(arg));
            } },
            new ClientGM() { name = "发送多个测试邮件", action = (arg) => {
                foreach(var i in MAIL_DIC.DIC)
                {
                    GmModule.OSendGm($"testMail {i.Key}");
                }
                foreach(var i in MAIL_DIC.DIC)
                {
                    GmModule.OSendGm($"testMail 2 33800302 100");
                }
            } },
            new ClientGM() { name = "打开邮件界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.MailView);
                UIViewManager.Instance.Hide(UIViewEnum.GmMainView);
            } },
            new ClientGM() { name = "打开背包界面", action = (arg) => {
            UIViewManager.Instance.Show(UIViewEnum.BagView);
            } },
            //new ClientGM() { name = "gm加道具[道具 数量]", action = (arg) => {
            //    var str= Utility.Text.Format("addItem {0}",arg);
            //    GmModule.OSendGm(str);
            //} },
            //   new ClientGM() { name = "gm扣除道具[道具 数量]", action = (arg) => {
            //    var str= Utility.Text.Format("decreaseItem {0}",arg);
            //    GmModule.OSendGm(str);
            //} },
             new ClientGM() { name = "请求背包数据", action = (arg) => {
                 BagControl.Instance.OnReqBagList((eBagType) arg.ToInt());
            }},
            new ClientGM() { name = "打开功能开启界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.SystemOpenView);
            } },
            new ClientGM() { name = "打开变强界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.StrongerMainView);
            } },

            new ClientGM() { name = "请求功能开启数据", action = (arg) => {
                SystemOpenControl.Instance.OnReqSystemInfo();
            } },
            new ClientGM() { name = "请求功能开启 任务数据", action = (arg) => {
                SystemOpenControl.Instance.OnReqTaskInfo();
            } },
            new ClientGM() { name = "发送系统通知", action = (arg) => {
                foreach(var i in CHAT_MSG_DIC.DIC.Keys)
                {
                    GmModule.OSendGm($"chatSendMessage {i}");
                }
            } },
            new ClientGM() { name = "打开聊天界面", action = (arg) => {
                // 开启全部功能
                UIViewManager.Instance.Show(UIViewEnum.ChatView);
            } },
            //new ClientGM() { name = "打开Tips界面", action = (arg) => {
              
            //    //if (UIViewManager.Instance.GetIsShowing(UIViewEnum.ItemTipsView))
            //    //{
            //        UIViewManager.Instance.Hide(UIViewEnum.ItemTipsView );
            //    //}
            //    TipsHelper.ShowTips(arg.ToInt());
            //} },
            //new() { name = "道具弹窗_小", action = (arg) => {

            //    var list = new List<CommonItemData>();
            //    var num = 10;
            //    var temp = arg.ToInt();
            //    num = temp>0 ?temp : num;
            //    for (int i = 0; i < num; i++) {
            //        list.Add(new CommonItemData(i));
            //    }
            //    var isOpen = UIViewManager.Instance.GetIsShowing(UIViewEnum.ItemGetRewardsSmallView);
            //    if (isOpen)
            //    {
            //        EventManager.Instance.Dispatch(EEventType.BagItemChange_Show, list);
            //    }
            //    else
            //    {
            //        UIViewManager.Instance.Show(UIViewEnum.ItemGetRewardsSmallView, list);
            //    }
            //} },
            //new() { name = "道具弹窗_大", action = (arg) => {

            //    var list = new List<CommonItemData>();
            //    var num = 10;
            //    var temp = arg.ToInt();
            //    num = temp>0 ?temp : num;
            //    for (int i = 0; i < num; i++) {
            //        list.Add(new CommonItemData(i));
            //    }
            //    var isOpen = UIViewManager.Instance.GetIsShowing(UIViewEnum.ItemGetRewardsView);
            //    if (isOpen)
            //    {
            //        UIViewManager.Instance.Hide(UIViewEnum.ItemGetRewardsView);
            //    }
            //    UIViewManager.Instance.Show(UIViewEnum.ItemGetRewardsView, list);
            //} },
            new ClientGM() { name = "打开升级界面", action = (arg) => {
              UIViewManager.Instance.Show(UIViewEnum.RoleLevelView, 11);
            } },
            new ClientGM() { name = "打开任务界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.TaskView);
            } },
            new ClientGM() { name = "请求每日任务列表", action = (arg) => {
                TaskControl.Instance.TaskListReq(msg.task.TaskModudle.Daily);
            } },
            new ClientGM() { name = "请求每周任务列表", action = (arg) => {
                TaskControl.Instance.TaskListReq(msg.task.TaskModudle.Weekly);
            } },
            new ClientGM() { name = "完成任务tips", action = (arg) => {
                foreach(var i in TaskControl.Instance.Model.GetTasks(msg.task.TaskModudle.Daily))
                    EventManager.Instance.Dispatch(EEventType.EventTaskFinish,i);
                foreach(var i in TaskControl.Instance.Model.GetTasks(msg.task.TaskModudle.Weekly))
                    EventManager.Instance.Dispatch(EEventType.EventTaskFinish,i);
            } },
            new ClientGM() { name = "好友", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.FriendView);
            } },
            new ClientGM() { name = "挂机信息请求", action = (arg) => {
                OnHookControl.Instance.OnHookGetInfoReq();
            } },
            new ClientGM() { name = "挂机加速请求", action = (arg) => {
                OnHookControl.Instance.OnHookSpeedUpReq();
            } },
            new ClientGM() { name = "挂机领取奖励请求", action = (arg) => {
                OnHookControl.Instance.OnHookGainRewardReq();
            } },
            new ClientGM() { name = "打开排行榜界面", action = (arg) => {

                UIViewManager.Instance.Show(UIViewEnum.RankView);
            } },
            new ClientGM() { name = "开启全部系统 没打脸图", action = (arg) => {

                SystemOpenControl.Instance.Model.ShieldPopView = true;
                GmModule.OSendGm(Utility.Text.Format("{0} {1}", "functionOpen", 0));
            } },
            new ClientGM() { name = "打开离线奖励界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.OnHookView_ShowReward);
            } },
            new ClientGM() { name = "挂机奖励", action = (arg) => {
                OnHookControl.Instance.OnHookGainRewardResp(new (){
                   dungeonId =202,
                   startTime = 1756438440000,
                   onHookTime = 100000,
                   beforeLevel=1,
                   beforeExp=0,
                   afterLevel=10,
                   afterExp=100,
                });
            } },
            new ClientGM() { name = "充值界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.RechargeView);
            } },
             new ClientGM() { name = "打开游历界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.ExperienceView);
            } },
            new ClientGM() { name = "打开贵族界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.VipView);
            } },
            new ClientGM() { name = "打开雅典娜试炼界面", action = (arg) => {
                UIViewManager.Instance.Show(UIViewEnum.AthenaTrialMainView);
            } },
};

        public static List<ClientGM> GMTest1 = new List<ClientGM>()
        {
            new ClientGM() { name = "日期str转换成时间戳", action = (arg) => {

               var seconds=  DateFormatUtil.GetSecondsByDateStr(arg);
                Logger.PrintError(seconds.ToString());
            } },
            new ClientGM() { name = "打印时间戳 秒", action = (arg) => {

                var seconds=  DateFormatUtil.GetDateTimeStr(arg.ToInt());
                Logger.PrintError(seconds.ToString());
            } },

            new ClientGM() { name = "打印时间戳 毫秒", action = (arg) => {

                var seconds=  DateFormatUtil.GetDateTimeStr2(arg.ToInt());
                Logger.PrintError(seconds.ToString());
            } },
            new ClientGM() { name = "今天星期几", action = (arg) => {
                var seconds=  TimeManager.GetChinaWeekDay(arg.ToInt());
                Logger.PrintError(seconds.ToString());
            } },
            new ClientGM() { name = "时间测试", action = (arg) => {

                var curTime = TimeManager.GetServerUnixTime();
                //var time2 = TimeManager.GetServerDateTime();
                var openServerDay = TimeManager.GetOpenServerDay();
                var serverOpenTime = LoginControl.Instance.Model?.LoginResp?.serverOpenTime ?? 0;
                Logger.PrintError(Utility.Text.Format(@"curTime=>{0} serverOpenTime=>{1} openServerDay=>{2} ",curTime,serverOpenTime,openServerDay));
            } },
            new ClientGM() { name = "雅典娜关卡攻略", action = (arg) => {
                AthenaTrialControl.Instance.ReqTrialLevelStrategy(arg.ToInt());
            } },
        };


        public static List<GMTabItemData> gmTabs = new List<GMTabItemData>() {

            new GMTabItemData(){ name = "客户端gm",eTabType = eTabType.client ,clientGMs= clientGMs},
            new GMTabItemData(){ name = "服务端gm",eTabType = eTabType.custom},
            new GMTabItemData(){ name = "Test1",eTabType = eTabType.client,clientGMs= GMTest1},
        };
    }
}




