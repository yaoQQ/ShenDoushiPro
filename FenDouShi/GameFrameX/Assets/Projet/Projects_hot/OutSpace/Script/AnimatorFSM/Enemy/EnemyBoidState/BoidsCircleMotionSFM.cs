using UnityEngine;
using System.Collections;

public class BoidsCircleMotionSFM : SceneLinkedSMB<EnemyBoidsMotionBehaviour>
{
    float attrackTime = 2;//攻击刷新时间
    public override void OnStart(Animator animator)
    {
        base.OnStart(animator);
        m_MonoBehaviour.initAnimator();
    }

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        m_MonoBehaviour.isMotionComplete = false;
        m_MonoBehaviour.delayTime = 2;
        Logger.PrintDebug("BoidsCircleMotionSFM OnSLStateEnter()");
    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

        //    m_MonoBehaviour.isMotionComplete = m_MonoBehaviour.IsMonsterCompeletMotion(5);
        m_MonoBehaviour.delayTime -= Time.deltaTime;
        if (m_MonoBehaviour.delayTime < 0)//3秒刷新一次
        {
            m_MonoBehaviour.isMotionComplete = m_MonoBehaviour.IsMonsterCompeletMotion(10);
 
            Debug.Log("m_MonoBehaviour.isMotionComplete=" + m_MonoBehaviour.isMotionComplete);
            m_MonoBehaviour.delayTime = 2;
        }
        if (m_MonoBehaviour.isMotionComplete)//动画完成
        {
            m_MonoBehaviour.RotateBoids();
            attrackTime -= Time.deltaTime;
            if (attrackTime <= 0)
            {
                m_MonoBehaviour.ToAttrick(10);
                attrackTime = 1;
                Logger.PrintDebug("m_MonoBehaviour.ToAttrick(10);");
            }
          //  Debug.Log("m_MonoBehaviour.RotateBoids（） m_MonoBehaviour.delayTime=" + m_MonoBehaviour.delayTime);
        }



    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
