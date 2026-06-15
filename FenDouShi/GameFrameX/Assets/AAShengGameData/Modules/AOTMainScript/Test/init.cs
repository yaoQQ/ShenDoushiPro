using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.ParticleSystem;

public class init : MonoBehaviour
{

    void Start()
    {

        //  Addressables.CleanBundleCache();
        string cataLogPath = $"{Addressables.RuntimePath}/catalog.json";
        string cataLogPath2 = $"{Addressables.LibraryPath}/catalog.json";
        string cataLogPath3 = $"{Addressables.BuildPath}/catalog.json";
        string cataLogPath4 = $"{Addressables.PlayerBuildDataPath}/catalog.json";
        Debug.Log("RuntimePath=" + cataLogPath);
        Debug.Log("LibraryPath=" + cataLogPath2);
        Debug.Log("BuildPath=" + cataLogPath3);
        Debug.Log("PlayerBuildDataPath=" + cataLogPath4);
        Addressables.InitializeAsync();
        Debug.Log("Addressables.BuildPath=" + UnityEngine.AddressableAssets.Addressables.BuildPath);
        Debug.Log("Addressables.RuntimePath=" + UnityEngine.AddressableAssets.Addressables.RuntimePath);
        TestAssemby();
        AOTLoadManager.Instance.Load();
    }
    private void TestAssemby()
    {
        Assembly[] assmblesPath = System.AppDomain.CurrentDomain.GetAssemblies();
        foreach (var assemble in assmblesPath)
        {
            Debug.Log("@@@@Assemble@@@@ name=" + assemble.GetName().Name);

        }
        Assembly BasePackageAss = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "BasePackage");
        Debug.Log("@@@@Assemble@@@@ BasePackage=" + BasePackageAss);
    }

}
