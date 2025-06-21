using UnityEngine;
using System.Collections;

public class BezierShipIdleSMF : SceneLinkedSMB<BezierShipMonoBehaviour>
{
    private float delayTime = 1;
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
        delayTime -= Time.deltaTime;
        if (delayTime <= 0)
        {
            m_MonoBehaviour.AttrackState();
            delayTime = 1;
        }
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
