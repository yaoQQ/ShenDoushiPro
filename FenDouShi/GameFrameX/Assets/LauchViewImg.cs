using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LauchViewImg : MonoBehaviour
{
    public Texture2D bgImg; 
    public Image bgImage;
	//初始化
	void Awake()
	{
        bgImage.sprite = Sprite.Create(bgImg, new Rect(0, 0, bgImg.width, bgImg.height), new Vector2(0.5f, 0.5f));

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
}
