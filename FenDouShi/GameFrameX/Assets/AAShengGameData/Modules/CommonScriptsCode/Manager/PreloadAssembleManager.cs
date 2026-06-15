using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
//管理加载程序集
public class PreloadAssembleManager
{
    private Dictionary<PackageEnum, Assembly> AssembleDic = new Dictionary<PackageEnum, Assembly>();
    public PreloadAssembleManager() {
        Assembly gameAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == PackageEnum.CommonScriptCode.ToString());
        if (gameAss == null)
        {
            Logger.PrintError("yellow", "LoadGameAssemble() 开始加载失败！packageName=" + PackageEnum.CommonScriptCode);
            return;
        }
        Logger.PrintColor("yellow", "LoadGameAssemble() 加载完成 gameAss=" + gameAss);
        AssembleDic[PackageEnum.CommonScriptCode] = gameAss;
    }
    //加载所需的程序集代码
    public void LoadGameAssemble(PackageEnum AssembleName, Action<Assembly> callBack) {
        if (AssembleDic.ContainsKey(AssembleName)) {
            Logger.PrintColor("yellow", "已经加载过" + AssembleName + " Ass=" + AssembleDic[AssembleName]);
            if (callBack != null) {
                callBack(AssembleDic[AssembleName]);
            }
            return;
        }
 #if  UNITY_EDITOR
        Logger.PrintColor("yellow", "LoadGameAssemble（） 开始加载热更程序包=" + AssembleName);
        Assembly gameAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == AssembleName.ToString());
        if (gameAss == null)
        {
            Logger.PrintError("yellow", "LoadGameAssemble() 开始加载失败！packageName=" + AssembleName);
            return;
        }
        Logger.PrintColor("yellow", "LoadGameAssemble() 加载完成 gameAss=" + gameAss);
        AssembleDic[AssembleName] = gameAss;
        if (callBack != null)
        {
            callBack(gameAss);
        }
#else
        Logger.PrintColor("yellow","loadDll（） 开始加载热更程序包"+ AssembleName);
              AsyncOperationHandle ProjectTestHandle = Addressables.LoadAssetAsync<TextAsset>(AssembleName.ToString());
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
        AssembleDic = null;
    }
}

