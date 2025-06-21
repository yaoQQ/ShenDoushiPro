using UnityEngine;
using System.Collections;

public class UVAShipIdleSFM : SceneLinkedSMB<UVAMonoBehaviour>
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
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);


    }

}
