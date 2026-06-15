#if UNITY_EDITOR
namespace DataTableFrame
{
    [DataTableFrame.FilePath("ProjectSettings/AppBuildSettings.asset")]
    public class AppBuildSettings : DataTableFrame.EditorScriptableSignleton<AppBuildSettings>
    {
        public string UpdatePrefixUri;
        public string ApplicableGameVersion;
        public bool ForceUpdateApp = false;
        public string AppUpdateUrl;
        public string AppUpdateDesc;
        public bool RevealFolder = false;
        public bool UseResourceRule = true;
        //Android Build Settings
        public bool AndroidUseKeystore;
        public string AndroidKeystoreName;
        public string KeystorePass;
        public string AndroidKeyAliasName;
        public string KeyAliasPass;

        public string ResourceBuildDir = "AB";
        public string AppBuildDir = "BuildApp";
    }

}
#endif