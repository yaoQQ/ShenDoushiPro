using System.Collections.Generic;

public class RedPointData
{
    public int redPointType;

    public Dictionary<int, RedPointData> parents;
    public Dictionary<int, RedPointData> children;

    // 状态
    public bool allState;
    public bool selfState;
    bool needUpdateChildrenState;
    public bool childrenState;

    public RedPointData(ERedPointType redPointType) : this((int)redPointType) { }

    public RedPointData(int redPointType) => this.redPointType = redPointType;

    public void AddChild(RedPointData child)
    {
        if (CheckInParent(child.redPointType))
        {
            Logger.PrintError($"[红点]红点系统初始化出错,出现循环引用:父节点:{redPointType},子节点:{child.redPointType}");
        }
        else if (CheckInChild(child.redPointType))
        {
            Logger.PrintError($"[红点]红点系统初始化出错,父节点重复添加子节点:父节点:{redPointType},子节点:{child.redPointType}");
        }
        else
        {
            AddChildRelation(child);
        }
    }

    public bool CheckInParent(int redPointType) => parents != null && parents.ContainsKey(redPointType);

    public bool CheckInChild(int redPointType) => children != null && children.ContainsKey(redPointType);

    void AddChildRelation(RedPointData childData)
    {
        if (childData != null)
        {
            // 双向引用
            children ??= new();
            children.Add(childData.redPointType, childData);

            childData.parents ??= new();
            childData.parents.Add(redPointType, this);
        }
    }

    // 自身更新时
    public void SetState(bool show)
    {
        selfState = show;
        UpdateAllState();
    }

    // 孩子更新时
    void OnChildStateUpdate()
    {
        needUpdateChildrenState = true;
        UpdateAllState();
    }

    // 更新状态
    void UpdateAllState()
    {
        bool lastState = allState;

        // 更新自身状态
        allState = selfState;
        if (!allState)
        {
            if (needUpdateChildrenState)
            {
                needUpdateChildrenState = false;

                // 更新子状态
                childrenState = false;
                if (children != null)
                {
                    foreach (var child in children.Values)
                    {
                        childrenState |= child.allState;
                        if (childrenState)
                        {
                            break;
                        }
                    }
                }
                allState |= childrenState;
            }
        }

        if (lastState != allState)
        {
            // 向上传递
            if (parents != null)
            {
                foreach (var i in parents.Values)
                {
                    i.OnChildStateUpdate();
                }
            }

            // 更新自身UI状态
            RedPointManager.Instance.SetUIState(redPointType, allState);

            // 触发事件
            EventManager.Instance.Dispatch(EEventType.OnRedPointUpdate, redPointType, allState);
        }
    }
}