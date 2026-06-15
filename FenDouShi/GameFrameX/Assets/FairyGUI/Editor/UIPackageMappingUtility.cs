using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace FairyGUI.Dynamic.Editor
{
    public static class UIPackageMappingUtility
    {
        public static void GenerateMappingFile(string assetRoot, string generatePath)
        {
            var packageFiles = UIPackageUtility.GetUIPackageFiles(assetRoot);
            
            var mapping = GetMappingMapping(generatePath);

            var packageIds = new List<string>();
            var packageNames = new List<string>();
            
            foreach (var packageFile in packageFiles)
            {
                if (!UIPackageUtility.ParseUIPackageIdAndName(packageFile, out var id, out var name))
                    continue;
                
                packageIds.Add(id);
                packageNames.Add(name);
            }
            
            mapping.PackageIds = packageIds.ToArray();
            mapping.PackageNames = packageNames.ToArray();

            EditorUtility.SetDirty(mapping);
            AssetDatabase.SaveAssets();
            Debug.Log("Generating Success!");
        }
        
        private static UIPackageMapping GetMappingMapping(string settingPath)
        {
            var mapping = AssetDatabase.LoadAssetAtPath<UIPackageMapping>(settingPath);
            if (mapping == null)
            {
                var dir = System.IO.Path.GetDirectoryName(settingPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                mapping = ScriptableObject.CreateInstance<UIPackageMapping>();
                mapping.PackageIds = Array.Empty<string>();
                mapping.PackageNames = Array.Empty<string>();

                AssetDatabase.CreateAsset(mapping, settingPath);
                AssetDatabase.SaveAssets();
            }

            return mapping;
        }
    }
}