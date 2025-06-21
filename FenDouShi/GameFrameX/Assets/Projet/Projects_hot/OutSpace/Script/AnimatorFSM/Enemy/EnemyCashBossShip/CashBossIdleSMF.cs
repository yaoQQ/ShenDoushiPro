using UnityEngine;
using System.Collections;

public class CashBossIdleSMF : SceneLinkedSMB<CashBossMonoBehaviours>
{
    public override void OnStart(Animator animator)
    {
        base.OnStart(animator);
        m_MonoBehaviour.initAnimator();
    }

    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        m_MonoBehaviour.Stop();



    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);





        //m_MonoBehaviour.LookAtRotate();
        if (!m_MonoBehaviour.isRotateCompelet())
        {
            //旋转到目标点角度
            m_MonoBehaviour.RotateState();
        }
        else if (m_MonoBehaviour.pathList.Count > 0)
        {
            //设置有目标点移动
            m_MonoBehaviour.MoveState();
        }
        else if (m_MonoBehaviour.pathList.Count <= 0)//追逐玩家位置点
        {
            //完成所有路径，移动到玩家点
            m_MonoBehaviour.AddPathPos(m_MonoBehaviour.target.position);
        }
        //更新与玩家的距离,距离到达切换到攻击状态
        m_MonoBehaviour.UpdateToPlayerDistance();

    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
