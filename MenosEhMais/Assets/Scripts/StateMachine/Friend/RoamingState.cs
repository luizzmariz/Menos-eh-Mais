using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : BaseState {
    private FriendSM sm;

    public Roaming(FriendSM stateMachine) : base("Roaming", stateMachine) {
        sm = (FriendSM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        /*if (sm.playerInteracting) {
            stateMachine.ChangeState(sm.interactingState);
        } else if (Random.Range(0,1000) == 1) {
            stateMachine.ChangeState(sm.idleState);
        } else */if (Random.Range(0,400) == 1) {
            sm.direction = new Vector3(Random.Range(0,10), 0, Random.Range(0,10)).normalized;
        }
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        sm.rigidBody.velocity = sm.speed * sm.direction;
    }
}
