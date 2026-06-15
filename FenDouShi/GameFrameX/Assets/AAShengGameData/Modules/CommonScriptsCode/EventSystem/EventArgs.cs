public abstract class EventSysArgsBase : IClassPoolItem
{
    public int eventType;

    public void Init(int eventType)
    {
        this.eventType = eventType;
    }

    public virtual void OnRecycle()
    {
        eventType = -1;
    }
}

public class EventSysArgs : EventSysArgsBase { }

public class EventSysArgs<T1> : EventSysArgs
{
    public T1 args1;

    public void Init(int eventType, T1 args1)
    {
        Init(eventType);
        this.args1 = args1;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        args1 = default;
    }
}

public class EventSysArgs<T1, T2> : EventSysArgs<T1>
{
    public T2 args2;

    public EventSysArgs() { }

    public void Init(int eventType, T1 args1, T2 args2)
    {
        Init(eventType, args1);
        this.args2 = args2;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        args2 = default;
    }
}

public class EventSysArgs<T1, T2, T3> : EventSysArgs<T1, T2>
{
    public T3 args3;

    public void Init(int eventType, T1 args1, T2 args2, T3 args3)
    {
        Init(eventType, args1, args2);
        this.args3 = args3;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        args3 = default;
    }
}

public class EventSysArgs<T1, T2, T3, T4> : EventSysArgs<T1, T2, T3>
{
    public T4 args4;

    public void Init(int eventType, T1 args1, T2 args2, T3 args3, T4 args4)
    {
        Init(eventType, args1, args2, args3);
        this.args4 = args4;
    }

    public override void OnRecycle()
    {
        base.OnRecycle();
        args4 = default;
    }
}