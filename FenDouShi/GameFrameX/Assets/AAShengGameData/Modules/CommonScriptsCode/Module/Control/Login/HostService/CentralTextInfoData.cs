public class TextInfoDataRespone
{
    public DataContent Data { get; set; }
    public int Status { get; set; }
    public string Msg { get; set; }
}
public class DataContent
{
    public object Content { get; set; } // 使用 object 类型兼容 null 值
}