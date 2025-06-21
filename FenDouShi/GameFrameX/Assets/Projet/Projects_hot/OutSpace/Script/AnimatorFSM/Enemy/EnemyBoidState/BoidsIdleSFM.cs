using UnityEngine;
using System.Collections;

public class BoidsIdleSFM : SceneLinkedSMB<EnemyBoidsMotionBehaviour>
{
    public override void OnStart(Animator animator)
    {
        base.OnStart(animator);
        m_MonoBehaviour.initAnimator();
    }

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        m_MonoBehaviour.delayTime = 2;
      
    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        if (!m_MonoBehaviour.isInitComplete)
        {
            return;
        }
        m_MonoBehaviour.delayTime -= Time.deltaTime;
        if (m_MonoBehaviour.delayTime <= 0)
        {
            m_MonoBehaviour.delayTime = 2;
            m_MonoBehaviour.isMotionComplete = m_MonoBehaviour.IsMonsterCompeletMotion(5);
            Logger.PrintDebug(" m_MonoBehaviour.isMotionComplete=" + m_MonoBehaviour.isMotionComplete);
            if (m_MonoBehaviour.isMotionComplete)
            {
                if (m_MonoBehaviour.motionType == MotionType.CircleMotion)
                {
                    m_MonoBehaviour.TrigetShowCircleSMF();
                }
                else if(m_MonoBehaviour.motionType == MotionType.Pow2)
                {
                    m_MonoBehaviour.TrigetShowPowSMF();
                }
                else 
                {
                    m_MonoBehaviour.TrigetRotateCircleByPosSMF();
                }
            }
        }
    


    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
