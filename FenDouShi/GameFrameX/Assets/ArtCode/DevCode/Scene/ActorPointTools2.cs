using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorPointTools2 : MonoBehaviour
{
    //   左               右
    //        4                 14           row_1
    //   8         1      11          18     row_2
    //        5                 15           row_3
    //   9         2      12          19     row_4
    //        6                 16           row_5
    //   10        3      13          20     row_6
    //        7                 17           row_7 
    //   out  mid  in
    public List<Transform> actorPoints = new List<Transform>();

    [OnValueChanged("LogicPoint"), LabelText("旋转角度"), Range(0, 180)]
    public float rotate = 0f;

    [OnValueChanged("LogicPoint"), LabelText("缩放比例")]
    public float scale = 1.0f;


    [Header("列设置")]
    [OnValueChanged("LogicPoint"), LabelText("内侧")]
    public Vector2 column_inside = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("中间")]
    public Vector2 column_mid = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("外侧")]
    public Vector2 column_outside = new Vector2();

    [Header("行设置")]
    [OnValueChanged("LogicPoint"), LabelText("1")]
    public Vector2 row_1 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("2")]
    public Vector2 row_2 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("3")]
    public Vector2 row_3 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("4")]
    public Vector2 row_4 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("5")]
    public Vector2 row_5 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("6")]
    public Vector2 row_6 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("7")]
    public Vector2 row_7 = new Vector2();

    [Serializable]
    public class V2Item
    {

        public string name;
        public Vector2 value;
        public V2Item(string name, Vector2 value)
        {
            this.name = name;
            this.value = value;
        }
    }

    [Header("单个偏移")]
    [OnValueChanged("LogicPoint"), LabelText("1")]
    public Vector2 sing_1 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("2")]
    public Vector2 sing_2 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("3")]
    public Vector2 sing_3 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("4")]
    public Vector2 sing_4 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("5")]
    public Vector2 sing_5 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("6")]
    public Vector2 sing_6 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("7")]
    public Vector2 sing_7 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("8")]
    public Vector2 sing_8 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("9")]
    public Vector2 sing_9 = new Vector2();
    [OnValueChanged("LogicPoint"), LabelText("10")]
    public Vector2 sing_10 = new Vector2();
   

    public bool showGizmos = true;

    [ListDrawerSettings( ShowIndexLabels = true)]
    public List<GameObject> actorPrefabs = new List<GameObject>();


    [Button("创建角色对象")]
    public void CreateObjBtn()
    {
        for (int i = 0; i < actorPoints.Count; i++)
        {
            if (actorPoints[i] == null)
            {
                Debug.LogError($"ActorPointTools: actorPoints[{i}] is null");
                continue;
            }

            var actorPrefab = actorPrefabs.Count > i ? actorPrefabs[i] : null;

            if (actorPrefab != null)
            {
                GameObject obj = Instantiate(actorPrefab);
                obj.name = $"ActorPoint_{i + 1}";
                obj.transform.SetParent(actorPoints[i]);
                obj.transform.localPosition = Vector3.zero; // 设置位置为点位位置
                obj.transform.localRotation = Quaternion.identity; // 设置旋转为默认
                obj.transform.localScale = Vector3.one; // 设置缩放为默认
            }
        }
    }

    [Button("清除角色对象")]
    public void ClearObjBtn()
    {
        for (int i = 0; i < actorPoints.Count; i++)
        {
            if (actorPoints[i] == null)
            {
                Debug.LogError($"ActorPointTools: actorPoints[{i}] is null");
                continue;
            }
            // 清除点位下的所有子对象
            for (int j = actorPoints[i].childCount - 1; j >= 0; j--)
            {
                Transform child = actorPoints[i].GetChild(j);
                if (child != null)
                {
                    DestroyImmediate(child.gameObject); // 使用DestroyImmediate在编辑器中立即删除
                }
            }
        }
        Debug.Log("ActorPointTools: Clear all actor objects.");
    }

    public void LogicPoint()
    {
        if (actorPoints.Count != 20)
        {
            Debug.LogError("ActorPointTools: actorPoints count is not 20");
            return;
        }

        // 左半边点位 (1-10)
        // 第1排 
        actorPoints[3].localPosition = new Vector3(-column_mid.x - row_1.x, 0, column_mid.y + row_1.y);  // 点4 (左中+row1)

        // 第2排 (左中)
        actorPoints[0].localPosition = new Vector3(-column_inside.x - row_2.x, 0, column_inside.y + row_2.y);   // 点1 (左内+row2)
        actorPoints[7].localPosition = new Vector3(-column_outside.x - row_2.x, 0, column_outside.y + row_2.y); // 点8 (左外+row2)

        // 第3排
        actorPoints[4].localPosition = new Vector3(-column_mid.x - row_3.x, 0, column_mid.y + row_3.y);   // 点5 (左中+row3)

        // 第4排
        actorPoints[1].localPosition = new Vector3(-column_inside.x - row_4.x, 0, column_inside.y + row_4.y);    // 点2 (左内+row4)
        actorPoints[8].localPosition = new Vector3(-column_outside.x - row_4.x, 0, column_outside.y + row_4.y);  // 点9 (左外+row4)

        // 第5排
        actorPoints[5].localPosition = new Vector3(-column_mid.x - row_5.x, 0, column_mid.y + row_5.y);   // 点6 (左中+row5)

        // 第6排
        actorPoints[2].localPosition = new Vector3(-column_inside.x - row_6.x, 0, column_inside.y + row_6.y);    // 点3 (左内+row6)
        actorPoints[9].localPosition = new Vector3(-column_outside.x - row_6.x, 0, column_outside.y + row_6.y);  // 点10 (左外+row6)

        // 第7排 (仅外侧)
        actorPoints[6].localPosition = new Vector3(-column_mid.x - row_7.x, 0, column_mid.y + row_7.y);   // 点7 (左中+row7)

        // 右半边点位 (11-20)
        // 第1排 
        actorPoints[13].localPosition = new Vector3(column_mid.x + row_1.x, 0, column_mid.y + row_1.y);  // 点14 (右中+row1)

        // 第2排 (右中)
        actorPoints[10].localPosition = new Vector3(column_inside.x + row_2.x, 0, column_inside.y + row_2.y);   // 点11 (右内+row2)
        actorPoints[17].localPosition = new Vector3(column_outside.x + row_2.x, 0, column_outside.y + row_2.y); // 点18 (右外+row2)

        // 第3排
        actorPoints[14].localPosition = new Vector3(column_mid.x + row_3.x, 0, column_mid.y + row_3.y);   // 点15 (右中+row3)

        // 第4排
        actorPoints[11].localPosition = new Vector3(column_inside.x + row_4.x, 0, column_inside.y + row_4.y);    // 点12 (右内+row4)
        actorPoints[18].localPosition = new Vector3(column_outside.x + row_4.x, 0, column_outside.y + row_4.y);  // 点19 (右外+row4)

        // 第5排
        actorPoints[15].localPosition = new Vector3(column_mid.x + row_5.x, 0, column_mid.y + row_5.y);   // 点16 (右中+row5)

        // 第6排
        actorPoints[12].localPosition = new Vector3(column_inside.x + row_6.x, 0, column_inside.y + row_6.y);    // 点13 (右内+row6)
        actorPoints[19].localPosition = new Vector3(column_outside.x + row_6.x, 0, column_outside.y + row_6.y);  // 点20 (右外+row6)

        // 第7排 (仅外侧)
        actorPoints[16].localPosition = new Vector3(column_mid.x + row_7.x, 0, column_mid.y + row_7.y);   // 点17 (右中+row7)

        // 单个偏移设置
        actorPoints[0].localPosition += new Vector3(sing_1.x, 0, sing_1.y);
        actorPoints[1].localPosition += new Vector3(sing_2.x, 0, sing_2.y);
        actorPoints[2].localPosition += new Vector3(sing_3.x, 0, sing_3.y);
        actorPoints[3].localPosition += new Vector3(sing_4.x, 0, sing_4.y);
        actorPoints[4].localPosition += new Vector3(sing_5.x, 0, sing_5.y);
        actorPoints[5].localPosition += new Vector3(sing_6.x, 0, sing_6.y);
        actorPoints[6].localPosition += new Vector3(sing_7.x, 0, sing_7.y);
        actorPoints[7].localPosition += new Vector3(sing_8.x, 0, sing_8.y);
        actorPoints[8].localPosition += new Vector3(sing_9.x, 0, sing_9.y);
        actorPoints[9].localPosition += new Vector3(sing_10.x, 0, sing_10.y);

        actorPoints[10].localPosition += new Vector3(-sing_1.x, 0, sing_1.y);
        actorPoints[11].localPosition += new Vector3(-sing_2.x, 0, sing_2.y);
        actorPoints[12].localPosition += new Vector3(-sing_3.x, 0, sing_3.y);
        actorPoints[13].localPosition += new Vector3(-sing_4.x, 0, sing_4.y);
        actorPoints[14].localPosition += new Vector3(-sing_5.x, 0, sing_5.y);
        actorPoints[15].localPosition += new Vector3(-sing_6.x, 0, sing_6.y);
        actorPoints[16].localPosition += new Vector3(-sing_7.x, 0, sing_7.y);
        actorPoints[17].localPosition += new Vector3(-sing_8.x, 0, sing_8.y);
        actorPoints[18].localPosition += new Vector3(-sing_9.x, 0, sing_9.y);
        actorPoints[19].localPosition += new Vector3(-sing_10.x, 0, sing_10.y);


        // 应用旋转
        for (int i = 0; i < actorPoints.Count; i++)
        {
            if (i < 10) // 左半边顺时针旋转
            {
                actorPoints[i].localRotation = Quaternion.Euler(0, rotate, 0);
                actorPoints[i].localScale = new Vector3(scale, scale, scale); // 应用缩放
            }
            else // 右半边逆时针旋转
            {
                actorPoints[i].localRotation = Quaternion.Euler(0, -rotate, 0);
                actorPoints[i].localScale = new Vector3(-scale, scale, scale); // 应用缩放
            }
        }
    }

    void OnDrawGizmos()
    {
        if (actorPoints.Count != 20 || !showGizmos) return;


        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.blue;
        style.fontSize = 40;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;

        // 绘制所有点
        for (int i = 0; i < actorPoints.Count; i++)
        {
            if (actorPoints[i] != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(actorPoints[i].position, 0.1f);
                UnityEditor.Handles.Label(actorPoints[i].position + Vector3.up * 0.2f, (i+1).ToString(), style);
            }
        }

        // 绘制列连接线 (白色)
        Gizmos.color = Color.white;
        DrawLine(0, 1);   // 点1-点8 (左内-左中)
        DrawLine(1, 2);    // 点8-点4 (左中-左外)

        DrawLine(3, 4);    // 点2-点9
        DrawLine(4, 5);    // 点9-点5
        DrawLine(5, 6);    // 点3-点10
        
        DrawLine(7, 8);    // 点10-点6
        DrawLine(8, 9);  // 点11-点18 (右内-右中)

        //------
        DrawLine(10, 11);  // 点18-点14 (右中-右外)
        DrawLine(11, 12);  // 点12-点19

        DrawLine(13, 14);  // 点19-点15
        DrawLine(14, 15);  // 点13-点20
        DrawLine(15, 16);  // 点20-点16

        DrawLine(17, 18);  // 点13-点20
        DrawLine(18, 19);  // 点20-点16

        // 绘制排连接线 (绿色)
        Gizmos.color = Color.green;
        DrawLine(3, 13); 

        DrawLine(7, 0);  
        DrawLine(0, 10); 
        DrawLine(10, 17);

        DrawLine(4, 14); 

        DrawLine(8, 1);  
        DrawLine(1, 11); 
        DrawLine(11, 18);

        DrawLine(5, 15); 
        
        DrawLine(9, 2);  
        DrawLine(2, 12); 
        DrawLine(12, 19);

        DrawLine(6, 16); 
    }

    void DrawLine(int indexA, int indexB)
    {
        if (indexA < actorPoints.Count && indexB < actorPoints.Count)
        {
            if (actorPoints[indexA] != null && actorPoints[indexB] != null)
            {
                Gizmos.DrawLine(
                    actorPoints[indexA].position,
                    actorPoints[indexB].position);
            }
        }
    }
}