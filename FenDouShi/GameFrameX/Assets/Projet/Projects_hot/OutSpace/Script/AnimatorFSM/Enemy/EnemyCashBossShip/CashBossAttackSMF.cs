
using UnityEngine;
using System.Collections;

public class CashBossAttackSMF : SceneLinkedSMB<CashBossMonoBehaviours>
{



    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);
        //  Logger.PrintColor("red", "进入攻击状态");
        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed;
        m_MonoBehaviour.bossCreateEnemy.gameObject.SetActive(true);
        m_MonoBehaviour.bossCreateEnemy2.gameObject.SetActive(true);
        m_MonoBehaviour.transform.localEulerAngles = new Vector3(0, 180, 0);
        //靠近玩家速度降低，提高反应时间
        // m_MonoBehaviour.currSpeed = m_MonoBehaviour.speed * 0.3f;
        delayTime = 10;
        SetFaceRandomPos(m_MonoBehaviour);
    }
    private float delayTime = 10;
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        // Vector3 targetPos = m_MonoBehaviour.target.position;
        delayTime--;
        if (delayTime>0)
        {
            
            return;
        }
         bool isArrive=  m_MonoBehaviour.MoveAttrack();
        if (isArrive)
        {
           SetFaceRandomPos(m_MonoBehaviour);
        }
        float disToPlayer = m_MonoBehaviour.UpdateToPlayerDistance();
        if (disToPlayer >= m_MonoBehaviour.attrackDistance)
        {
            m_MonoBehaviour.PlayerEscape();
        }
    }
    private void SetFaceRandomPos(CashBossMonoBehaviours m_MonoBehaviour)
    {
        Vector3 movePos = GetFarwardPos(m_MonoBehaviour);
        Logger.PrintColor("blue", "boss 随机移动到位置SetFaceRandomPos=" + movePos);
        m_MonoBehaviour.targetPos = movePos;
    }
    private Vector3 GetFarwardPos(CashBossMonoBehaviours m_MonoBehaviour)
    {
       Vector3 currPlayerPos= m_MonoBehaviour.target.transform.position;
        Logger.PrintColor("red", "boss  m_MonoBehaviour.target.forward=" + m_MonoBehaviour.target.forward);
        float randomX = currPlayerPos.x + Random.Range(10, -10);
        float randomY = currPlayerPos.y + Random.Range(2.5f, -2.5f);
        float randomZ = currPlayerPos.z+ m_MonoBehaviour.attrackDistance;
        return new Vector3(randomX, randomY, randomZ);
    }
   

   


    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        m_MonoBehaviour.bossCreateEnemy.gameObject.SetActive(false);
        m_MonoBehaviour.bossCreateEnemy2.gameObject.SetActive(false);

    }

}
