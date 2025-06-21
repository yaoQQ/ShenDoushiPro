using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UIWidget;

public class CharacterInfoPanel : MonoBehaviour, IMiddleware
{
    public GameObject main;
    public Text characterInfoText;
    public ButtonWidget closeBtn;
    // Start is called before the first frame update
    void Awake()
    {
        main = this.gameObject;
        GameObject go = this.gameObject;
        characterInfoText = go.transform.Find("Viewport/Content/characterInfoText").GetComponent<Text>();
        closeBtn = go.transform.Find("CloseBg").GetComponent<ButtonWidget>();
    }
  
  
    public GameObject go
    {
        get
        {
            return this.main;
        }
    }

    public void DelReference()
    {
#if TOOL
#else
        if (main != null) GameObject.Destroy(main);
#endif
    }
}
