using HybridCLR.Editor.Link;
using HybridCLR.Editor.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HybridCLR.Editor.Commands
{

    public static class Il2CppDefGeneratorCommand
    {

        [MenuItem("HybridCLR/Generate/Il2CppDef", priority = 104)]
        public static void GenerateIl2CppDef()
        {
            Debug.Log($"<color='white'>Generate/Il2CppDef() Start</color>");
            var options = new Il2CppDef.Il2CppDefGenerator.Options()
            {
                UnityVersion = Application.unityVersion,
                HotUpdateAssemblies = SettingsUtil.HotUpdateAssemblyNamesIncludePreserved,
                UnityVersionTemplateFile = $"{SettingsUtil.TemplatePathInPackage}/UnityVersion.h.tpl",
                UnityVersionOutputFile = $"{SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr/generated/UnityVersion.h",
                AssemblyManifestTemplateFile = $"{SettingsUtil.TemplatePathInPackage}/AssemblyManifest.cpp.tpl",
                AssemblyManifestOutputFile = $"{SettingsUtil.LocalIl2CppDir}/libil2cpp/hybridclr/generated/AssemblyManifest.cpp",
            };

            var g = new Il2CppDef.Il2CppDefGenerator(options);
            g.Generate();
            Debug.Log($"<color='white'>Generate/Il2CppDef() End</color>");
        }
    }
}
