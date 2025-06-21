
using UnityEngine;
using System.Collections;

public class BezierShipAttackSMF : SceneLinkedSMB<BezierShipMonoBehaviour>
{
    private Vector3 getWarningPlayerPos;
    private float reFreshTime=3f;
    private int warningDis = 2;
    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        getWarningPlayerPos = m_MonoBehaviour.target.position;
        //  Logger.PrintColor("red", "进入攻击状态");
        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        //m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed;
        //m_MonoBehaviour.transform.LookAt(m_MonoBehaviour.target);
        //靠近玩家速度降低，提高反应时间
        // m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed * 0.3f;

    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        m_MonoBehaviour.MoveAttrack();
        float disToPlayer = m_MonoBehaviour.UpdateToPlayerDistance();
        reFreshTime -= Time.deltaTime;
        if (reFreshTime <= 0)
        {
            reFreshTime = 3f;
            if (disToPlayer < this.warningDis) {

                getWarningPlayerPos = m_MonoBehaviour.target.position;
            }
            
        }

        //0.5f< <m_MonoBehaviour.attrackDistance
        if (m_MonoBehaviour.attrackDisMin<= disToPlayer&& disToPlayer <= m_MonoBehaviour.attrackDistance)
        {
            Vector3 direction = getWarningPlayerPos - m_MonoBehaviour.transform.position;
            if (Vector3.Dot(m_MonoBehaviour.transform.forward, direction) > 0) {
             //   Debug.Log("Position is in front of object");
                if (m_MonoBehaviour.gun != null)
                {
                    //瞄准记录点玩家目标点，开火
                    m_MonoBehaviour.gun.transform.LookAt(getWarningPlayerPos);
                    m_MonoBehaviour.gun.Shoot();
                }
            }
      
          
        }
    }
   

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
