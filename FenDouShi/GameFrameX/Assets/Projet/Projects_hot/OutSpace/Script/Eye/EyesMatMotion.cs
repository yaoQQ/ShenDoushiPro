using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesMatMotion : MonoBehaviour
{
    public Material eyesMat;
    public Vector2 iniTiling;//x :3.05~ 39.48     y:0.73~1.24
    public Vector2 iniOffset;

    public float DelayTime = 1;
    [SerializeField]
    private float _DelayTimeCount = 0;
    public bool isShow = true;
    // Start is called before the first frame update
    void Start()
    {
        if (eyesMat == null)
        {
            MeshRenderer render = this.GetComponent<MeshRenderer>();
            eyesMat = render.material;
        }
        init();
        _DelayTimeCount = DelayTime;
        _closeEyeTimeCount = closeEyeTime;
    }

    private void init()
    {
        targetX = 37;
        targetY = 1;
        eyesMat.SetTextureScale("_BaseMap", new Vector2(targetX, targetY));
    }
    // Update is called once per frame
    private float targetX = 0;
    private float targetY = 0;
    private float currX = 0;
    private float currY = 0;
    public float speed;
    public float closeEyeTime = 8;
    [SerializeField]
    private float _closeEyeTimeCount = 0;
    private void OnDisable()
    {
        isShow = false;
    }
    void Update()
    {
        if (!isShow)
        {
            return;
        }
        _DelayTimeCount -= Time.deltaTime;
        _closeEyeTimeCount -= Time.deltaTime;
        speed -= Time.deltaTime;
        if (speed < 1)
        {
            speed = 1;
        }
        if (_DelayTimeCount <= 0&& _closeEyeTimeCount > 0)
        {
            targetX = Random.Range(3.05f, 6.7f);
            targetY = Random.Range(0.9f, 1.25f);
            _DelayTimeCount = DelayTime;
        
        }
        if (_closeEyeTimeCount <= 0)
        {
            if (19 - currX < 0.1f)
            {
                _closeEyeTimeCount = closeEyeTime;
            }
            if (targetX != 37)
            {
                ShowCloseEye();
            }

           
        }
        ShowEyeMove(Mathf.Lerp(currX, targetX, speed*Time.deltaTime), Mathf.Lerp(currY, targetY, speed * Time.deltaTime));

        // eyesMat.SetTextureOffset("_MainTex", new Vector2(1, 1));
    }

    private void ShowEyeMove(float x,float y)
    {
        currX = x;
        currY = y;
        eyesMat.SetTextureScale("_BaseMap",new Vector2(currX,currY));
    }
    private void ShowCloseEye()
    {
        speed = 10;
        targetX = 37;
        targetY = 1;
    }
    private void OnBecameVisible()
    {
        isShow = true;
        //  Logger.PrintColor("red", isShow+"在视野内 this.game.name=" +this.gameObject.name);
    }
    private void OnBecameInvisible()
    {
        isShow = false;
       // Logger.PrintColor("red", isShow + "不在视野内 this.game.name=" + this.gameObject.name);
    }

}
