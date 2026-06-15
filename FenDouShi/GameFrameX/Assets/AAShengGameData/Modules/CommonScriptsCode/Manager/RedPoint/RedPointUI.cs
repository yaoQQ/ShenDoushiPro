using System.Collections.Generic;

public class RedPointUI
{
    public int redPointType;
    public Dictionary<ushort, RedPointUIBase> uiComs;

    public bool HasRedPoint(ushort uiFromId)
    {
        return uiComs != null && uiComs.TryGetValue(uiFromId, out var redPoint) && redPoint != null;
    }

    public void Register(RedPointUIBase redPointCom)
    {
        uiComs ??= uiComs.GetFromPool();
        uiComs.Add(redPointCom.id, redPointCom);
        redPointCom.AddRedPointCom();
    }

    public void Deregister(RedPointUIBase redPointCom)
    {
        if (redPointCom == null) return;
        if (uiComs == null) return;
        if (!uiComs.Remove(redPointCom.id, out var redPoint)) return;
        if (uiComs.Count == 0)
        {
            uiComs.RecycleToPool();
            uiComs = null;
        }

        // ªÿ ’µΩ≥ÿ
        redPointCom.Deregister();
    }

    public void RemoveAllUICom()
    {
        if (uiComs != null)
        {
            foreach (var i in uiComs.Values)
            {
                i.Deregister();
            }
            uiComs.Clear();
            uiComs.RecycleToPool();
            uiComs = null;
        }
    }

    public void SetState(bool state)
    {
        if (uiComs != null)
        {
            foreach (var i in uiComs.Values)
            {
                i.SetState(state);
            }
        }
    }
}