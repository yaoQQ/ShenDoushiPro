using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
//管理加载程序集
public class PreloadAssembleManager
{
    private Dictionary<string, Assembly> AssembleDic = new Dictionary<string, Assembly>();
    public PreloadAssembleManager() {

    }
    //加载所需的程序集代码
    public void LoadGameAssemble(string AssembleName, Action<Assembly> callBack) {
        if (AssembleDic.ContainsKey(AssembleName)) {
            Logger.PrintColor("yellow", "已经加载过" + AssembleName + " Ass=" + AssembleDic[AssembleName]);
            if (callBack != null) {
                callBack(AssembleDic[AssembleName]);
            }
            return;
        }
#if HY_EDITOR
        Logger.PrintColor("yellow", "LoadGameAssemble（） 开始加载packageName=" + AssembleName);
        Assembly gameAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == AssembleName);
        if (gameAss == null) {
            Logger.PrintError("yellow", "LoadGameAssemble() 开始加载失败！packageName=" + AssembleName);
            return;
        }
        Logger.PrintColor("yellow", "LoadGameAssemble() 加载完成 gameAss=" + gameAss);
        AssembleDic[AssembleName] = gameAss;
        if (callBack != null) {
            callBack(gameAss);
        }
#else
        Logger.PrintColor("yellow","loadDll（） 开始加载"+ packageName);
              AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<TextAsset>(AssembleName);
        ProjectTestHandle.Completed += (op) => {
            if (op.Status == AsyncOperationStatus.Succeeded) {
                TextAsset textAsset = (TextAsset)op.Result;
                Assembly gameAss = Assembly.Load(textAsset.bytes);
                AssembleDic[AssembleName] = gameAss;
                if (callBack != null) {
                    callBack(gameAss);
                }
            }
            else {
               Logger.PrintError("yellow", "LoadGameAssemble() 开始加载失败！packageName=" + AssembleName);
            }
        };
#endif
    }
    public void Destory() {
        AssembleDic.Clear();
    }
}

