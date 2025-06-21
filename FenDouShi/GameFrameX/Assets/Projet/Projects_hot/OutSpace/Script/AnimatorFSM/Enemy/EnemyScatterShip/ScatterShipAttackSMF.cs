using UnityEngine;
using System.Collections;

public class ScatterShipAttackSMF : SceneLinkedSMB<ScatterShipMonoBehaviour>
{



    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        //  Logger.PrintColor("red", "进入攻击状态");
        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
     //   m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed;
        //m_MonoBehaviour.transform.LookAt(m_MonoBehaviour.target);
        //靠近玩家速度降低，提高反应时间
       // m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed * 0.3f;

    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        Vector3 targetPos = m_MonoBehaviour.target.position;
        MoveToPosShoot(animator, targetPos);

    }
    private void MoveToPosShoot(Animator animator, Vector3 targetPos)
    {
        m_MonoBehaviour.transform.LookAt(targetPos);
      //  m_MonoBehaviour.transform.position = Vector3.MoveTowards(m_MonoBehaviour.transform.position, targetPos, m_MonoBehaviour.currSpeed * Time.deltaTime);
        //setState = ShipState.Move;
        if (m_MonoBehaviour.gun != null)
        {
            m_MonoBehaviour.gun.transform.LookAt(targetPos);
           // m_MonoBehaviour.gun.transform.LookAt(OutSpaceCameraManager.Instance.Player.position);
           //  targetPos = OutSpaceCameraManager.Instance.Player.position;

            m_MonoBehaviour.gun.Shoot();
        }

        float disToPlayer = m_MonoBehaviour.UpdateToPlayerDistance();
        if (disToPlayer >= m_MonoBehaviour.attrackDistance)
        {

            m_MonoBehaviour.PlayerEscape();
        }
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
