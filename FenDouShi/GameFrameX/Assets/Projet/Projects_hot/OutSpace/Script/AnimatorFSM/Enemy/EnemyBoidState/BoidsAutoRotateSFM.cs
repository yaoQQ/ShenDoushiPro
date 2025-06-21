using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoidsAutoRotateSFM : SceneLinkedSMB<EnemyBoidsMotionBehaviour>
{
    float attrackTime = 1;//攻击刷新时间
    private List<Vector3> motionDataList;
    private float RotateSpeed=0.1f;
    private Quaternion rotation;
    List<EnemyShipBase> boids;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        m_MonoBehaviour.delayTime = 5;
         rotation = Quaternion.AngleAxis(RotateSpeed, Vector3.forward);
         boids = m_MonoBehaviour.boidsList;
    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        attrackTime -= Time.deltaTime;
        int boidsNum = boids.Count - 1;
        for (int i = boidsNum; i >= 0; i--)
        {
            if (boids[i].shipState == AIState.Dead)
            {
                continue;
            }
            boids[i].transform.position = rotation * boids[i].transform.position;
        }
        if (attrackTime <= 0)
        {
            m_MonoBehaviour.ToAttrick(1);
            Logger.PrintDebug("m_MonoBehaviour.ToAttrick(1);");
            attrackTime = 1;
            m_MonoBehaviour.delayTime -= attrackTime;
            if (m_MonoBehaviour.delayTime <= 0)
            {
                m_MonoBehaviour.CheckDestroy();
                m_MonoBehaviour.delayTime = 5;
            }
        }
        //  Debug.Log("m_MonoBehaviour.RotateBoids（） m_MonoBehaviour.delayTime=" + m_MonoBehaviour.delayTime);
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }


}