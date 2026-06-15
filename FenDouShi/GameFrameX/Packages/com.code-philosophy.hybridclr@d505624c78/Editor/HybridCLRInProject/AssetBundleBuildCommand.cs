using HybridCLR.Editor.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor
{
    public static class AssetBundleBuildCommand
    {
        public static string HybridCLRBuildCacheDir => Application.dataPath + "/HybridCLRBuildCache";

        public static string AssetBundleOutputDir => $"{HybridCLRBuildCacheDir}/AssetBundleOutput";

        public static string AssetBundleSourceDataTempDir => $"{HybridCLRBuildCacheDir}/AssetBundleSourceData";

        public static HotUpdateAssemblyManifest HotUpdateManifest => Resources.Load<HotUpdateAssemblyManifest>("HotUpdateAssemblyManifest");

        public static string GetAssetBundleOutputDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleOutputDir}/{target}";
        }

        public static string GetAssetBundleTempDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleSourceDataTempDir}/{target}";
        }

        public static string ToRelativeAssetPath(string s)
        {
            return s.Substring(s.IndexOf("Assets/"));
        }

        /// <summary>
        /// 将HotFix.dll和HotUpdatePrefab.prefab打入common包.
        /// 将HotUpdateScene.unity打入scene包.
        /// </summary>
        /// <param name="tempDir"></param>
        /// <param name="outputDir"></param>
        /// <param name="target"></param>
        private static void BuildAssetBundles(string tempDir, string outputDir, BuildTarget target)
        {
            Directory.CreateDirectory(tempDir);
            Directory.CreateDirectory(outputDir);
            CompileDllCommand.CompileDll(target);


            List<string> notSceneAssets = new List<string>();

            string hotfixDllSrcDir = SettingsUtil.GetHotUpdateDllsOutputDirByTarget(target);

            foreach (var dll in SettingsUtil.HotUpdateAssemblyFilesIncludePreserved)
            {
                string dllPath = $"{hotfixDllSrcDir}/{dll}";
                string dllBytesPath = $"{tempDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                notSceneAssets.Add(dllBytesPath);
                // copy hotfix dll 
                //E:\unity\unityProject\The Little Prince\build_HuotuoResTools/HybridCLRData/HotUpdateDlls/StandaloneWindows64/CameraPostPackage.dll 
                // -> E:/unity/unityProject/The Little Prince/build_HuotuoResTools/Assets/HybridCLRBuildCache/AssetBundleSourceData/StandaloneWindows64/CameraPostPackage.dll.bytes
                //
                Debug.Log($"[BuildAssetBundles] copy hotfix dll {dllPath} -> {dllBytesPath}");
            }

            //string aotDllDir = SettingsUtil.GetAssembliesPostIl2CppStripDir(target);

            //反射"mscorlib", "System", "System.Core" 不知道啥用 先禁用
            /*
            HotUpdateAssemblyManifest manifest = HotUpdateManifest;
            if (manifest == null)
            {
                throw new Exception($"resource asset:{nameof(HotUpdateAssemblyManifest)} 配置不存在，请在Resources目录下创建");
            }
            List<string> AOTMetaAssemblies= (manifest.AOTMetadataDlls ?? Array.Empty<string>()).ToList();
            foreach (var dll in AOTMetaAssemblies)
            {
                string dllPath = $"{aotDllDir}/{dll}.dll";
                if (!File.Exists(dllPath))
                {
                    Debug.LogError($"ab中添加AOT补充元数据dll:{dllPath} 时发生错误,文件不存在。裁剪后的AOT dll在BuildPlayer时才能生成，因此需要你先构建一次游戏App后再打包。");
                    continue;
                }
                string dllBytesPath = $"{tempDir}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
                notSceneAssets.Add(dllBytesPath);
                //copy AOT dll 
                //E:\unity\unityProject\The Little Prince\build_HuotuoResTools/HybridCLRData/AssembliesPostIl2CppStrip/StandaloneWindows64/mscorlib.dll 
                //-> E:/unity/unityProject/The Little Prince/build_HuotuoResTools/Assets/HybridCLRBuildCache/AssetBundleSourceData/StandaloneWindows64/mscorlib.bytes
                Debug.Log($"[BuildAssetBundles] copy AOT dll {dllPath} -> {dllBytesPath}");
            }
           */

            //string testPrefab = $"{Application.dataPath}/Prefabs/HotUpdatePrefab.prefab";
            //notSceneAssets.Add(testPrefab);
            //AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            //List<AssetBundleBuild> abs = new List<AssetBundleBuild>();
            //AssetBundleBuild notSceneAb = new AssetBundleBuild
            //{
            //    assetBundleName = "DllBytes",
            //    assetNames = notSceneAssets.Select(s => ToRelativeAssetPath(s)).ToArray(),
            //};
            //abs.Add(notSceneAb);

            //UnityEditor.BuildPipeline.BuildAssetBundles(outputDir, abs.ToArray(), BuildAssetBundleOptions.None, target);

            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);

            //string streamingAssetPathDst = $"{Application.streamingAssetsPath}";
            //Directory.CreateDirectory(streamingAssetPathDst);

            //foreach (var ab in abs)
            //{
            //    AssetDatabase.CopyAsset(ToRelativeAssetPath($"{outputDir}/{ab.assetBundleName}"),
            //        ToRelativeAssetPath($"{streamingAssetPathDst}/{ab.assetBundleName}"));
            //}
        }
        private static void showDlls(string tar,List<string> strList)
        {
            for (int i = 0; i < strList.Count; i++)
            {
            
               Debug.LogFormat("@@{0}={1}", tar, strList[i]);
                
            }
           
        }

        public static void BuildAssetBundleByTarget(BuildTarget target)
        {
            BuildAssetBundles(GetAssetBundleTempDirByTarget(target), GetAssetBundleOutputDirByTarget(target), target);
        }

        [MenuItem("HybridCLR/CopyTO/ActiveBuildTarget")]
        public static void BuildSceneAssetBundleActiveBuildTarget()
        {
            BuildAssetBundleByTarget(EditorUserBuildSettings.activeBuildTarget);
            Debug.Log("<color='yellow'>BuildAssetBundleByTarget() Success</color>");
        }

        [MenuItem("HybridCLR/CopyTO/Win64")]
        public static void BuildSceneAssetBundleWin64()
        {
            var target = BuildTarget.StandaloneWindows64;
            BuildAssetBundleByTarget(target);
        }

        [MenuItem("HybridCLR/CopyTO/Win32")]
        public static void BuildSceneAssetBundleWin32()
        {
            var target = BuildTarget.StandaloneWindows;
            BuildAssetBundleByTarget(target);
        }

        [MenuItem("HybridCLR/CopyTO/Android")]
        public static void BuildSceneAssetBundleAndroid()
        {
            var target = BuildTarget.Android;
            BuildAssetBundleByTarget(target);
        }

        [MenuItem("HybridCLR/CopyTO/IOS")]
        public static void BuildSceneAssetBundleIOS()
        {
            var target = BuildTarget.iOS;
            BuildAssetBundleByTarget(target);
        }
    }
}
