using System;
using System.Collections.Generic;
using System.Text;

public class RedPointManager : Singleton<RedPointManager>
{
    public Dictionary<int, RedPointData> datas = new(); // 管理数据
    public Dictionary<int, RedPointUI> uis = new();     // 管理ui
    public RedPointPool pool = new RedPointPool();
    bool init;
    bool initExcel;

    public IdCounter idCounter = new();

    public void Dispose()
    {
        datas.Clear();
        uis.Clear();
        pool = new();
        idCounter = new();
        init = false;
    }

    public void Init()
    {
        if (!init)
        {
            init = true;
            InitGraph();
        }
        else
        {
            foreach (var i in uis)
            {
                i.Value.RemoveAllUICom();
            }
            foreach (var i in datas)
            {
                SetState(i.Key, false);
            }
        }
    }

    void InitGraph()
    {
        RedPointGraph graph = new RedPointGraph();
        foreach (var i in graph.relations)
        {
            RedPointData parent = GetOrCreateNode((int)i.self);
            if (i.children != null)
            {
                foreach (var childType in i.children)
                {
                    RedPointData child = GetOrCreateNode((int)childType);
                    parent.AddChild(child);
                }
            }
        }
    }

    public void InitExcel()
    {
        if (!initExcel)
        {
            var dic = ConfigMgr.Instance.GetConfig<RedDotVo>();
            if (dic != null)
            {
                initExcel = true;
                foreach (var i in dic.Values)
                {
                    if (i.ParentId == 0)
                    {
                        CreateNode(i.Id);
                    }
                    else
                    {
                        CreateNode(i.ParentId, i.Id);
                    }
                }
            }
        }
    }

    // 创建红点数据
    public void CreateNode(int redPointType)
    {
        GetOrCreateNode(redPointType);
    }

    public void CreateNode(int parentType, int childType)
    {
        RedPointData parent = GetOrCreateNode(parentType);
        RedPointData child = GetOrCreateNode(childType);
        parent.AddChild(child);
    }

    RedPointData GetOrCreateNode(ERedPointType redPointType) => GetOrCreateNode((int)redPointType);

    RedPointData GetOrCreateNode(int redPointType)
    {
        if (!datas.TryGetValue(redPointType, out var data))
        {
            data = new RedPointData(redPointType);
            datas.Add(redPointType, data);
        }
        return data;
    }

    // 注册
    public void Register(int redPointType, RedPointUIBase redPointComData)
    {
        if (!datas.TryGetValue(redPointType, out var data))
        {
            Logger.PrintError($"[红点]红点类型未注册到红点系统:{redPointType}");
            return;
        }

        // 添加组件
        if (!uis.TryGetValue(redPointType, out var uiComData))
        {
            uiComData = ClassPoolManger.Instance.Get<RedPointUI>();
            uiComData.redPointType = redPointType;
            uis.Add(redPointType, uiComData);
        }
        uiComData.Register(redPointComData);
    }

    // 注销
    public void Deregister(RedPointUIBase uiComData)
    {
        if (uiComData == null) return;

        if (!datas.TryGetValue(uiComData.redPointType, out RedPointData data))
        {
            Logger.PrintWarning($"[红点]红点类型未注册到红点系统:{uiComData.redPointType}");
            return;
        }
        if (!uis.TryGetValue(uiComData.redPointType, out var redPointUI))
        {
            Logger.PrintWarning($"[红点]红点类型未注册到红点组件系统:{uiComData.redPointType}");
            return;
        }
        redPointUI.Deregister(uiComData);
    }

    // 设置红点状态
    public void SetState(ERedPointType redPointType, Func<bool> func) => SetState(redPointType, func());

    public void SetState(ERedPointType redPointType, bool state) => SetState((int)redPointType, state);

    public void SetState(int redPointType, bool state)
    {
        if (!datas.TryGetValue(redPointType, out var data))
        {
            //Logger.PrintWarning($"[红点]红点类型未注册到红点系统:{redPointType}");
            data = GetOrCreateNode(redPointType);
            //return;
        }

        data.SetState(state);
    }

    public bool GetState(int redPointType)
    {
        return datas.TryGetValue(redPointType, out var data) && data.allState;
    }

    public void SetUIState(int redPointType, bool state)
    {
        if (uis.TryGetValue(redPointType, out var uiComData))
        {
            try
            {
                uiComData.SetState(state);
            }
            catch (Exception e)
            {
                Logger.PrintError($"[红点]红点出错:{e}");
            }
        }
    }


    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void Log()
    {
        StringBuilder sb = new StringBuilder("[红点]红点系统Log:\n");
        foreach (var i in datas)
        {
            sb.AppendLine($"{i.Value.redPointType} , all:{i.Value.allState} , self:{i.Value.selfState} , children:{i.Value.childrenState}");
            if (i.Value.children != null)
            {
                foreach (var j in i.Value.children.Values)
                {
                    sb.AppendLine($"  {j.redPointType} , all:{j.allState} , self:{j.selfState} , children:{j.childrenState}");
                }
            }
            sb.AppendLine();
        }
        Logger.PrintLog(sb.ToString());
    }
}

public struct RedPointPosition
{
    public int redPointType;
    public ERedPointAlignment align;
    public float offsetX;
    public float offsetY;

    public RedPointPosition(ERedPointType redPointType, ERedPointAlignment align = ERedPointAlignment.RightTop, float offsetX = 0, float offsetY = 0)
        : this((int)redPointType, align, offsetX, offsetY)
    {

    }

    public RedPointPosition(int redPointType, ERedPointAlignment align = ERedPointAlignment.RightTop, float offsetX = 0, float offsetY = 0)
    {
        this.redPointType = redPointType;
        this.align = align;
        this.offsetX = offsetX;
        this.offsetY = offsetY;
    }
}