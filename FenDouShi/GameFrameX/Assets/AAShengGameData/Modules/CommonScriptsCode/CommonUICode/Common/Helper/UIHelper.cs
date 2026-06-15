using FairyGUI;
using System;
using System.Collections.Generic;
using System.Linq;

public static class UIHelper
{
    public static List<BaseItemData> TransformDatas<T>(List<T> itemDatas)
    {
        if (itemDatas is not { Count: > 0 })
        {
            return new List<BaseItemData>();
        }
        var mData = itemDatas.OfType<BaseItemData>().ToList();
        return mData.Count > 0 ? mData : itemDatas.Cast<BaseItemData>().ToList();
    }

    public static string GetFguiUrl(string package, string resName)
    {
        return Utility.Text.Format("{0}{1}/{2}", UIPackage.URL_PREFIX, package, resName);
    }

    /// <summary>
    /// package icon res
    /// </summary>
    /// <param name="package"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public static string GetIconUrl(string resName)
    {
        return GetFguiUrl(ItemDefine.IconPackage, resName);
    }

    /// <summary>
    /// package common res
    /// </summary>
    /// <param name="package"></param>
    /// <param name="resName"></param>
    /// <returns></returns>
    public static string GetCommonUrl(string resName)
    {
        return GetFguiUrl(ItemDefine.commonPackage, resName);
    }

    public static bool GetRandomBool()
    {
        return DataTableFrame.Utility.Random.GetRandom(0, 2) <= 0;
    }

    private static uint _id;

    /// <summary>
    /// New一个Id
    /// </summary>
    public static uint GetNewId => ++_id;


    /// <summary>
    /// 递归查找所有节点
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GObject FindNodeByName(GObject parent, string name)
    {
        if (parent == null)
        {
            return null;
        }
        if (parent.name == name)
        {
            return parent;
        }
        var component = parent as GComponent;
        if (parent is not GComponent) return null;
        for (int i = 0; i < component.numChildren; i++)
        {
            var child = component.GetChildAt(i);
            var foundNode = FindNodeByName(child, name);
            if (foundNode != null)
            {
                return foundNode;
            }
        }
        return null;
    }
}
