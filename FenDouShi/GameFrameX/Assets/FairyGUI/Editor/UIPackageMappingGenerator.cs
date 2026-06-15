using FairyGUI.Dynamic.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

public static class UIPackageMappingGenerator
{
    [MenuItem("Tools/FairyGUI/Generate Package Mapping(生成fairyGUI包映射资源)")]
    public static void Generate()
    {
        Debug.Log("生成fairyGUI包映射资源 Generating UIPackageMapping.asset...");
        UIPackageMappingUtility.GenerateMappingFile("Assets/AssetsPackage/FairyGUI", "Assets/AssetsPackage/FairyGUI/UIPackageMapping.asset");
        Debug.Log("<color='yellow'>UIPackageMapping 更新完成</color>");
    }

    [MenuItem("Tools/ReloadDomain(清理缓存,重新编译代码)")]
    public static void ReloadDomain()
    {
        CompilationPipeline.RequestScriptCompilation();
        Debug.Log("ReloadDomain() compeleted!");
    }
}