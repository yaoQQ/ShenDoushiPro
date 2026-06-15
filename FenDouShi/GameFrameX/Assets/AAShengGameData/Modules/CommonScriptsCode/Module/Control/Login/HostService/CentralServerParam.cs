using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

//中央服务器请求类型
public enum RequestHostType
{
    version,
    CENTRAL_SERVER_URL,
    FULL_SERVER_LIST_URL,
    user_agreement,
    privacy_agreement,
    age_appropriate_remind,
    maintain_info,
    notice_list,
    notice,

    None
}
/// <summary>
/// 中央服务器参数管理类
/// 提供统一的API接口管理和调用机制
/// </summary>
public class CentralServerParam : Singleton<CentralServerParam>
{
    // 服务器基础URL
    private const string HOST = "http://192.168.2.230:9080";
    // 应用配置
    private const int APP_ID = 2;
    private const string CLIENT_SECRET = "89d74a905bdf3a11d8932b09eb74e842";

    // 玩家信息
    public const string PlayerNameKey = "PlayerNameKey";
    public static string PlayerName;

    // 请求超时时间(秒)
    private const int REQUEST_TIMEOUT = 10;

    // 事件回调
    public Action<UnityWebRequest> RequestCallback { get; set; }

    // API接口URL
    private readonly Dictionary<RequestHostType, string> _apiUrls = new Dictionary<RequestHostType, string>{
        {RequestHostType.version, HOST + "/api/ver"},//   /**版本号 */
        {RequestHostType.CENTRAL_SERVER_URL, HOST + "/api/login"},   /**登录 */
        {RequestHostType.FULL_SERVER_LIST_URL, HOST + "/api/servers"},   /**服务器列表 */
        {RequestHostType.user_agreement, HOST + "/api/user_agreement"}, /**用户协议 */
        {RequestHostType.privacy_agreement, HOST + "/api/privacy_agreement"}, /**隐私协议 */
        {RequestHostType.age_appropriate_remind, HOST + "/api/age_appropriate_remind"}, /**适龄提示 */
        {RequestHostType.maintain_info, HOST + "/api/maintain_info"}, /**获取维护信息 */
        {RequestHostType.notice_list, HOST + "/api/notice_list"}, /**获取公告列表 */
        {RequestHostType.notice, HOST + "/api/notice"} /**获取公告详情 */
    };

    /// <summary>
    /// 生成MD5加密字符串
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>MD5加密后的字符串</returns>
    public static string GenerateMD5(string input)
    {
        using (MD5 md5 = MD5.Create())
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }



    /// <summary>
    /// 获取API接口URL
    /// </summary>
    /// <param name="apiName">API名称</param>
    /// <returns>完整的API URL</returns>
    private string GetApiUrl(RequestHostType apiName)
    {
        if (_apiUrls.TryGetValue(apiName, out string url))
            return url;

        Logger.PrintError($"未找到API: {apiName}");
        return string.Empty;
    }

    /// <summary>
    /// 构建中央服务器请求表单
    /// </summary>
    ///  <param name="RequestHostType">请求的中央服务器API</param>
    /// <param name="param">额外参数</param>
    /// <returns>构建好的WWWForm</returns>
    private WWWForm GetCentralServerForm(RequestHostType requestType = RequestHostType.None, string param = null)
    {
        WWWForm form = new WWWForm();
        form.AddField("app_id", APP_ID);
        form.AddField("user_id", PlayerName);
        // 生成签名
        string sign = GenerateMD5($"{APP_ID}{PlayerName}{CLIENT_SECRET}").ToLower();
        form.AddField("sign", sign);

        // 添加固定参数
        form.AddField("sdk_bundleid", "");
        form.AddField("sdk_token", "");
        form.AddField("tck", "");


        // 添加额外参数
        if (requestType != RequestHostType.None)
        {
            //特殊点，请求列表需要加page参数
            if (requestType == RequestHostType.FULL_SERVER_LIST_URL && !string.IsNullOrEmpty(param))
            {
                form.AddField("page", int.Parse(param));
            }
            else
            {
                form.AddField(requestType.ToString(), _apiUrls[requestType]);
            }

        }

        return form;
    }

    /// <summary>
    /// 请求协议文本
    /// </summary>
    ///  <param name="RequestHostType">请求的中央服务器API</param>
    /// <param name="param">额外参数</param>
    /// <returns>构建好的WWWForm</returns>
    private WWWForm GetCentralText(RequestHostType requestType = RequestHostType.None, string param = null)
    {
        WWWForm form = new WWWForm();

        // 生成签名
        string sign = GenerateMD5($"{APP_ID}{CLIENT_SECRET}").ToLower();
        form.AddField("app_id", APP_ID);
        form.AddField("sign", sign);


        // 获取公告信息
        if (requestType == RequestHostType.notice)
        {
            if (string.IsNullOrEmpty(param))
            {
               Logger.PrintError($"未找到公告ID param="+ param);
                return form;
            }
            form.AddField("id", int.Parse(param));
        }
        // 获取维护信息
        else if (requestType == RequestHostType.maintain_info)
        {
            if (string.IsNullOrEmpty(param))
            {
                Logger.PrintError($"未找到公告ID param=" + param);
                return form;
            }
            form.AddField("sid", int.Parse(param));
        }

        return form;
    }

    /// <summary>
    /// 发送请求到中央服务器
    /// </summary>
    /// <param name="apiName">API名称</param>
    /// <param name="extraParams">额外参数</param>
    /// <param name="callback">请求完成后的回调</param>
    public void SendRequest(RequestHostType requestType, string param = null, Action<UnityWebRequest> callback = null)
    {
        string url = GetApiUrl(requestType);
        if (string.IsNullOrEmpty(url))
            return;

        WWWForm form = GetCentralServerForm(requestType, param);
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.timeout = REQUEST_TIMEOUT;
        MainThread.Instance.StartCoroutine(SendRequestCoroutine(request, callback));
    }
    /// <summary>
    /// 发送请求到中央服务器
    /// </summary>
    /// <param name="apiName">API名称</param>
    /// <param name="extraParams">额外参数</param>
    /// <param name="callback">请求完成后的回调</param>
    public void SendRequTextRequest(RequestHostType requestType, string param = null, Action<UnityWebRequest> callback = null)
    {
        string url = GetApiUrl(requestType);
        if (string.IsNullOrEmpty(url))
            return;

        WWWForm form = GetCentralText(requestType, param);
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.timeout = REQUEST_TIMEOUT;
        MainThread.Instance.StartCoroutine(SendRequestCoroutine(request, callback));
    }
    private IEnumerator SendRequestCoroutine(UnityWebRequest request, Action<UnityWebRequest> callback)
    {
        yield return request.SendWebRequest();

        // 调用回调
        callback?.Invoke(request);
        RequestCallback?.Invoke(request);

        // 释放资源
        request.Dispose();
    }



}
