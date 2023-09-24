using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendSM : StateMachine {
    [HideInInspector]
    public Roaming RoamingState;

    public Rigidbody rigidBody;
    public Vector3 direction;
    public float speed = 0.5f;

    private void Awake() {
        RoamingState = new Roaming(this);
    }

    protected override BaseState GetInitialState() {
        direction = new Vector3(Random.Range(0,10), 0, Random.Range(0,10)).normalized;
        return RoamingState;
    }
    /*
    private void OnGUI() {
        string content = this.currentState != null ? this.currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
    */
}
