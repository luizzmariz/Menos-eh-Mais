using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSM : StateMachine {
    [HideInInspector]
    public Idle idleState;
    [HideInInspector]
    public Moving movingState;
    [HideInInspector]
    public Interacting interactingState;

    public Rigidbody rigidBody;
    public Vector3 direction;
    public bool playerInteracting = false;
    public float speed = 0.5f;

    private void Awake() {
        idleState = new Idle(this);
        movingState = new Moving(this);
        interactingState = new Interacting(this);
    }

    protected override BaseState GetInitialState() {
        playerInteracting = false;
        return idleState;
    }

    private void OnGUI() {
        string content = this.currentState != null ? this.currentState.name : "(no current state)";
        GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
    }
}
