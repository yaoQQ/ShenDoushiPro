
using UnityEngine;
using System.Collections;

public class UVAAttrackSMF : SceneLinkedSMB<UVAMonoBehaviour>
{


    public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateEnter(animator, stateInfo, layerIndex);

        // m_MonoBehaviour.controller.SetFollowNavmeshAgent(false);
        m_MonoBehaviour.Move();
        m_MonoBehaviour.isRandomPos = true;


    }
    public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);
        m_MonoBehaviour.Attrack();
    }

    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);
        m_MonoBehaviour.isRandomPos = false;
        m_MonoBehaviour.BackToPlayer();

    }

}

