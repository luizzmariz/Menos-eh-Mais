using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : BaseState {
    private SecuritySM sm;
    public Return(SecuritySM stateMachine) : base("Return", stateMachine) {
        sm = (SecuritySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (Vector3.Distance(sm.rigidBody.transform.position, sm.player.position) <= 3) {
            stateMachine.ChangeState(sm.chaseState);
        } else if (Vector3.Distance (sm.rigidBody.transform.position, sm.initialPos) < 1) {
            stateMachine.ChangeState(sm.alertState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        Vector3 currentPos = new Vector3(sm.rigidBody.transform.position.x, 0, sm.rigidBody.transform.position.z);
        Vector3 initialPos = new Vector3(sm.initialPos.x, 0, sm.player.position.z);
        Vector3 direction = initialPos - currentPos;
        sm.rigidBody.velocity = sm.speed * direction;
    }
}
