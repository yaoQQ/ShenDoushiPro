using System;
using UnityEngine;
using Assets.script.FSMFrame;

[CreateAssetMenu(menuName = "State/StateBase/EnemyStateMove")]
public class EnemyStateMove : EnemyState
{
    private Quaternion targetRotation;
    public EnemyStateMove(Enemy enemy)
        : base(enemy) {
            mStateName = ShipState.Move.ToString();
    }
    public override void initTransition() {
        FSmTransition.RegisterTransition(ShipState.stop.ToString());
        FSmTransition.RegisterTransition(ShipState.Dead.ToString());
        FSmTransition.RegisterTransition(ShipState.Attrack.ToString());
    }
    public override bool OnEnter(string prevState) {
       // Debug.Log("OnEnter EnemyStateMove  prevState=" + prevState);
        if (enemy.TailTransform != null) {
            enemy.TailTransform.gameObject.SetActive(true);
        }
        return true;
        // public delegate TResult Func<T, TResult>(T arg1);
    }


    public override bool OnExit(string nextState) {
      //  Debug.Log("OnExit EnemyStateMove nextState=" + nextState);
        return true;
    }

    public override void OnUpdate() {
        Vector3 targetPos = enemy.targetPos;
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetPos, enemy.speed * Time.deltaTime);
        Vector3 currPos = enemy.transform.position;

        if (Vector3.Distance(currPos, OutSpaceCameraManager.Instance.Player.position) <= enemy.attrackDistance /*&& isReadRote*/) {
            enemy.playerFSM.Trigger(ShipState.Attrack.ToString());
            return;
        }

        float distance = Vector3.Distance(currPos, targetPos);
        if (Vector3.Distance(enemy.transform.position, targetPos) < enemy.speed * Time.deltaTime) {
            if (enemy.pathList.Count <= 0) {
                enemy.playerFSM.Trigger(ShipState.stop.ToString());
                return;
            }
            enemy.getNextPos();
        }      
    }

    public virtual void lookTargetRotate() {
        if (isReadRote) {
            return;
        }
        Quaternion newRotation = Quaternion.Lerp(enemy.transform.rotation, targetRotation, enemy.RotateSpeed * Time.deltaTime);
        enemy.transform.rotation = newRotation;
    }
    public virtual void LookAtPos(Vector3 lookPos) {
        Vector3 forwardPos = lookPos - enemy.transform.position;
        if (forwardPos == Vector3.zero) {
            return;
        }
        targetRotation = Quaternion.LookRotation(forwardPos, Vector3.up);
    }
    protected bool isReadRote {
        get {
            float angel = Quaternion.Angle(targetRotation, enemy.transform.rotation);
            if (angel <= 1f && angel >= -1f) {
                return true;
            }
            return false;
        }
    }
}

