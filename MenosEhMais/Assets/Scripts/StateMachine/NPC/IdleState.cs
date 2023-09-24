using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : BaseState {
    private NPCSM sm;

    public Idle(NPCSM stateMachine) : base("Idle", stateMachine) {
        sm = (NPCSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (sm.playerInteracting) {
            stateMachine.ChangeState(sm.interactingState);
        } else if (Random.Range(0,1000) == 1) {
            sm.direction = new Vector3(Random.Range(0,10), 0, Random.Range(0,10)).normalized;
            stateMachine.ChangeState(sm.movingState);
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = new Vector3(0,0,0);
    }
}
