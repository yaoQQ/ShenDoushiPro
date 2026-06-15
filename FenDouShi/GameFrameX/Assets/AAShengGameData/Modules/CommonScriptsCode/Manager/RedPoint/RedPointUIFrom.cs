using FairyGUI;

// 红点UI是动态创建的还是绑定的,如果是动态创建的则要回收，如果是绑定的则要解绑
public abstract class RedPointUIBase
{
    public ushort id;
    public int redPointType;

    public void Init(int type)
    {
        id = RedPointManager.Instance.idCounter.GetNewId();
        redPointType = type;
    }

    public virtual void AddRedPointCom() { }

    public virtual void SetState(bool state) { }

    public void Deregister()
    {
        id = ushort.MinValue;
        redPointType = 0;
        OnDeregister();
    }

    public abstract void OnDeregister();
}

public class RedPointUI_Create : RedPointUIBase
{
    public GComponent parent;
    public GComponent redPointCom;
    public RedPointPosition position;

    public override void AddRedPointCom()
    {
        RecycleRedPointCom();

        RedPointManager.Instance.pool.Get(x =>
        {
            RecycleRedPointCom();

            redPointCom = x;
            parent.AddChild(redPointCom);

            redPointCom.Align(position);
            redPointCom.visible = RedPointManager.Instance.GetState(redPointType);
        });
    }

    public override void SetState(bool state)
    {
        if (redPointCom != null)
        {
            redPointCom.visible = state;
        }
    }

    public override void OnDeregister()
    {
        parent = null;
        RecycleRedPointCom();

        this.RecycleToPool();
    }

    void RecycleRedPointCom()
    {
        if (redPointCom != null)
        {
            //RedPointManager.Instance.pool.Recycle(redPointCom);
            redPointCom = null;
        }
    }
}

public class RedPointUI_Attach : RedPointUIBase
{
    public GComponent attachCom;

    public override void AddRedPointCom()
    {
        if (attachCom != null)
        {
            attachCom.visible = RedPointManager.Instance.GetState(redPointType);
        }
    }

    public override void SetState(bool state)
    {
        if (attachCom != null)
        {
            attachCom.visible = state;
        }
    }

    public override void OnDeregister()
    {
        attachCom.visible = false;
        attachCom = null;

        this.RecycleToPool();
    }
}