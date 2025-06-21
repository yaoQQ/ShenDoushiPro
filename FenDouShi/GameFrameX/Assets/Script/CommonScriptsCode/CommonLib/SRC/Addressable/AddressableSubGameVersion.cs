using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableSubGameVersion 
{
    private bool m_isDownloadComplete = false;//是否下载完
    private Action<float> m_progressCallback = null;

    private string m_packageName = "";
    public string packageName
    {
        get { return m_packageName; }
    }
    private bool m_isCancelDownload = false;
    public bool isCancelDownload
    {
        get { return m_isCancelDownload; }
    }
    private bool isDownAddressable = false;//是否需要加载
    private AsyncOperationHandle downloadDependenciesOp = new AsyncOperationHandle();//游戏包的依赖
    public AsyncOperationHandle downloadHandle
    {
        get
        {
            return downloadDependenciesOp;
        }
    }
    public AddressableSubGameVersion(string packageName)
    {
        m_packageName = packageName;
   
    }
   public IEnumerator CheckDownload(Action<uint> checkCallback)
    {
        Debug.LogFormat("AddressableSubGameVersion CheckDownload() packageName = " + m_packageName);
        if (m_isDownloadComplete)//已经下载
        {
            checkCallback(0);
            yield break;
        }
        var loadSizeHandel = Addressables.GetDownloadSizeAsync(m_packageName);
        yield return loadSizeHandel;
        uint downLoadTotalSze = (uint)(loadSizeHandel.Result );
        string str = m_packageName+"下载总量=" + downLoadTotalSze + "k";
        Debug.Log(str);
        Logger.PrintColor("red", "AddressableGameProcessManager CheckDownload() downLoadTotalSze=" + downLoadTotalSze);

        checkCallback(downLoadTotalSze);
    }
   
    public void StartDownload(Action<float> progressCallback = null)
    {
        if (progressCallback != null)
            m_progressCallback = progressCallback;
        m_isCancelDownload = false;
        isDownAddressable = true;
        downloadDependenciesOp = Addressables.DownloadDependenciesAsync(m_packageName);
       
    }
    public void UpdateAddressableDependencies()
    {
        if (!isDownAddressable)
        {
            return;
        }
        if (!downloadDependenciesOp.IsValid())
        {
            return;
        }
        if (!downloadDependenciesOp.IsDone)
        {
            if (m_progressCallback != null)
                m_progressCallback(downloadDependenciesOp.PercentComplete);
        }
        else if (downloadDependenciesOp.IsDone)
        {


            isDownAddressable = false;
            if (downloadDependenciesOp.Status == AsyncOperationStatus.Succeeded)
            {
                m_isDownloadComplete = true;
            }
            m_progressCallback(1);
           
            Logger.PrintDebug("加载完成 packName="+ m_packageName);
           
        }
    }
   
    public void CancelDownload()
    {
        m_isCancelDownload = true;
        isDownAddressable = false;
        m_progressCallback = null;
        if (downloadDependenciesOp.IsValid())
            Addressables.Release(downloadDependenciesOp.Result);
    }
    public void Destroy()
    {
        m_isCancelDownload = false;
        isDownAddressable = false;
        m_progressCallback = null;
        if (downloadDependenciesOp.IsValid())
        Addressables.Release(downloadDependenciesOp);
    }

}