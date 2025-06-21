using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comet : MonoBehaviour
{
    public GameObject[] star;
    public Vector3 Target;
    public GameObject CometPrefab;
    public float CometDistance=2;
    public float StartDistance = 2;
    public float CometDestroyTime = 4f;
    public float StarDestroyTime = 10f;
    float CheckTime = 0;
    public float ScaleNum = 1;
    public int ChanceNum=8;
    public int CometNum = 4;
    private Quaternion m_rotation;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RandomEvent();
    }

    public Vector3 findPlayerCirclePos2(float radius, Vector3 Target)
    {
        Vector3 playerPos = Target;
        float angle = UnityEngine.Random.Range(0, 360);
        float x = playerPos.x + radius * Mathf.Cos(angle * 3.14f / 360f);
        float y = playerPos.y + radius * Mathf.Sin(angle * 3.14f / 360f);
        float z = playerPos.z + radius * Mathf.Sin(angle * 3.14f / 360f);
        return new Vector3(x, y, z);
    }


    public void CreateComet(Vector3 Target, GameObject CometPrefab, Quaternion Cometrotation,float Time)
    {
       var obj= Instantiate(CometPrefab, Target, Cometrotation);
        GameObject.Destroy(obj, Time);
    }

    public void CreateManyComets(Vector3 Target, GameObject CometPrefab, int CometNum, float Time,float Distance)
    {
        var direction = Random.Range(0f, 1f);
        float min;
        float max;
        if (direction > 0.5f)
        {
            min = -15;
            max = 15;
        }
        else
        {
            min = -190;
            max = -170;
        }
        var Temp = findPlayerCirclePos2(Distance, Target);
        m_rotation = Quaternion.Euler(new Vector3(Random.Range(min, max), Random.Range(0, 360), Random.Range(0, 360)));
        for (int temp= CometNum; temp>0;temp--)
        {
            Vector3 position = new Vector3(Temp.x + Random.Range(-3.0f, 3.0f) * ScaleNum, Temp.y + Random.Range(-3.0f, 3.0f) * ScaleNum, Temp.z + Random.Range(-3.0f, 3.0f) * ScaleNum);
            CreateComet(position, CometPrefab, m_rotation, Time);
        }
           
       
    }

    public void RandomEvent()
    {
       
        if (CheckTime <= 3)
        {
            CheckTime += Time.deltaTime;
            return;
        }
        else
        {
            var num = Random.Range(0, 10);
            if (num > 10 - ChanceNum)
            {
                CreateManyComets(Target, CometPrefab, 4, CometDestroyTime, CometDistance);

            }
            else if (num > 10 - ChanceNum / 2)
            {
                CreateManyComets(Target, CometPrefab, 1, CometDestroyTime, CometDistance);

            }
            else if(num>5) 
            {
                var StarNum = Random.Range(0, star.Length);
                CreateManyComets(Target, star[StarNum], 1, StarDestroyTime, StartDistance);
            }
           
            CheckTime = 0;
        }
    }




}
