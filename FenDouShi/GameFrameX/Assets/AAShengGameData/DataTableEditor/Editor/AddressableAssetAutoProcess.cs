using FairyGUI.Dynamic;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.VersionControl;
using UnityEngine;

// 定义命名空间
namespace Assets.AAShengGameData.DataTableEditor.Editor
{
    // 定义一个继承自AssetPostprocessor的类，用于处理资源导入后的操作
    public class AddressableAssetAutoProcess : AssetPostprocessor
    {
        // 静态常量，存储Addressable Asset设置文件路径
        private const string ADDRESSABLE_SETTINGS_PATH = "Assets/AddressableAssetsData/AddressableAssetSettings.asset";
        // 静态常量，存储资源路径前缀
        private const string ASSETS_PREFIX = "Assets/";
        // 静态常量，存储可寻址资源路径标识 Fgui资源
        private const string ADDRESSABLE_RES_PATH = "/AssetsPackage/";
        // 静态常量，存储可寻址资源路径标识 Config
        private const string ADDRESSABLE_EXCEL_PATH = "/JsonData/";
        // 静态常量，存储文件夹模式标识
        private const string FOLDER_MODE = "FolderMode";
        // 静态常量，存储地址ableres路径标识
        private const string ADDRESSABLE_RES_LOWER = "addressableres/";
        // 静态常量，存储斜杠字符
        private const char SLASH = '/';
        // 静态常量，存储连字符字符
        private const char HYPHEN = '-';

        private const string fguiGroupName = "fgui_Login";//fairyGUI的Addressables组名
        private const string FairyGUIFolder = "FairyGUI";//fairyGUI所在文件夹包含的名字

        private const string DataTableGroupName = "Configs";//配置表所在Addressables组的名字
        private const string DataTableFolder = "JsonData";//配置表所在文件夹包含的名字

        // 静态变量，加载Addressable Asset设置文件
        private static AddressableAssetSettings setting = AssetDatabase.LoadAssetAtPath<AddressableAssetSettings>(ADDRESSABLE_SETTINGS_PATH);
        // 添加此静态字段，用于防止在导入回调中重复触发 UI 包映射生成，避免循环
        private static bool s_isProcessingImport = false;
        // 当所有资源导入、删除或移动后调用的静态方法
        public static void OnPostprocessAllAssets(string[] importedAsset, string[] deleteAsset, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // 标记开始处理导入
            if (!s_isProcessingImport)
            {
                s_isProcessingImport = true;
                try
                {
                    // 处理导入的资源
                    ProcessImportedAssets(importedAsset);
                    // 处理移动的资源
                    ProcessMovedAsset(movedAssets, movedFromAssetPaths);
                    // 处理删除的资源
                    ProcessDeletedAssets(deleteAsset);
                }
                finally
                {
                    // 标记处理结束
                    s_isProcessingImport = false;
                }
            }
        }

        // 静态方法，用于处理导入的资源
        private static void ProcessImportedAssets(string[] assetPath)
        {
            // 如果导入的资源路径数组为空或长度为0，则直接返回
            if (assetPath == null || assetPath.Length == 0)
            {
                return;
            }

            // 遍历导入的资源路径数组
            for (int i = 0; i < assetPath.Length; ++i)
            {
                // 检查该资源是否为可寻址资源
                if (CheckIsRes4Addresable(assetPath[i]))
                {
                    // 处理该资源的分组
                    ProcessAssetGroup(assetPath[i]);
                }
            }
        }

        // 静态方法，用于处理移动的资源
        private static void ProcessMovedAsset(string[] movedAssets, string[] movedFromAssets)
        {
            // 如果移动的资源数组不为空且长度大于0
            if (movedAssets != null && movedAssets.Length > 0)
            {
              //  Logger.PrintGreen("=======处理移动文件==========");
                // 遍历移动的资源数组
                for (int i = 0; i < movedAssets.Length; ++i)
                {
                  //  Logger.PrintDebug($"处理移动文件movedAssets[{i}]={movedAssets[i]}  movedFromAssets[{i}]={movedFromAssets[i]}");
                    // 处理该资源的移动操作
                    ProcessAssetGroup(movedAssets[i], movedFromAssets[i]);

                }
            }
        }

        // 静态方法，用于处理删除的资源
        private static void ProcessDeletedAssets(string[] deletedAssets)
        {
            // 如果删除的资源路径数组为空或长度为0，则直接返回
            if (deletedAssets == null || deletedAssets.Length == 0)
            {
                return;
            }

            // 遍历删除的资源路径数组
            for (int i = 0; i < deletedAssets.Length; ++i)
            {
                // 检查删除的资源是否在FairyGUI文件夹中
                if (deletedAssets[i].Contains(FairyGUIFolder))
                {
                    // 调用 UIPackageMappingGenerator.Generate() 更新
                    UIPackageMappingGenerator.Generate();
                    break;
                }
            }
        }

        // 静态方法，用于检查资源是否为可寻址资源
        private static bool CheckIsRes4Addresable(string name)
        {
            bool isAddressableRes = name.Contains(ADDRESSABLE_RES_PATH);
            bool isAddressableExcel = name.Contains(ADDRESSABLE_EXCEL_PATH);
            bool isUIPackageMapping = name.Contains("UIPackageMapping.asset");
            if (isUIPackageMapping) return false;
            if (!isAddressableRes && !isAddressableExcel) return false;

            // 检查是否是文件而非文件夹
            if (string.IsNullOrEmpty(Path.GetFileName(name))) return false;

            // 检查是否有扩展名
            if (!Path.HasExtension(name)) return false;

            return true;
        }

        // 静态方法，将Assets路径转换为绝对路径
        public static string AssetsPath2ABSPath(string assetsPath)
        {
            // 获取Assets目录的绝对路径
            string assetRootPath = Path.GetFullPath(Application.dataPath);
            // 拼接路径，去掉"Assets"部分后加上传入的资源路径
            assetRootPath = assetRootPath.Substring(0, assetRootPath.Length - ASSETS_PREFIX.Length) + assetsPath;
            // 将路径中的反斜杠替换为正斜杠
            return assetRootPath.Replace("\\", "/");
        }

        // 实现 EditorUtils.AssetPath2ReltivePath 类似功能
        // 静态方法，将资源路径转换为相对路径
        private static string AssetPath2ReltivePath(string assetPath)
        {
            // 假设这里简单返回去掉 "Assets/" 前缀的路径
            // 如果资源路径以"Assets/"开头
            if (assetPath.StartsWith(ASSETS_PREFIX))
            {
                // 返回去掉"Assets/"前缀后的路径
                return assetPath.Substring(ASSETS_PREFIX.Length);
            }
            // 否则直接返回原路径
            return assetPath;
        }

        // 静态方法，处理资源的分组
        private static void ProcessAssetGroup(string assetPath)
        {
            Logger.PrintGreen("========检查导入的fgui文件是否自动添加到Addressables==============");
            Logger.PrintGreen("资源Asset路径为:" + assetPath);
            // 获取指定路径的资源导入器
            AssetImporter ai = AssetImporter.GetAtPath(assetPath);
            // 如果资源导入器为空
            if (ai == null)
            {
                // 打印未找到资源的调试信息
                Logger.PrintDebug("未找到资源:" + assetPath);
                return;
            }

            // 将资源路径转换为绝对路径
            string fullPath = AssetsPath2ABSPath(assetPath);
            Logger.PrintDebug("资源全路径为=" + fullPath);
            // 如果该路径是一个目录
            if (Directory.Exists(fullPath))
            {
                Logger.PrintColor("red", "路径是一个目录:" + fullPath);
                return;
            }

            // 定义分组名称变量
            string groupName = string.Empty;

            // 获取资源路径的目录名
            string dirName = Path.GetDirectoryName(assetPath);
            // 替换 EditorUtils.AssetPath2ReltivePath 调用
            // 将目录名转换为相对路径并转为小写
            Logger.PrintDebug("资源路径的目录名=" + dirName);
            string assetBundleName = AssetPath2ReltivePath(dirName).ToLower(); ;
            // 去掉路径中的"addressableres/"部分
            assetBundleName = assetBundleName.Replace(ADDRESSABLE_RES_LOWER, "");
            // 如果资源路径包含"FolderMode"
            Logger.PrintDebug("分组前资源包名=" + assetBundleName + " assetPath=" + assetPath);
            if (assetPath.Contains(FairyGUIFolder))
            {
                // 分组名称为处理后的资源包名
                groupName = fguiGroupName;
                UIPackageMappingGenerator.Generate();
                // 整体处理流程已经在 OnPostprocessAllAssets 中控制，此处移除重复控制
            }
            else if (assetPath.Contains(DataTableFolder))
            {
                // 分组名称为处理后的资源包名
                groupName = DataTableGroupName;
            }
            // 将分组名称中的斜杠替换为连字符
            groupName = groupName.Replace(SLASH, HYPHEN);
            // 在设置中查找该分组
            var group = setting.FindGroup(groupName);
            Logger.PrintGreen("分组后的资源包名=" + groupName);
            // 如果分组不存在
            if (group == null)
            {
                Logger.PrintError("分组不存在:" + groupName);
                return;
            }

            // 获取资源的GUID
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            // 在设置中创建或移动资源条目到指定分组
            var entry = setting.CreateOrMoveEntry(guid, group);
            // 假设 PathHelper 也无法获取，暂时保留原调用，实际使用时需要处理
            // 设置资源条目的地址
            string fileName = Path.GetFileName(assetPath);
            string fileNameWithoutSuffix = PathHelper.FileNameWithoutSuffix(fileName);
            Logger.PrintDebug("资源文件名=" + fileNameWithoutSuffix);
            entry.SetAddress(fileNameWithoutSuffix, true);
            Logger.PrintColor("yellow", $"自动设置资源条目地址{fileNameWithoutSuffix}成功");

            // 如果资源属于Configs组，添加config标签
            if (groupName == DataTableGroupName)
            {
                entry.labels.Add(ConfigMgr.label);
                Logger.PrintColor("green", $"为资源 {assetPath} 添加 config 标签成功");
            }
        }

        /// <summary>
        /// 处理移动
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="moveFromPath"></param>
        // 静态方法，处理资源移动操作
        private static void ProcessAssetGroup(string assetPath, string moveFromPath)
        {
            // 获取指定路径的资源导入器
            AssetImporter ai = AssetImporter.GetAtPath(assetPath);
            // 如果资源导入器为空
            if (ai == null)
            {
                // 打印未找到资源的调试信息
                Logger.PrintDebug("未找到资源:" + assetPath);
                return;
            }

            // 替换 EditorUtils.AssetsPath2ABSPath 调用
            // 将资源路径转换为绝对路径
            string fullPath = AssetsPath2ABSPath(assetPath);
            Logger.PrintDebug("资源全路径为=" + fullPath);
            // 如果该路径是一个目录
            if (Directory.Exists(fullPath))
            {
                Logger.PrintColor("red", "路径是一个目录:" + fullPath);
                return;
            }

            // 检查移动后的资源是否为可寻址资源
            if (CheckIsRes4Addresable(assetPath))// 检查移动后的是否是资源文件
            {
                // 处理该资源的分组
                ProcessAssetGroup(assetPath);
            }
            else
            {
                // 获取资源的GUID
                var guid = AssetDatabase.AssetPathToGUID(assetPath);
                // 从设置中移除该资源条目
                setting.RemoveAssetEntry(guid);
            }

            // 检查移动前的资源是否为可寻址资源
            if (CheckIsRes4Addresable(moveFromPath))
            {
                // 处理移动前的Group
                // 定义要移除的分组名称变量
                string removeFromGroupName = string.Empty;
                // 获取移动前资源路径的目录名
                string dirName = Path.GetDirectoryName(moveFromPath);
                // 替换 EditorUtils.AssetPath2ReltivePath 调用
                // 将目录名转换为相对路径并转为小写
                string assetBundleName = AssetPath2ReltivePath(dirName).ToLower();
                // 去掉路径中的"addressableres/"部分
                assetBundleName = assetBundleName.Replace(ADDRESSABLE_RES_LOWER, "");

                // 如果移动前的资源路径包含"FolderMode"
                if (moveFromPath.Contains(FOLDER_MODE))
                {
                    // 要移除的分组名称为处理后的资源包名
                    removeFromGroupName = assetBundleName;
                }
                else
                {
                    // 要移除的分组名称为默认分组名 等同于fguiGroupName（为默认）
                    removeFromGroupName = setting.DefaultGroup.name;
                }
                // 将分组名称中的斜杠替换为连字符
                removeFromGroupName = removeFromGroupName.Replace(SLASH, HYPHEN);
                // 在设置中查找该分组
                var group = setting.FindGroup(removeFromGroupName);
                // 如果分组存在
                if (group != null)
                {
                    // 如果该分组中的条目数量为0
                    if (group.entries.Count == 0)
                    {
                        // 从设置中移除该分组
                        setting.RemoveGroup(group);
                    }

                }
            }
        }
    }
}
public static class PathHelper
{
    /// <summary>
    /// 获取文件名（不包含后缀）
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>不包含后缀的文件名</returns>
    public static string FileNameWithoutSuffix(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return string.Empty;
        }
        string fileName = Path.GetFileName(path);
        if (string.IsNullOrEmpty(fileName))
        {
            return string.Empty;
        }
        int dotIndex = fileName.LastIndexOf('.');
        if (dotIndex > 0)
        {
            return fileName.Substring(0, dotIndex);
        }
        return fileName;
    }
}