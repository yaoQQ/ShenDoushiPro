using UnityEngine;
using UnityEditor;

public class EnemyShipFSMAttack : SceneLinkedSMB<EnemyMonoBehaviour>
{
    protected Vector3 m_AttackPosition;

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

       // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);

        m_AttackPosition = m_MonoBehaviour.target.transform.position;
        Vector3 toTarget = m_AttackPosition - m_MonoBehaviour.transform.position;
        toTarget.y = 0;

        m_MonoBehaviour.transform.forward = toTarget.normalized;
        //m_MonoBehaviour.controller.SetForward(m_MonoBehaviour.transform.forward);

        //if (m_MonoBehaviour.attackAudio != null)
        //    m_MonoBehaviour.attackAudio.PlayRandomClip();
    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

        //remainingToNextGrunt -= Time.deltaTime;

        //if (remainingToNextGrunt < 0)
        //{
        //    remainingToNextGrunt = Random.Range(minimumIdleGruntTime, maximumIdleGruntTime);
        //    m_MonoBehaviour.Grunt();
        //}

        //m_MonoBehaviour.FindTarget();
        //if (m_MonoBehaviour.target != null)
        //{
        //    m_MonoBehaviour.StartPursuit();
        //}
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);

        //if (m_MonoBehaviour.attackAudio != null)
        //    m_MonoBehaviour.attackAudio.audioSource.Stop();

        //m_MonoBehaviour.controller.SetFollowNavmeshAgent(true);
    }
}