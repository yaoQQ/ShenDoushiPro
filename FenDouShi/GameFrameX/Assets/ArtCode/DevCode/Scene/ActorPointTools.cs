using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorPointTools : MonoBehaviour
{
    //   左          右
    //   4    1      7   10     row_far
    //   5    2      8   11     row_mid
    //   6    3      9   12     row_near
    //   out  in
    public List<Transform> actorPoints = new List<Transform>();

    [OnValueChanged("LogicPoint"), LabelText("旋转角度"),Range(0,180)]
    public float rotate = 0f; // 旋转角度

    [Header("列设置")]
    [OnValueChanged("LogicPoint"),LabelText("内侧")]
    public Vector2 column_inside = new Vector2();  // 左列/右列内部偏移
    [OnValueChanged("LogicPoint"), LabelText("外侧")]
    public Vector2 column_outside = new Vector2(); // 左列/右列外部偏移

    [Header("行设置")]
    [OnValueChanged("LogicPoint"), LabelText("远")]
    public Vector2 row_far = new Vector2();  // 远排行偏移
    [OnValueChanged("LogicPoint"), LabelText("中")]
    public Vector2 row_mid = new Vector2();  // 中排行偏移
    [OnValueChanged("LogicPoint"), LabelText("近")]
    public Vector2 row_near = new Vector2(); // 近排行偏移

    public bool showGizmos = true; // 是否显示Gizmos

    public void LogicPoint()
    {
        if (actorPoints.Count != 12)
        {
            Debug.LogError("ActorPointTools: actorPoints count is not 12");
            return;
        }

        // 左半边点位 (1-6)
        // 第1排 (左上)
        actorPoints[0].localPosition = new Vector3(-column_inside.x, 0, row_far.y);   // 点1 (左内+远排)
        actorPoints[3].localPosition = new Vector3(-column_outside.x, 0, row_far.y);  // 点4 (左外+远排)

        // 第2排 (左中)
        actorPoints[1].localPosition = new Vector3(-column_inside.x, 0, row_mid.y);    // 点2 (左内+中排)
        actorPoints[4].localPosition = new Vector3(-column_outside.x, 0, row_mid.y);  // 点5 (左外+中排)

        // 第3排 (左下)
        actorPoints[2].localPosition = new Vector3(-column_inside.x, 0, row_near.y);   // 点3 (左内+近排)
        actorPoints[5].localPosition = new Vector3(-column_outside.x, 0, row_near.y);  // 点6 (左外+近排)

        // 右半边点位 (7-12)
        // 第1排 (右上)
        actorPoints[6].localPosition = new Vector3(column_inside.x, 0, row_far.y);     // 点7 (右内+远排)
        actorPoints[9].localPosition = new Vector3(column_outside.x, 0, row_far.y);    // 点10 (右外+远排)

        // 第2排 (右中)
        actorPoints[7].localPosition = new Vector3(column_inside.x, 0, row_mid.y);     // 点8 (右内+中排)
        actorPoints[10].localPosition = new Vector3(column_outside.x, 0, row_mid.y);    // 点11 (右外+中排)

        // 第3排 (右下)
        actorPoints[8].localPosition = new Vector3(column_inside.x, 0, row_near.y);    // 点9 (右内+近排)
        actorPoints[11].localPosition = new Vector3(column_outside.x, 0, row_near.y);   // 点12 (右外+近排)


        // 旋转所有点位
        actorPoints[0].localRotation= Quaternion.Euler(0, rotate, 0); // 旋转中心点
        actorPoints[1].localRotation = Quaternion.Euler(0, rotate, 0); // 旋转中心点
        actorPoints[2].localRotation = Quaternion.Euler(0, rotate, 0); // 旋转中心点
        actorPoints[3].localRotation = Quaternion.Euler(0, rotate, 0); // 旋转中心点
        actorPoints[4].localRotation = Quaternion.Euler(0, rotate, 0); // 旋转中心点
        actorPoints[5].localRotation = Quaternion.Euler(0, rotate, 0); // 旋转中心点

        actorPoints[6].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点
        actorPoints[7].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点
        actorPoints[8].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点
        actorPoints[9].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点
        actorPoints[10].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点
        actorPoints[11].localRotation = Quaternion.Euler(0, -rotate, 0); // 旋转中心点

    }

    // 编辑器可视化
    void OnDrawGizmos()
    {
        if (actorPoints.Count != 12 || !showGizmos) return;

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.fontSize = 50;
        style.fontStyle = FontStyle.Bold;
        style.alignment = TextAnchor.MiddleCenter;

        for (int i = 0; i < actorPoints.Count; i++) {
            if (actorPoints[i] != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(actorPoints[i].position, 0.1f);
                UnityEditor.Handles.Label(actorPoints[i].position, i.ToString(), style);
            }
        }
  

        // 绘制连接线
        Gizmos.color = Color.white;
        DrawLine(0, 3); // 左内1 - 左外4
        DrawLine(1, 4); // 左内2 - 左外5
        DrawLine(2, 5); // 左内3 - 左外6

        DrawLine(6, 9); // 右内7 - 右外10
        DrawLine(7, 10); // 右内8 - 右外11
        DrawLine(8, 11); // 右内9 - 右外12

        // 绘制排线
        Gizmos.color = Color.green;
        DrawLine(0, 6); // 远排左1 - 远排右7
        DrawLine(1, 7); // 中排左2 - 中排右8
        DrawLine(2, 8); // 近排左3 - 近排右9
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