using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIWidget;

public class OutSpaceRadar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerImg;
    public IconWidget enenmyImg;
    private float delayUpdate = 1;
    private List<IconWidget> enemyPoolList = new List<IconWidget>();
    // key:enemy value:map
    private Dictionary<GameObject, IconWidget> enemyDic = new Dictionary<GameObject, IconWidget>();
    public int _score = 0;
    public int wave = 0;
    private float raDarScale=0.15f;//x:0.6f~-0.6f z:0.6f~-0.6f ,地图boss飞船最远距离4,敌机2
    private int countScore = 150;
    private Transform player;

    private const int maxPoolNum = 350;
    void Awake()
    {
        if (PlayerImg == null)
        {
            Logger.PrintError("OutSpaceRadar palyerMap");
        }
        if (enenmyImg == null)
        {
            Logger.PrintError("OutSpaceRadar 没有设置enemyMap");
        }
        player = OutSpaceCameraManager.Instance.MainCamera.transform;
    }

    void Update()
    {
        delayUpdate--;
        if (delayUpdate > 0)
        {
            return;
        }
        foreach (KeyValuePair<GameObject, IconWidget> pair in enemyDic)
        {
            worldPosToMap(pair.Key, pair.Value);
        }
        delayUpdate = 1;
        PlayerImg.transform.localEulerAngles=Vector3.up* player.localEulerAngles.y;
    }
    private int totalNum = 0;
   
    public void createEnemy(EnemyShipBase enemy)
    {
        totalNum++;
        GameObject enenmyImgObj;
        if (enemyPoolList.Count <= 0)
        {

            enenmyImgObj = GameObject.Instantiate(enenmyImg.gameObject);
            enenmyImgObj.transform.transform.SetParent(enenmyImg.transform.parent);
            enenmyImgObj.transform.localScale = enenmyImg.transform.localScale;
           
        }
        else
        {
            enenmyImgObj = enemyPoolList[0].gameObject;
            enemyPoolList.RemoveAt(0);
            //Debug.Log("enemyPoolList.count=" + enemyPoolList.Count);
        }
       
        enenmyImgObj.SetActive(true);
        IconWidget icon = enenmyImgObj.GetComponent<IconWidget>();
        if (!enemyDic.ContainsKey(enemy.gameObject))
        {
            enenmyImgObj.name = enemy.gameObject.name + "_" + totalNum;
            enemyDic.Add(enemy.gameObject, icon);
        }
        if (enemy.shipType == EnemyShipType.Asteriod)
        {
            icon.ChangeIcon(0);
        }
        else
        {
            icon.ChangeIcon(1);
        }
        worldPosToMap(enemy.gameObject, icon);
    }

    //敌方不动，玩家图标璇转动方式
    private void worldPosToMap(GameObject enemyObj, IconWidget icon)
    {

        if (enemyObj == null)
            return;
        if (icon == null)
            return;

        Vector3 offset = enemyObj.transform.position - player.position;
        //  offset *= 1 / 2f;
        
        offset *= raDarScale;

        icon.transform.localPosition = new Vector3(-offset.x, 0.5f, -offset.z);
    }
   
    public void destroy(GameObject enemyObj)
    {
        if (enemyDic.ContainsKey(enemyObj))
        {
            enemyDic[enemyObj].gameObject.SetActive(false);
            if (enemyPoolList.Count <= maxPoolNum)
            {
                enemyPoolList.Add(enemyDic[enemyObj]);
            }
            enemyDic.Remove(enemyObj);

        }
    }
    public void addScore(int score)
    {
        _score += score;
       
        if (_score >= countScore * (wave + 1))
        {
            wave++;
            return;
        }
    }


}
