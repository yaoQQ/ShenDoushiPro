#if UNITY_EDITOR
using System.IO;
using UnityEngine;
namespace DataTableFrame
{

    /// <summary>
    /// 默认编辑器配置项
    /// </summary>
    public class ConstEditor
    {
        public const bool AutoScriptUTF8 = true;//新建脚本时自动修改脚本编码方式为utf-8以支持中文
        public const string UIViewScriptFile = "Assets/AAShengGameData/Scripts/UI/Core/UIViews.cs";

        public const string UITableExcel = "Core/UITable.xlsx";
        public static string UITableExcelFullPath => CongfigUtility.AssetsPath.GetCombinePath(DataTableExcelPath, UITableExcel);

        // public const string ConstEditorPath = "Assets/AAShengGameData/DataTableEditor/Editor/Common/ConstEditor.cs";
        public const string ConstEditorPath = "Assets/AAShengGameData/Modules/CommonScriptsCode/CommonScriptsCode/Preload/PreloadManager.cs";
        public const string SoundGroupTableExcel = "Core/SoundGroupTable.xlsx";
        public static string SoundGroupTableExcelFullPath => CongfigUtility.AssetsPath.GetCombinePath(DataTableExcelPath, SoundGroupTableExcel);

        public const string UIGroupTableExcel = "Core/UIGroupTable.xlsx";
        public static string UIGroupTableExcelFullPath => CongfigUtility.AssetsPath.GetCombinePath(DataTableExcelPath, UIGroupTableExcel);

        public const string ConstGroupScriptFileFullName = "Assets/AAShengGameData/Scripts/Common/Core/Const.Groups.cs";

        public static readonly string PrefabsPath = "Assets/AAShengGameData/Prefabs";
        public static readonly string ScenePath = "Assets/AAShengGameData/Scene";

        public const string DataTableCodeTemplate = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates/DataTableCodeTemplate.txt"; //生成配置表代码的模板文件
        public const string FairyGUIViewCodeTemplate = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates/FairyGUIViewTemplate.txt"; //生成FairyGUI代码的模板文件
        public const string FairyGUIRenderCodeTemplate = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates/FairyGUIRendererTemplate.txt"; //生成FairyGUI render代码的模板文件
        public const string FairyGUIViewCodeOutPath = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates"; //生成FairyGUI代码的模板文件

        public const string ControlCodeTemplate = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates/ControlTemplate.txt"; //生成Control代码的模板文件
        public const string ModelCodeTemplate = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates/ModelTemplate.txt"; //生成Model代码的模板文件


        public const string SharedAssetBundleName = "SharedAssets";//AssetBundle分包共用资源
        public static readonly string[] DefaultLayers = { "UI" };
        internal static string KeystoreName => CongfigUtility.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "user.keystore");
        internal static string AssetBundleOutputPath => CongfigUtility.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AB");
        public static readonly string UpdatePrefixUri = "http://127.0.0.1/1_0_0_1/";//默认资源下载地址
        internal static readonly string AppUpdateUrl = "https://play.google.com/store/apps/details?id=";

        /// <summary>
        /// 数据表Excel目录
        /// </summary>
        public static string DataTableExcelPath => UtilityBuiltin.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAShengGameData/DataTables");//在项目Assets外
        /// <summary>
        /// 配置表Excel目录
        /// </summary>
        public static string ConfigExcelPath => UtilityBuiltin.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAShengGameData/Configs");//在项目Assets外
        /// <summary>
        /// 语言国际化Excel目录
        /// </summary>
        public static string LanguageExcelPath => UtilityBuiltin.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "AAShengGameData/Languages");//在项目Assets外


        public static string ToolsPath = UtilityBuiltin.AssetsPath.GetCombinePath(Directory.GetParent(Application.dataPath).FullName, "Tools");
        public const string DataTablePath = "Assets/AAShengGameData/DataTable";
        public const string GameConfigPath = "Assets/AAShengGameData/Config";
        public const string LanguagePath = "Assets/AAShengGameData/Language";
        public const string DataTableCodePath = "Assets/AAShengGameData/Modules/CommonScriptsCode/Excel2Cs";
        public const string UIFormTemplate = "Assets/AAShengGameData/ScriptsBuiltin/Editor/UI/Templates/UIFormTemplate.prefab";
        public const string UIDialogTemplate = "Assets/AAShengGameData/ScriptsBuiltin/Editor/UI/Templates/UIDialogTemplate.prefab";
    }
}
#endif