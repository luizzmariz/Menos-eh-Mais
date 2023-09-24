using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alert : BaseState {
    private SecuritySM sm;
    
    public Alert(SecuritySM stateMachine) : base("Alert", stateMachine) {
        sm = (SecuritySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        sm.sr.sprite = sm.down;
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (Vector3.Distance(sm.rigidBody.transform.position, sm.player.position) <= 5) {
            stateMachine.ChangeState(sm.chaseState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }
}
