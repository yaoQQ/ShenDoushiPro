using System;
using System.Collections.Generic;
using UnityEngine;


public class JsonExample : MonoBehaviour
{

    public string excelName = "Chat";
    public TextAsset itemfile;
    public TextAsset chatfile;
    //≥ı ºªØ
    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Type type = System.Type.GetType(excelName + "Vo");

        Debug.Log("type=" + type);
        ChatVo chatVoobj = Activator.CreateInstance(type) as ChatVo;
        Debug.Log("chatVoobj=" + chatVoobj);
    }

    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 100), "Test1"))
        {
            LoadExcel();
        }

        if (GUI.Button(new Rect(10, 200, 100, 100), "Test2"))
        {
            ConfigMgr.Instance.LoadConfigText(ConfigConst.Chat);
        }

        if (GUI.Button(new Rect(10, 250, 100, 100), "Test3"))
        {

            Dictionary<int, ChatVo> response = ConfigMgr.Instance.GetConfig<ChatVo>();
            Logger.PrintColor("yellow", "response=" + response);
        }
    }

    public void LoadChat()
    {
        Dictionary<string, ChatVo> response = DataTableFrame.CongfigUtility.Json.ToObject<Dictionary<string, ChatVo>>(chatfile.text);
        Debug.Log("file.text=" + chatfile.text);
        foreach (var pair in response)
        {
            ChatVo chat = pair.Value;
            //Debug.Log("chat.id=" + chat.Id + "  chat.name=" + chat.Name + "  chat.canSpeak=" + chat.CanSpeak);


        }

    }
    public void LoadExcel()
    {
        ConfigMgr.Instance.LoadConfigText("Item");

    }

}
