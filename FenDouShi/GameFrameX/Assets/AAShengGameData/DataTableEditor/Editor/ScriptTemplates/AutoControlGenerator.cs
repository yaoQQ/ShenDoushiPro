using DataTableFrame;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AutoControlGenerator : EditorWindow
{
    private static string ModuleName = "Custom";
    private static bool generateModel = true;

    [MenuItem("Assets/Create/Control Script", false, 8)]
    public static void ShowWindow()
    {
        GetWindow<AutoControlGenerator>("Generate Control");
    }

    void OnGUI()
    {
        GUILayout.Label("请输入模块名称:", EditorStyles.boldLabel);
        ModuleName = EditorGUILayout.TextField(ModuleName);
        EditorGUILayout.Space();

        generateModel = EditorGUILayout.ToggleLeft("是否生成Model:", generateModel);
        EditorGUILayout.Space();

        if (GUILayout.Button("点击生成"))
        {
            CreateControlFromTemplate();
            Close(); // 生成后关闭窗口
        }
    }   
    private static void CreateControlFromTemplate()
    {
        if (string.IsNullOrEmpty(ModuleName)) return;

        string path = "Assets/";
        if (Selection.activeObject != null)
        {
            path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!UnityEngine.Windows.Directory.Exists(path))
            {
                path = Path.GetDirectoryName(path);
            }
        }

        string filePath = Path.Combine(path, ModuleName + "Control.cs");
        filePath = AssetDatabase.GenerateUniqueAssetPath(filePath);

        // 读取模板文件，使用GB2312编码
        string[] templateLines = File.ReadAllLines(ConstEditor.ControlCodeTemplate, Encoding.GetEncoding("GB2312"));
        var outputLines = new List<string>();

        Dictionary<string, string> replacements = new Dictionary<string, string>
        {
            { "#MODULENAME#", ModuleName },
            { "__DATA_TABLE_CREATE_TIME__", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") }
        };

        Dictionary<string, bool> conditionEvaluator = new Dictionary<string, bool>
        {
            { "GENERATEMODEL", generateModel }
        };

        foreach (string line in templateLines)
        {
            string processedLine = ProcessLine(line, replacements, conditionEvaluator);
            if (processedLine != null)
            {
                outputLines.Add(processedLine);
            }
        }

        // 写入文件，使用GB2312编码
        File.WriteAllLines(filePath, outputLines, Encoding.GetEncoding("GB2312"));
        AssetDatabase.Refresh();

        // 生成对应的Model脚本
        if (generateModel)
        {
            string modelFilePath = Path.Combine(path, ModuleName + "Model.cs");
            modelFilePath = AssetDatabase.GenerateUniqueAssetPath(modelFilePath);

            // 读取模板文件，使用GB2312编码
            string templateModelContent = File.ReadAllText(ConstEditor.ModelCodeTemplate, Encoding.GetEncoding("GB2312"));
            // 替换模板中的占位符
            string finalModelContent = templateModelContent.Replace("#MODULENAME#", ModuleName);

            // 写入文件，使用GB2312编码
            File.WriteAllText(modelFilePath, finalModelContent, Encoding.GetEncoding("GB2312"));
            AssetDatabase.Refresh();
        }

        // 选中新创建的文件
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(filePath);
    }

    private static string ProcessLine(
        string line,
        Dictionary<string, string> replacements,
        Dictionary<string, bool> conditionEvaluator)
    {

        // 处理替换标记
        if (replacements != null)
        {
            foreach (var replacement in replacements)
            {
                line = line.Replace(replacement.Key, replacement.Value);
            }
        }


        // 处理条件语句
        if (IsConditionalLine(line, out string condition))
        {
            if (conditionEvaluator.TryGetValue(condition, out bool value))
            {
                if (value)
                {
                    return line.Substring(condition.Length + 6); // 移除条件标记
                }
            }
            return null; // 跳过这行
        }

        return line;
    }

    private static bool IsConditionalLine(string line, out string condition)
    {
        condition = null;
        line = line.Trim();

        if (line.StartsWith("//#IF "))
        {
            condition = line.Substring(6).Split(" ")[0].Trim(); // 移除 "//#IF "
            return true;
        }

        return false;
    }
}

    