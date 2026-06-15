using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using DataTableFrame;

public class AutoRenderScriptGenerator : EditorWindow
{
    private string renderName = "NewRender";


    [MenuItem("Assets/Create/FairyGUI Render Script", false, 7)]
    public static void CreateRender()
    {
        GetWindow<AutoRenderScriptGenerator>("Generate FairyGUIView");
    }


    void OnGUI()
    {
        GUILayout.Label("请输入脚本名称:", EditorStyles.boldLabel);
        renderName = EditorGUILayout.TextField(renderName);

        EditorGUILayout.Space();

        if (GUILayout.Button("点击生22成"))
        {
            CreateScriptFromTemplate(renderName);
            Close(); // 生成后关闭窗口
        }
    }

    private static void CreateScriptFromTemplate(string scriptName)
    {
        if (string.IsNullOrEmpty(scriptName)) return;

        string selectPath = "Assets/";
        if (Selection.activeObject != null)
        {
            selectPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (!UnityEngine.Windows.Directory.Exists(selectPath))
            {
                selectPath = Path.GetDirectoryName(selectPath);
            }
        }

        string filePath = Path.Combine(selectPath, scriptName + ".cs");
        filePath = AssetDatabase.GenerateUniqueAssetPath(filePath);

        // 读取模板文件，使用GB2312编码
        string templateContent = File.ReadAllText(ConstEditor.FairyGUIRenderCodeTemplate, Encoding.GetEncoding("GB2312"));
        // 替换模板中的占位符
        string finalContent = templateContent.Replace("#SCRIPTNAME#", scriptName);

        // 写入文件，使用GB2312编码
        File.WriteAllText(filePath, finalContent, Encoding.GetEncoding("GB2312"));
        AssetDatabase.Refresh();

        // 选中新创建的文件
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(filePath);
    }
}

