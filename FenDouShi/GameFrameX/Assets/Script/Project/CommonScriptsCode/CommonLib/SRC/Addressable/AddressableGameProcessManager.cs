using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using UnityEngine.AddressableAssets;

public class AddressableGameProcessManager : Singleton<AddressableGameProcessManager>
{
    private static Dictionary<string, AddressableSubGameVersion> m_dictSubGameVersionManager = new Dictionary<string, AddressableSubGameVersion>();
    /// <summary>游戏包名字典</summary>
    private static Dictionary<int, string> m_dictPackageName = new Dictionary<int, string>();


    /// <summary>
    /// 注册游戏包名
    /// </summary>
    public static void RegisterPackageName(int gameID, string packageName)
    {
        m_dictPackageName[gameID] = packageName;
    }

    public static void updateGameProcess()
    {
        if (m_dictSubGameVersionManager.Count <= 0)
        {

            return;
        }
        //子游戏加载更新
        foreach(KeyValuePair<string, AddressableSubGameVersion> pair in m_dictSubGameVersionManager)
        {
            pair.Value.UpdateAddressableDependencies();
        }
    }
    /// <summary>
    /// 检查下载
    /// </summary>
    public static void CheckDownload(int gameID, Action<uint> checkCallback)
    {
        ////不加载资源，游戏内包直接开始游戏 checkCallback返回游戏包size
        //if (CommonPathUtils.isLoadEditorRes)
        //{
        //    checkCallback(0);
        //    return;
        //}
      
        Logger.PrintLog(CommonUtils.ConnectStrs("检查下载：", gameID.ToString()));

        //内部游戏
        string packageName = GetPackageName(gameID);
        Logger.PrintColor("red", "AddressableGameProcessManager CheckDownload()packageName=" + packageName);

        if (!m_dictSubGameVersionManager.ContainsKey(packageName))
            m_dictSubGameVersionManager.Add(packageName, new AddressableSubGameVersion(packageName));
        AddressableSubGameVersion subGameVersionManager = m_dictSubGameVersionManager[packageName];
     
        //test@@@
        //  MainThread.Instance.StartCoroutine(subGameVersionManager.CheckDownload(checkCallback));

    }
    /// <summary>
    /// 开始下载  lua点击下载调用
    /// </summary>
    public static void StartDownload(int gameID)
    {
        Logger.PrintColor("red", "AddressableGameProcessManager StartDownload()");
        string packageName = GetPackageName(gameID);
        if (!m_dictSubGameVersionManager.ContainsKey(packageName))
            m_dictSubGameVersionManager.Add(packageName, new AddressableSubGameVersion(packageName));
        AddressableSubGameVersion subGameVersionManager = m_dictSubGameVersionManager[packageName];
        subGameVersionManager.StartDownload((progress) =>
        {
            NoticeManager.Instance.Dispatch(NoticeType.Game_Update_Progress, progress);
        });
        return;
    }
    /// <summary>
    /// 取消下载
    /// </summary>
    public static void CancelDownload(int gameID)
    {
        Logger.PrintLog(CommonUtils.ConnectStrs("取消下载：", gameID.ToString()));
        string packageName = GetPackageName(gameID);
        if (!m_dictSubGameVersionManager.ContainsKey(packageName))
            m_dictSubGameVersionManager.Add(packageName, new AddressableSubGameVersion(packageName));
        AddressableSubGameVersion subGameVersionManager = m_dictSubGameVersionManager[packageName];
        subGameVersionManager.CancelDownload();
        return;

    }
 

    

   
    private static string GetPackageName(int gameID)
    {
        if (m_dictPackageName.ContainsKey(gameID))
            return m_dictPackageName[gameID];
        else
        {
            Logger.PrintError("游戏包名未注册");
            return "";
        }
    }
    public void DestroyPackage(string packageName)
    {
        if (m_dictSubGameVersionManager.ContainsKey(packageName)) {
            AddressableSubGameVersion subGame = m_dictSubGameVersionManager[packageName];
            subGame.Destroy();
            m_dictSubGameVersionManager.Remove(packageName);
          }
    }
}