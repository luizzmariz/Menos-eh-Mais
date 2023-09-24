using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacting : BaseState {
    private NPCSM sm;
    
    public Interacting(NPCSM stateMachine) : base("Interacting", stateMachine) {
        sm = (NPCSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (!sm.playerInteracting) {
            stateMachine.ChangeState(sm.idleState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }
}
