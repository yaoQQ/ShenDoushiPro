
using FairyGUI;
using FairyGUI.Dynamic;
using HybridCLR;
using msg.login;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AddressableAssets;


public class TypeRef : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UIPackageMapping.Init();
        //LoadMetadataForAOTAssemblies(() => {


        //    LoginReq loginReq = new LoginReq();
        //    loginReq.Token = "123";
        //    loginReq.Account = "4321";
        //    loginReq.ServerId = 1120;
        //    using (var ms = new MemoryStream())
        //    {
        //        Logger.PrintColor("white", $"发送协议数据 ProtobufTool ms({ms})");
        //        Logger.PrintColor("white", $"发送协议数据 ProtobufTool Serializer({typeof(Serializer)})");
        //        //   Serializer.Serialize(ms, loginReq);
        //        Serializer.Serialize(ms, loginReq);
        //        Logger.PrintGreen("TypeRef  Serialize complete!");
        //    }

        //});
      


    }

    private static void LoadMetadataForAOTAssemblies(Action callBack)
    {
        List<string> aotDllList = new List<string>
        {
            //"mscorlib.dll",
            //"System.dll",
            //"System.Core.dll", // 如果使用了Linq，需要这个
            //"CommonScriptCode.dll",
            //"Newtonsoft.Json.dll",
            //"System.Runtime.CompilerServices.Unsafe.dll",
            //"UniTask.dll",
            //"Unity.Addressables.dll",
            //"Unity.ResourceManager.dll",
            //"UnityEngine.CoreModule.dll",
            //"ZString.dll",
            "protobuf-net.Core",
            "protobuf-net",
        };
        int isLoadSuccess = 0;
        foreach (var aotDllName in aotDllList)
        {
            Addressables.LoadAssetAsync<TextAsset>(aotDllName).Completed += (handle) =>
            {
                // byte[] dllBytes = File.ReadAllBytes($"{Application.streamingAssetsPath}/{aotDllName}.bytes");
                byte[] dllBytes = handle.Result.bytes;
                var err = HybridCLR.RuntimeApi.LoadMetadataForAOTAssembly(dllBytes, HomologousImageMode.SuperSet);
                Logger.PrintColor("red", $"@@@@@@@@LoadMetadataForAOTAssembly:{aotDllName}. ret:{err}");
                isLoadSuccess++;
                if(isLoadSuccess>= aotDllList.Count)
                {
                    if (callBack != null)
                    {
                        callBack();
                    }
                }
            };

        }
    }

    private void OnApplicationQuit()
    {
        FGUIOperatHandletManager.Instance.ReleaseAllAssets();
        UIPackage.RemoveAllPackages();
        FGUIAssetManager.Instance.Destroy();
    }
}
