using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour
{

 

    private float delayUpdate = 1;
    public Transform enenmyImg;
    public Transform PlayerImg;
    // key:enemy value:map
    private Dictionary<GameObject, GameObject> enemyList = new Dictionary<GameObject, GameObject>();
    private Text scoreText;
    public int _score = 0;
    public int wave = 0;
    private int countScore = 150;
    void Awake()
    {
       
        scoreText = this.transform.Find("ScoreText").GetComponent<Text>();
        PlayerImg = this.transform.Find("player");
        enenmyImg = this.transform.Find("enemyMask/enemy");
    }
    void Start()
    {
        addScore(0);
    }
    void Update()
    {
        delayUpdate--;
        if (delayUpdate > 0)
        {
            return;
        }
        foreach (KeyValuePair<GameObject, GameObject> pair in enemyList)
        {
            worldPosToMap(pair.Key, pair.Value);
        }
        delayUpdate = 1;
        PlayerImg.transform.localEulerAngles = new Vector3(0, 0, -OutSpaceCameraManager.Instance.Player.localEulerAngles.y);
    }
    private int totalNum = 0;
    public void createEnemy(GameObject enemy)
    {
        totalNum++;

        GameObject enenmyImgObj = GameObject.Instantiate(enenmyImg.gameObject);
        enenmyImgObj.transform.transform.parent = enenmyImg.transform.parent;
        enenmyImgObj.transform.localScale = Vector3.one;
        enenmyImgObj.name = enemy.name+"_"+ totalNum;
        enenmyImgObj.SetActive(true);
       // enemyStruct.enenmyImg = enenmyImg;
        if (!enemyList.ContainsKey(enemy))
        {
            enemyList.Add(enemy, enenmyImgObj);
        }
        worldPosToMap(enemy, enenmyImgObj);
    }

    //敌方不动，玩家图标璇转动方式
    private void worldPosToMap(GameObject enemyObj,GameObject enenmyImg)
    {

        GameObject obj = enemyObj;
        if (obj == null)
            return;
        if (enenmyImg == null)
            return;

        Vector3 pos = obj.transform.position;
        Vector3 targetPos = OutSpaceCameraManager.Instance.MainCamera.transform.position;


        Vector3 offset = pos - targetPos;
        //  offset *= 1 / 2f;
       
        offset /= MyUtils.ArkitScale;
        enenmyImg.transform.localPosition = new Vector3(offset.x, offset.z, 0);
    }
    //玩家视角不动，敌方旋转动方式
    private void worldPosToMap2(EnemyMap enemyStruct, bool isInit = false)
    {
        GameObject obj = enemyStruct.enemy;
        if (obj == null)
            return;
        if (enemyStruct.enenmyImg == null)
            return;

        Vector3 pos = obj.transform.position;
        Vector3 targetPos = OutSpaceCameraManager.Instance.MainCamera.transform.position;
        Vector3 offset = pos - targetPos;


        int add=1 ;

        Vector3 left = OutSpaceCameraManager.Instance.MainCamera.transform.TransformDirection(Vector3.right);
        Vector3 toOtherL = pos - targetPos;
        //if (Vector3.Dot(left, toOtherL) < 0)
        //{
        //    add = -1;
        //}
        //else
        //{
        //    add = 1;
        //}

        offset /= MyUtils.ArkitScale;
        float angel = MyUtils.Angle(getVec2ByVec3(targetPos), getVec2ByVec3(OutSpaceCameraManager.Instance.MainCamera.transform.forward), getVec2ByVec3(obj.transform.position));


        float distance = Vector3.Distance(targetPos, obj.transform.position);
        float x = distance * 10 * Mathf.Sin(add * angel * 3.14f / 180f);
        float y = distance * 10 * Mathf.Cos(angel * 3.14f / 180f);

        if (isInit)
        {
            enemyStruct.enenmyImg.transform.localPosition = new Vector3(x, y, 0);
        }
        else
        {
            enemyStruct.enenmyImg.transform.localPosition = Vector3.Lerp(enemyStruct.enenmyImg.transform.localPosition, new Vector3(x, y, 0), 2 * Time.deltaTime);
        }
        //enemyStruct.enenmyImg.transform.localPosition = new Vector3(offset.x, offset.z, 0);
        // Debug.Log(obj.name + "  angel=" + angel+"  x="+ x+"  y="+y+ "   add=" + add);
    }
    private Vector2 getVec2ByVec3(Vector3 vec)
    {
        return new Vector2(vec.x, vec.z);
    }
    public void destroy(GameObject enemyObj)
    {
        if (enemyList.ContainsKey(enemyObj))
        {
            GameObject.Destroy(enemyList[enemyObj]);
            enemyList.Remove(enemyObj);
           
        }
    }
    public void addScore(int score)
    {
        _score += score;
        scoreText.text = " Wave: "+ (wave+1) + "\nScore:" + _score;
        if (_score >= countScore * (wave + 1))
        {
            wave++;
            return;
        }
    }
}

