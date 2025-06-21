
using UnityEngine;
public class CashBossMoveSMF : SceneLinkedSMB<CashBossMonoBehaviours>
{


    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        m_MonoBehaviour.Move();
        m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed;


    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        m_MonoBehaviour.MoveToPos();
        float disToPlay= m_MonoBehaviour.UpdateToPlayerDistance();
        if (disToPlay - m_MonoBehaviour.attrackDistance <= 2)
        {
            if (!m_MonoBehaviour.bossCreateEnemy.gameObject.activeSelf)
            {
                m_MonoBehaviour.bossCreateEnemy.gameObject.SetActive(true);
                m_MonoBehaviour.bossCreateEnemy2.gameObject.SetActive(true);
                m_MonoBehaviour.attrackLight.gameObject.SetActive(true);
            }
        }
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
