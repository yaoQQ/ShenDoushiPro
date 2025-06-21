using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MyUtils
{

    public static float modelScale = 0.01f;
    public static float bulletScale = 0.1f;

    public static float ArkitScale = 0.1f;
    public static int MaxEnemyDistance =12;

    public static int enemyLayer = LayerMask.NameToLayer("Enemy");
    public static int collderLayer=LayerMask.NameToLayer("Collider");
    public static int playerLayer= LayerMask.NameToLayer("Character");

    public static GameObject LoadGunPrefab(string path)
    {
        Transform parent = OutSpaceCameraManager.Instance.Player;
        GameObject obj = OutSpaceResourceManager.Instance.getPrefabDirect(path);
        GameObject playerGunParent = GameObject.Instantiate(obj) as GameObject;
        if (parent != null)
        {
            playerGunParent.transform.parent = parent;
        }
        playerGunParent.name = path;
        playerGunParent.transform.localPosition = obj.transform.localPosition;
        playerGunParent.transform.localRotation = obj.transform.localRotation;
        playerGunParent.transform.localScale = obj.transform.localScale;
        return playerGunParent;

    }
    public static void LoadSprite(string path,Action<Sprite> callBack)
    {
         OutSpaceResourceManager.Instance.GetSpriteByName(path, callBack);
    }
    public static GameObject LoadModelPrefab(string path)
    {
        GameObject monster = ResourceManagerPool.Instance.GetPoolObject(path, ResourceType.ship);
        if (GunManager.Ships != null)//默认子弹父类
            monster.transform.parent = GunManager.Ships.transform;
        monster.name = path;
        return monster;
    }

    public static GameObject LoadEffectPrefab(string path, bool isLocal=false)
    {
        GameObject effectObj = ResourceManagerPool.Instance.GetPoolObject(path, ResourceType.effect);
        if (effectObj == null)
        {
            return null;
        }
        if (GunManager.Effects != null)
        {//默认子弹父类
            //Debug.Log("effectObj=" + effectObj);
            effectObj.transform.parent = GunManager.Effects.transform;
        }
        effectObj.name = path;
        if (effectObj != null)
        {
            EffectItem effect = effectObj.GetComponent<EffectItem>();
            if (effect != null)
                effect.disActive();
        }
        return effectObj;
    }

    public static GameObject LoadBulletPrefab(string bulletName)
    {

        GameObject gunBullet = ResourceManagerPool.Instance.GetPoolObject(bulletName, ResourceType.bullet,true);
        if (gunBullet == null)
        {
            return null;
        }
        if (GunManager.Bullets != null)
        {//默认子弹父类
         //   Debug.Log(bulletName+" gunBullet=" + gunBullet);
            gunBullet.transform.parent = GunManager.Bullets.transform;
        }
        gunBullet.name = bulletName;
        return gunBullet;
    }
  

    

    ////根据当前位置查找，玩家一个圆弧位置
    public static Vector3 findPlayerCirclePos(Vector3 currPos, float radius)
    {
        Transform player = OutSpaceCameraManager.Instance.Player;
        Vector3 playerPos = player.position;
       Vector3 pos= playerPos + Vector3.up * 0.5f;
        Vector3 targetForword = Vector3.Normalize(currPos - pos);
        float x = UnityEngine.Random.Range(-3,3);
     //   float z = UnityEngine.Random.Range(0,3);
        Vector3 target = targetForword * x;
        target.y= playerPos.y +  0.5f;
        return target;
    }
    ////根据当前位置查找，玩家一个圆弧位置
    public static Vector3 findPlayerCirclePos2(float radius, bool isBoss = false)
    {
        radius = isBoss ? 4 : radius;
        Vector3 playerPos = OutSpaceCameraManager.Instance.Player.position;
        float angle = UnityEngine.Random.Range(-180f, 180f);
        float x = playerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
        float z = playerPos.z + radius * Mathf.Sin(angle * 3.14f / 180f);

      //  float angleZ = UnityEngine.Random.Range(0f, 30f);
        float y = isBoss?0: UnityEngine.Random.Range(0f, 0.8f);
        return new Vector3(x,y,z);
    }
    public static Vector3 findPlayeHeadrCircleHenPos(float radius,float angelT, bool isBoss = false)
    {
        radius = isBoss ? 4 : radius;
       // Debug.Log("findPlayeHeadrCircleHenPos radius=" + radius);
        Vector3 playerPos = OutSpaceCameraManager.Instance.Player.position;
        float angle = angelT;
        float x = playerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
        float y = playerPos.z + radius * Mathf.Sin(angle * 3.14f / 180f);

        //  float angleZ = UnityEngine.Random.Range(0f, 30f);
        float z = isBoss ? 0 : UnityEngine.Random.Range(0f, 0.8f);
        return new Vector3(x, playerPos.y, y);
    }
    public static Vector3 findPlayeHeadrCircleSuPos(float radius, float angelT, bool isBoss = false)
    {
        radius = isBoss ? 4 : radius;
        Vector3 playerPos = OutSpaceCameraManager.Instance.Player.position;
        float angle = angelT;
        float x = playerPos.x + radius * Mathf.Cos(angle * 3.14f / 180f);
        float y = playerPos.z + radius * Mathf.Sin(angle * 3.14f / 180f);

        //  float angleZ = UnityEngine.Random.Range(0f, 30f);
        float z = isBoss ? 0 : UnityEngine.Random.Range(0f, 0.8f);
        return new Vector3(x, y, radius);
    }
    //三点求角度
    public static float Angle(Vector3 cen, Vector3 first, Vector3 second)
    {
        const double M_PI = 3.1415926535897;

        double ma_x = first.x - cen.x;
        double ma_y = first.y - cen.y;
        double mb_x = second.x - cen.x;
        double mb_y = second.y - cen.y;
        double v1 = (ma_x * mb_x) + (ma_y * mb_y);
        double ma_val = Math.Sqrt(ma_x * ma_x + ma_y * ma_y);
        double mb_val = Math.Sqrt(mb_x * mb_x + mb_y * mb_y);
        double cosM = v1 / (ma_val * mb_val);
        double angleAMB = Math.Acos(cosM) * 180 / M_PI;

        return (float)angleAMB;
    }
    private int getIndex
    {
        get
        {
            return (UnityEngine.Random.Range(0, 3) > 1) ? 1 : -1; ;
        }
    }
    public static List<GameObject> CalculateEnemiesByDistance(List<EnemyShipBase> enemies, float distance, Vector3 explosionPos)
    {
        if (enemies == null)
        {
            return null;
        }
        List<GameObject> enemyList = new List<GameObject>();
        int size = enemies.Count;
        for (int cnt = 0; cnt < size; cnt++)
        {
            if (enemies[cnt] == null)
            {
                continue;
            }
            float temp = Vector3.Distance(enemies[cnt].transform.position, explosionPos);
            if (temp <= distance)
            {
                enemyList.Add(enemies[cnt].gameObject);
            }
        }
        return enemyList;
    }
    public static GameObject[] CalculateEnemiesByAngle(GameObject[] enemies, float angle, Transform player)
    {
        if (enemies == null)
        {
            return null;
        }
        List<GameObject> enemyList = new List<GameObject>();
        int size = enemies.Length;
        for (int cnt = 0; cnt < size; cnt++)
        {
            GameObject enemy;
            if ((enemy = CalculateEnemyByAngle(enemies[cnt], angle, player)) != null)
            {
                enemyList.Add(enemy);
            }
        }
        return enemyList.ToArray();
    }
    private static GameObject CalculateEnemyByAngle(GameObject enemy, float angle, Transform player)
    {
        Vector3 playerForward = player.transform.forward.normalized;
        Vector3 enemyDir = (enemy.transform.position - player.transform.position).normalized;
        float dotAngle = Vector3.Dot(playerForward, enemyDir);  // [-1, 1]
        float targetDotAngle = Mathf.Cos(Mathf.Deg2Rad * angle);
        if (angle < 90)
        {
            if (dotAngle >= targetDotAngle)
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }
        else if (angle == 90)
        {
            if (dotAngle >= 0)
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }
        else if (angle > 90)
        {
            if (dotAngle >= 0)
            {
                return enemy;
            }
            else if (dotAngle >= targetDotAngle)
            {
                return enemy;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public static string getEnemyTagByCamp(campEnum camp)
    {
        if (camp == campEnum.Enemy)
        {
            return OutSpaceTags.player;
        }
        else
        {
            return OutSpaceTags.Enemy;
        }
    }
    public static string getPrefabName(string cloneStr)
    {
        if (!string.IsNullOrEmpty(cloneStr))
        {
            string prefabName = cloneStr.Split("_")[0];
            return prefabName;
        }
        return null;
    }
    public static void ShowTimeCost(string name,Action fun)
    {
        if (fun != null)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            fun();
            sw.Stop();
            TimeSpan ts2 = sw.Elapsed;
            if (ts2.TotalMilliseconds > 200)
            {
                Logger.PrintColor("red", "[" + name + ":" + fun.Method.Name + "] Stopwatch总共花费:" + ts2.TotalMilliseconds + "ms.");
            }
        }
       // Stopwatch.TotalMilliseconds = 8.1153
    }

}
