using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Relaxed : BaseState {
    private SecuritySM sm;
    public Relaxed(SecuritySM stateMachine) : base("Relaxed", stateMachine) {
        sm = (SecuritySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (Vector3.Distance(sm.rigidBody.transform.position, sm.player.position) <= 3) {
            stateMachine.ChangeState(sm.chaseState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }
}
