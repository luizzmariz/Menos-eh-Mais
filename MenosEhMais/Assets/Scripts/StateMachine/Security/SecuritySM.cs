using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuritySM : StateMachine {
    [HideInInspector]
    public Relaxed relaxedState;
    [HideInInspector]
    public Chase chaseState;
    [HideInInspector]
    public Return returnState;
    [HideInInspector]
    public Alert alertState;

    public Rigidbody rigidBody;
    public Transform player;
    public Vector3 initialPos;
    public float speed = 1f;

    private void Awake() {
        relaxedState = new Relaxed(this);
        chaseState = new Chase(this);
        returnState = new Return(this);
        alertState = new Alert(this);
    }

    protected override BaseState GetInitialState() {
        player = GameObject.Find("Player").transform;
        initialPos = rigidBody.transform.position;
        return relaxedState;
    }
}
