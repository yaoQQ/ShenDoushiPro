using UnityEngine;

public class ApkUpdate : MonoBehaviour
{
    //private static ApkUpdate m_instance;
    //public static ApkUpdate Instance
    //{
    //    get
    //    {
    //        if (m_instance == null)
    //        {
    //            m_instance = new GameObject("ApkUpdate").AddComponent<ApkUpdate>();
    //        }
    //        return m_instance;
    //    }
    //}

    //public void StartUpdate()
    //{
    //    TimeSpan span = (TimeSpan)(DateTime.UtcNow - new DateTime(0x7b2, 1, 1, 0, 0, 0, 0));
    //    string str = Convert.ToInt64(span.TotalSeconds).ToString();
    //    string url = Path.SERVER_ROOT_PATH + "/" + Path.APK_UPDATE_FILE_NAME + "?" + str;
    //    WWWManager.Instance.load(url,
    //        (www) =>
    //        {
    //            char[] separator = new char[] { '.' };
    //            string[] strArray = www.text.Split(separator);
    //            char[] chArray2 = new char[] { '.' };
    //            string[] strArray2 = GameVersionManager.serverVersion.version.Split(chArray2);
    //            if (((strArray.Length != 4) || (strArray2.Length != 4)) || (((int.Parse(strArray[0]) != int.Parse(strArray2[0])) || (int.Parse(strArray[1]) != int.Parse(strArray2[1]))) || (int.Parse(strArray[2]) != int.Parse(strArray2[2]))))
    //            {
    //                LoadingCtrl.ShowAlert1("新版本还未上架，请稍后再试", "退出", () => Application.Quit());
    //                return;
    //            }

    //            string downloadPath = Path.SERVER_ROOT_PATH + "/apk/";
    //            string saveDir = Path.PERSISTENT_DATA_ROOT_PATH + "/apk/";
    //            IOUtil.CreateDirectory(saveDir);
    //            string saveFileName = "yoyo";
    //            /*switch (Utility.Platform.channel)
    //            {
    //                case 1:
    //                    name += "_zhongju";
    //                    break;
    //                case 2:
    //                    name += "_huawei";
    //                    break;
    //                case 3:
    //                    name += "_oppo";
    //                    break;
    //                case 4:
    //                    name += "_vivo";
    //                    break;
    //                case 5:
    //                    name += "_xiaomi";
    //                    break;
    //                case 6:
    //                    name += "_cctv";
    //                    break;
    //            }*/
    //            if (Utility.Platform.isDev)
    //                saveFileName += "_dev";
    //            else if (Utility.Platform.isTest)
    //                saveFileName += "_test";
    //            else
    //                saveFileName += "_publish";
    //            saveFileName += "_" + www.text + ".apk";
    //            string downloadUrl = downloadPath + saveFileName;
    //            string saveFullName = saveDir + saveFileName;
    //            LoadingCtrl.isAutoClose = false;
    //            StartCoroutine(DownloadContinue(downloadUrl, saveFullName));
    //        }, null, null, null, true, 0);
    //}

    //private void showErrorTips(string downloadUrl, string saveFullName)
    //{
    //    LoadingCtrl.ShowAlert2("网络连接失败，请检查网络后重新连接", "重新连接",
    //        () =>
    //        {
    //            StartCoroutine(DownloadContinue(downloadUrl, saveFullName));
    //        }, "退出", () => Application.Quit());
    //}

    //private void showInstallTips(string saveFullName)
    //{
    //    LoadingCtrl.ShowAlert1("请安装最新版本", "开始安装",
    //        () =>
    //        {
    //            Logger.PrintLog("安装Apk:" + saveFullName);
    //            AndroidSDK.Instance.InstallApk(saveFullName);
    //        });
    //}


    //private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
    //{
    //    return true; //总是接受     
    //}

    //private IEnumerator DownloadContinue(string downloadUrl, string saveFullName)
    //{
    //    Logger.PrintLog("开始下载Apk:" + downloadUrl);
    //    //先获取Apk的size
    //    HttpWebRequest httpWebRequest = null;
    //    long countLength = 0L;
    //    try
    //    {
    //        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

    //        httpWebRequest = WebRequest.Create(downloadUrl) as HttpWebRequest;
    //        countLength = (httpWebRequest.GetResponse() as HttpWebResponse).ContentLength;
    //    }
    //    catch (Exception e)
    //    {
    //        Logger.PrintError("Get Apk Size Error:" + e.Message);
    //        if (httpWebRequest != null)
    //            httpWebRequest.Abort();
    //        showErrorTips(downloadUrl, saveFullName);
    //        yield break;
    //    }
    //    if (httpWebRequest != null)
    //        httpWebRequest.Abort();

    //    long lStartPos = 0L;
    //    FileStream fs;
    //    //如果文件存在，则上次已下载了一部分
    //    if (File.Exists(saveFullName))
    //    {
    //        Logger.PrintLog("读取已下载的Apk");
    //        fs = File.OpenWrite(saveFullName);
    //        lStartPos = fs.Length;
    //        if (countLength - lStartPos <= 0L)
    //        {
    //            fs.Close();
    //            Logger.PrintLog("下载完成");
    //            showInstallTips(saveFullName);
    //            yield break;
    //        }
    //        Logger.PrintLog("继续下载:" + lStartPos);
    //        fs.Seek(lStartPos, SeekOrigin.Current);
    //    }
    //    else
    //        fs = new FileStream(saveFullName, FileMode.Create);

    //    //下载
    //    HttpWebRequest request = null;
    //    HttpWebResponse response = null;
    //    Stream ns = null;
    //    try
    //    {
    //        request = WebRequest.Create(downloadUrl) as HttpWebRequest;
    //        if (lStartPos > 0L)
    //            request.AddRange((int)lStartPos);
    //        Logger.PrintLog("获取服务器回应");
    //        response = request.GetResponse() as HttpWebResponse;
    //        Logger.PrintLog("获取数据流");
    //        ns = response.GetResponseStream();
    //    }
    //    catch (Exception e)
    //    {
    //        Logger.PrintError("Request Apk Error:" + e.Message);
    //        if (ns != null)
    //            ns.Close();
    //        fs.Close();
    //        if (request != null)
    //            request.Abort();
    //        showErrorTips(downloadUrl, saveFullName);
    //        yield break;
    //    }

    //    //暂时改为是否WIFI环境下都提示下载
    //    LoadingCtrl.ShowAlert2("需要下载最新版本(" + ((countLength - lStartPos) / 1024f / 1024f).ToString("0.00") + "M)", "开始下载",
    //        () =>
    //        {
    //            StartCoroutine(Download(downloadUrl, saveFullName, countLength, ns, fs, request));
    //        }, "退出", () => Application.Quit());

    //    //非WIFI环境下提示下载
    //    /*if (Application.internetReachability != NetworkReachability.ReachableViaLocalAreaNetwork)
    //    {
    //        LoadingCtrl.ShowAlert2("需要下载最新版本(" + ((countLength - lStartPos) / 1024f / 1024f).ToString("0.00") + "M)", "开始下载",
    //            () =>
    //            {
    //                StartCoroutine(Download(downloadUrl, saveFullName, countLength, ns, fs, request));
    //            }, "退出", () => Application.Quit());
    //    }
    //    else
    //        StartCoroutine(Download(downloadUrl, saveFullName, countLength, ns, fs, request));*/
    //}

    //private IEnumerator Download(string downloadUrl, string saveFullName, long countLength, Stream ns, FileStream fs, HttpWebRequest request)
    //{
    //    LoadingCtrl.ShowProgressWindow();
    //    float totalLength = countLength / 1024f / 1024f;
    //    float startTime = Time.realtimeSinceStartup;
    //    int len = 0x2000;
    //    byte[] nbytes = new byte[len];
    //    int nReadSize = ns.Read(nbytes, 0, len);
    //    while (nReadSize > 0)
    //    {
    //        try
    //        {
    //            fs.Write(nbytes, 0, nReadSize);
    //            float curLength = (float)fs.Length / 1024f / 1024f;
    //            float progress = curLength / totalLength;
    //            LoadingCtrl.SetProgress(progress, 0);
    //            LoadingCtrl.SetLoadContent("正在下载新版本：" + curLength.ToString("0.00") + "M / " + totalLength.ToString("0.00") + "M");
    //            nReadSize = ns.Read(nbytes, 0, len);
    //        }
    //        catch (Exception e)
    //        {
    //            Logger.PrintError("Download Apk Error:" + e.Message);
    //            ns.Close();
    //            fs.Close();
    //            request.Abort();
    //            showErrorTips(downloadUrl, saveFullName);
    //            yield break;
    //        }
    //        float curTime = Time.realtimeSinceStartup;
    //        if (curTime > startTime + 0.033f)
    //        {
    //            startTime = curTime;
    //            yield return 0;
    //        }
    //    }
    //    LoadingCtrl.HideProgressWindow();

    //    //下载完成
    //    long length = fs.Length;
    //    Logger.PrintLog("fs.Length:" + length);
    //    if (length < countLength)
    //    {
    //        ns.Close();
    //        fs.Close();
    //        request.Abort();
    //        showErrorTips(downloadUrl, saveFullName);
    //    }
    //    else
    //    {
    //        ns.Close();
    //        fs.Close();
    //        request.Abort();
    //        showInstallTips(saveFullName);
    //    }
    //}
}

