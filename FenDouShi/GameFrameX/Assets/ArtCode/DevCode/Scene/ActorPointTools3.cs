using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorPointTools3 : MonoBehaviour
{
    private const int POINT_COUNT = 12; // 点位数量

    public List<Transform> actorPoints = new List<Transform>();

    [OnValueChanged("LogicPoint"), LabelText("旋转角度"), Range(0, 360)]
    public float rotate = 0f;

    [OnValueChanged("LogicPoint"), LabelText("缩放比例")]
    public float scale = 1.0f;

    [Header("列设置")]
    [OnValueChanged("LogicPoint"), LabelText("内侧")]
    public Vector3 column_inside = new Vector3();

    [OnValueChanged("LogicPoint"), LabelText("外侧")]
    public Vector3 column_outside = new Vector3();

    [Header("行设置")]
    [OnValueChanged("LogicPoint"), LabelText("1")]
    public Vector3 row_1 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("2")]
    public Vector3 row_2 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("3")]
    public Vector3 row_3 = new Vector3();


    [Serializable]
    public class V2Item
    {

        public string name;
        public Vector3 value;
        public V2Item(string name, Vector3 value)
        {
            this.name = name;
            this.value = value;
        }
    }

    [Header("单个偏移")]
    [OnValueChanged("LogicPoint"), LabelText("1")]
    public Vector3 sing_1 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("2")]
    public Vector3 sing_2 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("3")]
    public Vector3 sing_3 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("4")]
    public Vector3 sing_4 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("5")]
    public Vector3 sing_5 = new Vector3();
    [OnValueChanged("LogicPoint"), LabelText("6")]
    public Vector3 sing_6 = new Vector3();

    [Header("单个缩放")]
    [OnValueChanged("LogicPoint"), LabelText("1")]
    public float sing_scale_1 = 1;
    [OnValueChanged("LogicPoint"), LabelText("2")]
    public float sing_scale_2 = 1;
    [OnValueChanged("LogicPoint"), LabelText("3")]
    public float sing_scale_3 = 1;
    [OnValueChanged("LogicPoint"), LabelText("4")]
    public float sing_scale_4 = 1;
    [OnValueChanged("LogicPoint"), LabelText("5")]
    public float sing_scale_5 = 1;
    [OnValueChanged("LogicPoint"), LabelText("6")]
    public float sing_scale_6 = 1;

    public bool showGizmos = true;

    [ListDrawerSettings(ShowIndexLabels = true)]
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
        if (actorPoints.Count != POINT_COUNT)
        {
            Debug.LogError("ActorPointTools: actorPoints count is not 20");
            return;
        }

        // 左半边点位 (1-10)
        // 第1排 
        actorPoints[0].localPosition = new Vector3(-column_inside.x - row_1.x, column_inside.y + row_1.y, column_inside.z + row_1.z);
        actorPoints[1].localPosition = new Vector3(-column_inside.x - row_2.x, column_inside.y + row_2.y, column_inside.z + row_2.z);
        actorPoints[2].localPosition = new Vector3(-column_inside.x - row_3.x, column_inside.y + row_3.y, column_inside.z + row_3.z);
        actorPoints[3].localPosition = new Vector3(-column_outside.x - row_1.x, column_outside.y + row_1.y, column_outside.z + row_1.z);
        actorPoints[4].localPosition = new Vector3(-column_outside.x - row_2.x, column_outside.y + row_2.y, column_outside.z + row_2.z);
        actorPoints[5].localPosition = new Vector3(-column_outside.x - row_3.x, column_outside.y + row_3.y, column_outside.z + row_3.z);

        actorPoints[6].localPosition = new Vector3(column_inside.x + row_1.x, column_inside.y + row_1.y, column_inside.z + row_1.z);
        actorPoints[7].localPosition = new Vector3(column_inside.x + row_2.x, column_inside.y + row_2.y, column_inside.z + row_2.z);
        actorPoints[8].localPosition = new Vector3(column_inside.x + row_3.x, column_inside.y + row_3.y, column_inside.z + row_3.z);
        actorPoints[9].localPosition = new Vector3(column_outside.x + row_1.x, column_outside.y + row_1.y, column_outside.z + row_1.z);
        actorPoints[10].localPosition = new Vector3(column_outside.x + row_2.x, column_outside.y + row_2.y, column_outside.z + row_2.z);
        actorPoints[11].localPosition = new Vector3(column_outside.x + row_3.x, column_outside.y + row_3.y, column_outside.z + row_3.z);


        // 单个偏移设置
        actorPoints[0].localPosition += new Vector3(sing_1.x, sing_1.y, sing_1.z);
        actorPoints[1].localPosition += new Vector3(sing_2.x, sing_2.y, sing_2.z);
        actorPoints[2].localPosition += new Vector3(sing_3.x, sing_3.y, sing_3.z);
        actorPoints[3].localPosition += new Vector3(sing_4.x, sing_4.y, sing_4.z);
        actorPoints[4].localPosition += new Vector3(sing_5.x, sing_5.y, sing_5.z);
        actorPoints[5].localPosition += new Vector3(sing_6.x, sing_6.y, sing_6.z);

        actorPoints[6].localPosition += new Vector3(-sing_1.x, sing_1.y, sing_1.z);
        actorPoints[7].localPosition += new Vector3(-sing_2.x, sing_2.y, sing_2.z);
        actorPoints[8].localPosition += new Vector3(-sing_3.x, sing_3.y, sing_3.z);
        actorPoints[9].localPosition += new Vector3(-sing_4.x, sing_4.y, sing_4.z);
        actorPoints[10].localPosition += new Vector3(-sing_5.x, sing_5.y, sing_5.z);
        actorPoints[11].localPosition += new Vector3(-sing_6.x, sing_6.y, sing_6.z);

        // 应用旋转
        actorPoints[0].localRotation = Quaternion.Euler(0, rotate, 0);
        actorPoints[1].localRotation = Quaternion.Euler(0, rotate, 0);
        actorPoints[2].localRotation = Quaternion.Euler(0, rotate, 0);
        actorPoints[3].localRotation = Quaternion.Euler(0, rotate, 0);
        actorPoints[4].localRotation = Quaternion.Euler(0, rotate, 0);
        actorPoints[5].localRotation = Quaternion.Euler(0, rotate, 0);

        actorPoints[6].localRotation = Quaternion.Euler(0, -rotate, 0);
        actorPoints[7].localRotation = Quaternion.Euler(0, -rotate, 0);
        actorPoints[8].localRotation = Quaternion.Euler(0, -rotate, 0);
        actorPoints[9].localRotation = Quaternion.Euler(0, -rotate, 0);
        actorPoints[10].localRotation = Quaternion.Euler(0, -rotate, 0);
        actorPoints[11].localRotation = Quaternion.Euler(0, -rotate, 0);

        // 应用缩放
        actorPoints[0].localScale = new Vector3(sing_scale_1 * scale, sing_scale_1 * scale, sing_scale_1 * scale);
        actorPoints[1].localScale = new Vector3(sing_scale_2 * scale, sing_scale_2 * scale, sing_scale_2 * scale);
        actorPoints[2].localScale = new Vector3(sing_scale_3 * scale, sing_scale_3 * scale, sing_scale_3 * scale);
        actorPoints[3].localScale = new Vector3(sing_scale_4 * scale, sing_scale_4 * scale, sing_scale_4 * scale);
        actorPoints[4].localScale = new Vector3(sing_scale_5 * scale, sing_scale_5 * scale, sing_scale_5 * scale);
        actorPoints[5].localScale = new Vector3(sing_scale_6 * scale, sing_scale_6 * scale, sing_scale_6 * scale);

        actorPoints[6].localScale = new Vector3(-sing_scale_1 * scale, sing_scale_1 * scale, sing_scale_1 * scale);
        actorPoints[7].localScale = new Vector3(-sing_scale_2 * scale, sing_scale_2 * scale, sing_scale_2 * scale);
        actorPoints[8].localScale = new Vector3(-sing_scale_3 * scale, sing_scale_3 * scale, sing_scale_3 * scale);
        actorPoints[9].localScale = new Vector3(-sing_scale_4 * scale, sing_scale_4 * scale, sing_scale_4 * scale);
        actorPoints[10].localScale = new Vector3(-sing_scale_5 * scale, sing_scale_5 * scale, sing_scale_5 * scale);
        actorPoints[11].localScale = new Vector3(-sing_scale_6 * scale, sing_scale_6 * scale, sing_scale_6 * scale);


    }

    void OnDrawGizmos()
    {
        if (actorPoints.Count != POINT_COUNT || !showGizmos) return;


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
                UnityEditor.Handles.Label(actorPoints[i].position + Vector3.up * 0.2f, (i + 1).ToString(), style);
            }
        }

        // 绘制列连接线 (白色)
        Gizmos.color = Color.white;
        DrawLine(0, 1);   // 点1-点8 (左内-左中)
        DrawLine(1, 2);    // 点8-点4 (左中-左外)

        DrawLine(3, 4);    // 点2-点9
        DrawLine(4, 5);    // 点9-点5



        //------
        DrawLine(6, 7);   // 点1-点8 (左内-左中)
        DrawLine(7, 8);    // 点8-点4 (左中-左外)

        DrawLine(9, 10);    // 点2-点9
        DrawLine(10, 11);    // 点9-点5

        // 绘制排连接线 (绿色)
        Gizmos.color = Color.green;
        DrawLine(3, 0);
        DrawLine(0, 6);
        DrawLine(6, 9);

        DrawLine(3 + 1, 0 + 1);
        DrawLine(0 + 1, 6 + 1);
        DrawLine(6 + 1, 9 + 1);

        DrawLine(3 + 2, 0 + 2);
        DrawLine(0 + 2, 6 + 2);
        DrawLine(6 + 2, 9 + 2);

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