using UnityEngine;

public static class LayerDefine
{
    public static int UI { get; private set; }

    /// <summary>
    /// 展示模型
    /// </summary>
    public static int Show { get; private set; }


    public static int Hide { get; private set; }



    static LayerDefine()
    {
        UI = LayerMask.NameToLayer("UI");
        Hide = LayerMask.NameToLayer("Hide");
        Show = LayerMask.NameToLayer("Show");
    }
}
