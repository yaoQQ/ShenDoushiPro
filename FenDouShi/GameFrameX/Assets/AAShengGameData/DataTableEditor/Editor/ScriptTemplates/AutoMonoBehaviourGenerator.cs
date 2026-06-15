using DataTableFrame;
using System;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class ScriptTemplatesGenerator : EditorWindow
{
    private string className = "MyGeneratedFairyGUIView";
    private string G_componetName = "BagView";
   

    [MenuItem("Tools/Generate Code/Generate FairyGUIView")]
    public static void ShowWindow()
    {
        GetWindow<ScriptTemplatesGenerator>("Generate FairyGUIView");
    }

    //// 可添加快捷键支持
    //[MenuItem("Assets/Create/FairyGUI View Script", true)]
    //private static bool ValidateCreateScript()
    //{
    //    // 仅允许在文件夹上创建
    //    return Selection.activeObject != null &&
    //           !AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(Selection.activeObject));
    //}
    // 可添加模板选择功能
    //private static string[] templateOptions = { "FairyGUIView", "DataView", "UIController" };
    //private static int selectedTemplate = 0;
    private static string selectedPath;
    // 新增右键菜单创建脚本功能
    [MenuItem("Assets/Create/FairyGUI View Script", false, 6)]
    private static void CreateScriptInContextDirectory()
    {  
        // 获取当前选中的文件夹路径
        selectedPath = GetSelectedFolderPath();
        Debug.Log("selectedPath="+ selectedPath);
        if (string.IsNullOrEmpty(selectedPath))
        {
            Debug.LogWarning("请选择一个有效的文件夹来创建脚本");
            return;
        }
        ShowWindow();
    }
   

    private static void CreateScriptFile(string selectedPath, string inputClassName,string G_componetName)
    {
        // 生成完整文件路径
        string fileName = $"{inputClassName}.cs";
        string fullPath = System.IO.Path.Combine(selectedPath, fileName);

        // 检查文件是否已存在
        if (File.Exists(fullPath))
        {
            Debug.LogError($"文件已存在: {fullPath}");
            return;
        }

        // 生成脚本内容
        string templateContent = File.ReadAllText(ConstEditor.FairyGUIViewCodeTemplate, Encoding.GetEncoding("GB2312"));
        templateContent = templateContent.Replace("#SCRIPTNAME#", inputClassName);
        if (String.IsNullOrEmpty(inputClassName))
        {
            templateContent = templateContent.Replace("#ComponetName#", inputClassName);
        }
        else
        {
            templateContent = templateContent.Replace("#ComponetName#", G_componetName);
        }
        templateContent = templateContent.Replace("__DATA_TABLE_CREATE_TIME__", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));

        // 写入文件 - 使用GB2312编码
        File.WriteAllText(fullPath, templateContent, Encoding.GetEncoding("GB2312"));
        
        AssetDatabase.Refresh();

        // 打开文件并定位到创建位置
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(fullPath));
        Debug.Log($"成功创建脚本: {fullPath}");
    }
    private static string GetSelectedFolderPath()
    {
        if (Selection.activeObject == null)
            return "Assets/";

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path))
            return "Assets/";

        // 如果是预制体或场景，取其所在文件夹
        if (Path.GetExtension(path) == "")
        {
            return path + "/";
        }
        else
        {
            return Path.GetDirectoryName(path) + "/";
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("生成一个 FairyGUIView 脚本", EditorStyles.boldLabel);
        className = EditorGUILayout.TextField("填入生成的类名:", className);
        G_componetName = EditorGUILayout.TextField("填入fairyGUi的导出组件名(为空则导出为G_+类名):", G_componetName);

        if (GUILayout.Button("点击生成"))
        {
            //  GenerateMonoBehaviourScript(className);
            CreateScriptFile(selectedPath,className,G_componetName);
        }
    }

   
    public static void GenerateCodeFile(string className)
    {
        string m_CodeTemplate="";
        try
        {
            // 使用GB2312编码读取模板文件
            m_CodeTemplate = File.ReadAllText(ConstEditor.FairyGUIViewCodeTemplate, Encoding.GetEncoding("GB2312"));
            
            
        }
        catch (Exception exception)
        {
            Debug.LogError(Utility.Text.Format("Set code template '{0}' failure, exception is '{1}'.", ConstEditor.FairyGUIViewCodeTemplate, exception.ToString()));
        }
        if (string.IsNullOrEmpty(m_CodeTemplate))
        {
            return;
        }
        Logger.PrintColor("red", $"GenerateCodeFile() 开始解析代码FairyGUIViewCodeTemplate");
        StringBuilder stringBuilder = new StringBuilder(m_CodeTemplate);
        DataTableCodeGenerator(stringBuilder, className);

       Logger.PrintDebug(stringBuilder.ToString());
        // 目标路径，比如在 Scripts/Generated/ 下
        string outputDir = "Assets/AAShengGameData/DataTableEditor/Editor/ScriptTemplates";
        string filePath = Path.Combine(outputDir, className + ".cs");


        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }
        // 修复：使用filePath而非className作为文件路径
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write)) // 修改此处
        {
            // 使用GB2312编码写入文件
            using (StreamWriter stream = new StreamWriter(fileStream, Encoding.GetEncoding("GB2312")))
            {
                stream.Write(stringBuilder.ToString());
            }
        }

        AssetDatabase.Refresh();

        Logger.PrintGreen($"Generated Success!  path at: {filePath}");

    }
    private static void DataTableCodeGenerator(StringBuilder codeContent,string className)
    {
      //  string dataTableClassName = Path.GetFileNameWithoutExtension((string)userData);
        codeContent.Replace("__DATA_TABLE_CREATE_TIME__", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        codeContent.Replace("#SCRIPTNAME#", className);
    }
    
}