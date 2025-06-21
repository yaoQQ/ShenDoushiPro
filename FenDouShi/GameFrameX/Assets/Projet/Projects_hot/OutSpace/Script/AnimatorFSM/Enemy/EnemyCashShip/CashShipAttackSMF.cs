using UnityEngine;
using System.Collections;

public class CashShipAttackSMF : SceneLinkedSMB<CashShipMonoBehaviour>
{

   

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        //  Logger.PrintColor("red", "进入攻击状态");
        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        
        m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed;
        m_MonoBehaviour.transform.LookAt(m_MonoBehaviour.target);
        //靠近玩家速度降低，提高反应时间
        m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed * 0.3f;

    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        Vector3 targetPos = m_MonoBehaviour.target.position;
        MoveToPosHit(animator,targetPos);

    }
    private void MoveToPosHit(Animator animator,Vector3 targetPos)
    {
        m_MonoBehaviour.transform.LookAt(targetPos);
        m_MonoBehaviour.transform.position = Vector3.MoveTowards(m_MonoBehaviour.transform.position, targetPos, m_MonoBehaviour.currSpeed * Time.deltaTime);
        //setState = ShipState.Move;
      //  float disToPlayer = m_MonoBehaviour.UpdateToPlayerDistance();
        //if (disToPlayer >= 1)
        //{
      //Logger.PrintColor("red", "攻击状态取消 转为Idle");
        //    m_MonoBehaviour.IdleState();
        //}
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
