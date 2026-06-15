using System.Collections.Generic;
using UnityEngine;
using FairyGUI.Utils;

namespace FairyGUI.Dynamic
{
    public  class UIPackageMapping : ScriptableObject, IUIPackageHelper
    {
        public string[] PackageIds;
        public string[] PackageNames;
        public static void Init()
        {

        }
        public UIPackageMapping(byte[] mappingData)
        {
            ByteBuf byteBuf = new ByteBuf(mappingData);
            int count = byteBuf.ReadInt();

            Dictionary<string, string> mappingDict = new Dictionary<string, string>(count);
            for (int i = 0; i < count; i++)
            {
                mappingDict.Add(byteBuf.ReadString(), byteBuf.ReadString());
            }

            m_PackageIdToNameMap = mappingDict;
        }
        public string GetPackageNameById(string id)
        {
            if (m_PackageIdToNameMap == null)
            {
                m_PackageIdToNameMap = new Dictionary<string, string>();

                if (PackageIds != null && PackageNames != null)
                {
                    var count = Mathf.Min(PackageIds.Length, PackageNames.Length);
                    for (var i = 0; i < count; i++)
                    {
                        Debug.Log("add id="+ PackageIds[i] + "  PackageName=" + PackageNames[i]);
                        m_PackageIdToNameMap.Add(PackageIds[i], PackageNames[i]);

                    }
                }
            }
            
            return m_PackageIdToNameMap.TryGetValue(id, out var packageName) ? packageName : null;
        }
        
        private Dictionary<string, string> m_PackageIdToNameMap;
    }
}