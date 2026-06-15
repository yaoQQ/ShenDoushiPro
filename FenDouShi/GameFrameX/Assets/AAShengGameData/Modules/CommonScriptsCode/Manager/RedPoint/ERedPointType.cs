// 红点类型,有数字的红点表示红点表red_dot的id
public enum ERedPointType : int
{
    None = 0,

    // 主页
    MainHome,

    // 背包
    Bag,
    BagItem,
    BagEquipment,
    BagFragment,

    // 邮件
    Mail,
    Mail_Tab1,
    Mail_Tab2,

    //系统开启
    SystemOpen,

    //排行榜
    Rank = 115000,


    // 任务
    Task = 116000,
    Task_Daily = 116001,
    Task_Weekly = 116002,


    //雅典娜试炼
    AthenaTrial = 118000,
    AthenaTrial_Tab1 = 118001,
    AthenaTrial_Tab2 = 118002,
    AthenaTrial_Tab3 = 118003,
    AthenaTrial_Tab4 = 118004,
    AthenaTrial_Tab5 = 118005,
}

// 红点结构,类似于拓扑结构,节点可以重复,不可以形成回路
// 红点表red_dot里如果已经配置了父红点，这里不用重复配置
public class RedPointGraph
{
    public RedPointRelation[] relations = new RedPointRelation[]
    {
        new(ERedPointType.MainHome),
        new(ERedPointType.Bag, new[]{
            ERedPointType.BagItem,
            ERedPointType.BagFragment
        }),
        new(ERedPointType.BagItem, new[]{
            ERedPointType.BagEquipment
        }),
        new(ERedPointType.Bag, new[]{
            ERedPointType.BagEquipment
        }),
        new(ERedPointType.Mail, new[]{
            ERedPointType.Mail_Tab1,
            ERedPointType.Mail_Tab2
        }),
        new(ERedPointType.AthenaTrial, new[]{
            ERedPointType.AthenaTrial_Tab1,
            ERedPointType.AthenaTrial_Tab2,
            ERedPointType.AthenaTrial_Tab3,
            ERedPointType.AthenaTrial_Tab4,
            ERedPointType.AthenaTrial_Tab5,
        }),
    };
}

// 拓扑节点
public struct RedPointRelation
{
    public ERedPointType self;
    public ERedPointType[] children;

    public RedPointRelation(ERedPointType redPointType)
    {
        self = redPointType;
        children = null;
    }

    public RedPointRelation(ERedPointType selfType, ERedPointType[] childrenType)
    {
        self = selfType;
        children = childrenType;
    }
}