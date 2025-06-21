using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCreateEnemy : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemyList;
    public void ShowEnemy(){
        MonsterManager.Instance.createrMonsterByPos(enemy, this.transform.position, RandomBorePos());
    }
    private Vector3 RandomBorePos()
    {

        Vector3 initPos = this.transform.position;
        float x = initPos.x;
        float z = initPos.y;
        x = initPos.x + Random.Range(-0.2f, 0.2f);
        z = initPos.z + Random.Range(-0.2f, 0.2f);

        return new Vector3(x, initPos.y - 0.5f, z);
    }
}