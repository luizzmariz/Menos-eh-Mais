using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : BaseState {
    private SecuritySM sm;

    

    
    public Chase(SecuritySM stateMachine) : base("Chase", stateMachine) {
        sm = (SecuritySM)stateMachine;
    }

    public override void Enter() {
        base.Enter();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.PoliceCall, sm.transform.position);
        
       
    }

    public override void UpdateLogic() {
        base.UpdateLogic();
        if (Vector3.Distance(sm.rigidBody.transform.position, sm.player.position) >= 5) {
            stateMachine.ChangeState(sm.returnState);
        }

        
        
    }

    public override void UpdatePhysics() {
        base.UpdatePhysics();
        Vector3 currentPos = new Vector3(sm.rigidBody.transform.position.x, 0, sm.rigidBody.transform.position.z);
        Vector3 playerPos = new Vector3(sm.player.position.x, 0, sm.player.position.z);
        sm.rigidBody.velocity = sm.speed * (playerPos - currentPos).normalized;
        if (sm.rigidBody.velocity.x == sm.rigidBody.velocity.y && sm.rigidBody.velocity.x < 0) {
            sm.sr.sprite = sm.down;
        } else if (sm.rigidBody.velocity.x == sm.rigidBody.velocity.z && sm.rigidBody.velocity.x > 0) {
            sm.sr.sprite = sm.up;
        } else if (sm.rigidBody.velocity.x > sm.rigidBody.velocity.z) {
            sm.sr.sprite = sm.right;
        } else if (sm.rigidBody.velocity.x < sm.rigidBody.velocity.z) {
            sm.sr.sprite = sm.left;
        } 
    }
}
