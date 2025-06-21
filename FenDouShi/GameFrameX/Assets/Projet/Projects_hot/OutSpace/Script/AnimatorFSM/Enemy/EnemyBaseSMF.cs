using UnityEngine;
using UnityEditor;

public class EnemyBaseSMF : EnemyShipBase
{
    private static readonly int hashIdle = Animator.StringToHash("Idle");
    private static readonly int hashRotate = Animator.StringToHash("Rotate");
    private static readonly int hashAttack = Animator.StringToHash("Attack");
    private static readonly int hashMove = Animator.StringToHash("Move");

    protected Animator _animator;
    public override  void Awake()
    {
        base.Awake();
        _animator = this.gameObject.GetComponent<Animator>();
    }
    //停止动画
    public override void IdleState()
    {
        base.IdleState();
        _tailTransform.gameObject.SetActive(false);
        _animator.SetTrigger(hashIdle);
    }
    //移动动画
    public override void MoveState()
    {
        base.MoveState();
        _tailTransform.gameObject.SetActive(true);
        _animator.SetTrigger(hashMove);
    }
    public override void AttrackState()
    {
        base.AttrackState();
        _tailTransform.gameObject.SetActive(true);
        _animator.SetTrigger(hashAttack);
    }
    public override void RotateState()
    {
        base.RotateState();
        _animator.SetTrigger(hashRotate);
    }
    public override void DeadState()
    {
        base.DeadState();

    }
}