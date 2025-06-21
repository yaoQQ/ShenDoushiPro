using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Animator animator { get { return m_Animator; } }
    protected Animator m_Animator;

    protected Rigidbody m_Rigidbody;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody= GetComponent<Rigidbody>();
    }


}
