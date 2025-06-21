using UnityEngine;

public class ResourcePoolEnable : MonoBehaviour
{
	[HideInInspector]
	public string prefabName;

    //对象池重新获取激活调用
    public virtual void Active()
    {

    }

    //在Active之后调用
    public virtual void AfterActive()
    {

    }

}

