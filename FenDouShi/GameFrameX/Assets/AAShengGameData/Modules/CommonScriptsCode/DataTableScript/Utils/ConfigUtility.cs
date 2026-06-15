using Cysharp.Text;
using System;
using System.Collections.Generic;

namespace DataTableFrame
{
    public class CongfigUtility
    {
        public const char AB_TEST_TAG = '#';
        public class Json
        {
            public static string ToJson(object obj)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }

            public static T ToObject<T>(string json)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
            }
            public static object ToObject(string json)
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            }
        }
        public static class AssetsPath
        {
            public static string GetCombinePath(params string[] args)
            {
                return Utility.Path.GetRegularPath(System.IO.Path.Combine(args));
            }
            public static string GetDataTablePath(string name, bool useBytes)
            {
                return Utility.Text.Format("Assets/AAShengGameData/DataTable/{0}.{1}", name, useBytes ? "bytes" : "txt");
            }

            public static string GetSoundPath(string name)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Audio/{0}", name);
            }

            public static string GetScenePath(string name)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Scene/{0}.unity", name);
            }
            public static string GetEntityPath(string name)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Prefabs/Entity/{0}.prefab", name);
            }

            public static string GetUIFormPath(string v)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Prefabs/UI/{0}.prefab", v);
            }

            public static string GetTexturePath(string fileName)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Textures/{0}", fileName);
            }
            public static string GetSpritesPath(string fileName)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Sprites/{0}", fileName);
            }
            public static string GetConfigPath(string v, bool useBytes)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Config/{0}.{1}", v, useBytes ? "bytes" : "txt");
            }
            public static string GetScriptableConfigPath(string v)
            {
                return Utility.Text.Format("Assets/AAShengGameData/ScriptableAssets/{0}.asset", v);
            }

            public static string GetPrefab(string v)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Prefabs/{0}.prefab", v);
            }

            public static string GetLanguagePath(string v, bool useBytes)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Language/{0}.{1}", v, useBytes ? "bytes" : "json");
            }
            public static string GetMaterialPath(string v)
            {
                return Utility.Text.Format("Assets/AAShengGameData/Material/{0}.mat", v);
            }

            public static string GetHotfixDll(string dllName)
            {
                return Utility.Text.Format("Assets/AAShengGameData/HotfixDlls/{0}.bytes", dllName);
            }

            public static string GetScriptableAsset(string v)
            {
                return Utility.Text.Format("Assets/AAShengGameData/ScriptableAssets/{0}.asset", v);
            }
        }
        /// <summary>
        /// 程序集相关的实用函数。
        /// </summary>
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblies = null;
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);

            static Assembly()
            {
                s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            /// <summary>
            /// 获取已加载的程序集。
            /// </summary>
            /// <returns>已加载的程序集。</returns>
            public static System.Reflection.Assembly[] GetAssemblies()
            {
                return s_Assemblies;
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的程序集中的所有类型。</returns>
            public static Type[] GetTypes()
            {
                List<Type> results = new List<Type>();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <param name="results">已加载的程序集中的所有类型。</param>
            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new Exception("Results is invalid.");
                }

                results.Clear();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new Exception("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    type = Type.GetType(String.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }
        }
        /// <summary>
        /// 获取规范的路径。
        /// </summary>
        /// <param name="path">要规范的路径。</param>
        /// <returns>规范的路径。</returns>
        public static string GetRegularPath(string path)
        {
            if (path == null)
            {
                return null;
            }

            return path.Replace('\\', '/');
        }

        public static string GetCombinePath(params string[] args)
        {
            return GetRegularPath(System.IO.Path.Combine(args));
        }
        public static string GetScriptableAsset(string v)
        {
            return string.Format("Assets/AAShengGameData/AAShengGameData/{0}.asset", v);
        }
        public static string Format(params string[] msgs)
        {
            using (var sb = ZString.CreateStringBuilder())
            {
                int len = msgs.Length;
                for (int i = 0; i < len; ++i)
                {
                    sb.Append(msgs[i]);
                }
                return sb.ToString();
            }
        }
    }
}