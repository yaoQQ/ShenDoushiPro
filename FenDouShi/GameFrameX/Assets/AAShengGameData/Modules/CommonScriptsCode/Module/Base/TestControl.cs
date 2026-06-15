


using UnityEngine;

[ControlAttribute]
public class TestControl : BaseControl<TestControl>
{

    public  TestModel Model { get; private set; }

    // 初始化成功调用
    protected override void onInit()
    {
        Logger.PrintDebug("=========================         TestControl onInit");
        Model = new TestModel();
    }


    // 事件监听处理
    protected override void onEventListener()
    {
    }


    // 清理数据调用
    protected override void onClear()
    {
    }

    protected override void onLoginSuccess()
    {
        Debug.Log("===================>>>>>>   TestControl  onLoginSuccess");
    }


}
