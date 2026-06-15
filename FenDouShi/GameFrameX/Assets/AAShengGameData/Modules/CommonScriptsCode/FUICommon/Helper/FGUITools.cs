using FairyGUI;
using System.Diagnostics;
using System.Text;
using UnityEngine;

public class FGUIComponent : MonoBehaviour
{
    public GComponent gComponent;
}

/// <summary>
/// Extension methods for GComponent
/// </summary>
public static class GComponentExtensions
{
    public static T AddComponent<T>(this GComponent parent) where T : FGUIComponent
    {
       var component = parent.displayObject.gameObject.AddComponent<T>();
       component.gComponent = parent;
        return component;
    }

    public static T GetComponent<T>(this GComponent parent) where T : FGUIComponent
    {
        return parent.displayObject.gameObject.GetComponent<T>();
    }
    public static T TryGetComponent<T>(this GComponent parent) where T : FGUIComponent
    {
        var com = parent.displayObject.gameObject.GetComponent<T>();
        return com ?? parent.AddComponent<T>();
    }
}

public enum coinType
{
    coin,//金币
    silverCoin,//银币
    gold,//黄金
    diamond,//钻石

}
public static class FGUITools
{
    [Conditional("DEBUG")]
    public static void LogChildren(this GComponent gCom)
    {
        if (gCom != null)
        {
            StringBuilder sb = new StringBuilder($"{gCom.name}的所有子物体:");
            foreach (var i in gCom.GetChildren())
            {
                sb.AppendLine(i.name);
            }
            Logger.PrintLog(sb.ToString());
        }
    }

    public static string GetFairyCoinIconStr(coinType coinType)
    {
        switch (coinType)
        {
            case coinType.coin:
                return "<img src='" + UIPackage.GetItemURL("common", "common_icon_coins") + "'/>";
            case coinType.silverCoin:
                return "<img src='" + UIPackage.GetItemURL("common", "common_icon_jinbi") + "'/>";
            case coinType.gold:
                return "<img src='" + UIPackage.GetItemURL("common", "common_icon_coins") + "'/>";
            case coinType.diamond:
                return "<img src='" + UIPackage.GetItemURL("common", "common_icon_coins") + "'/>";
        }
        return "<img src='" + UIPackage.GetItemURL("common", "common_icon_coins") + "'/>";
    }
}