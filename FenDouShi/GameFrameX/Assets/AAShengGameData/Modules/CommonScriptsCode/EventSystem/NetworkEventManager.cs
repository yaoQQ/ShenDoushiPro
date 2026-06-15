using UnityEngine;
using System.Collections.Generic;
//using ProtoBufSpace;


//public delegate void MessageReceive(uint protoID, byte[] info);
public class NetworkEventManager 
{

	protected static NetworkEventManager instance;
	
	public static NetworkEventManager Instance {
		get {
			if (instance == null) {
				instance = new NetworkEventManager ();
			}
			return instance;
		}
	}

	protected Dictionary<uint, MessageReceive> dicEventHandler = new Dictionary<uint, MessageReceive>();
	

	public void RegisterEventHandler(uint code, MessageReceive handler)
	{
        Logger.PrintLog("注册 协议Id===>>>  ",code.ToString());
		if (dicEventHandler.ContainsKey(code))
		{
			dicEventHandler[code] += handler;
		}
		else
		{
			dicEventHandler[code] = handler;
		}
	}

	public void RemoveEventHandler(uint code, MessageReceive handler)
	{
		if (dicEventHandler.ContainsKey(code))
		{
			dicEventHandler[code] -= handler;
		}
    }




    public void InvokeCallBack(uint protoID, byte[] ByteArray)
	{
		if (dicEventHandler.ContainsKey (protoID)) {
			if (dicEventHandler [protoID] != null) {
				dicEventHandler [protoID].Invoke (protoID, ByteArray);
			}
			else
			{
				Debug.Log(protoID+" 无监听");
			}
		} else {
			Debug.Log(protoID+" 无监听");
		}
	}

	public bool ContainsKey(uint code)
	{
		return dicEventHandler.ContainsKey(code);
	}
	
	public int Count { get { return this.dicEventHandler.Count; } }

	
	protected void reset()
	{
		this.dicEventHandler.Clear();
	}

}