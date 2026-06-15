// 最外层响应
public class ActivityHostData
{
    public NoticeData Data;
    public int Status;
    public string Msg;

    // 包含 Notice 的数据对象
    public class NoticeData
    {
        public Notice Notice;
    }

    // 公告具体内容
    public class Notice
    {
        public int Id;
        public string Title;
        public int Priority;
        public int AppId;
        public string Content;
    }
    public int IsShow;
}