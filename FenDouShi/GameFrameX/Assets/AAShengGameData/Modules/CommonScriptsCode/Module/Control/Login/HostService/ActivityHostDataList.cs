// 最外层响应
using System.Collections.Generic;

public class ActivityHostDataList
{
    public NoticeDataList Data;
    public int Status;
    public string Msg;

    // 包含 Notice 的数据对象
    public class NoticeDataList
    {
        public List<Notice> notice_list;
    }

    // 公告具体内容
    public class Notice
    {
        public int id;
        public string title;
        public int priority;
        public int app_id;
        public int is_show;
    }

}