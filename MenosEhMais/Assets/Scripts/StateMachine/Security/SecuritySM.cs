using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SecuritySM : StateMachine {
    [HideInInspector]
    public Relaxed relaxedState;
    [HideInInspector]
    public Chase chaseState;
    [HideInInspector]
    public Return returnState;
    [HideInInspector]
    public Alert alertState;

    public StudioEventEmitter emitter;
    public Rigidbody rigidBody;
    public Transform player;
    public Vector3 initialPos;
    public float speed = 1f;

    //[Header("Sprites")]
    public SpriteRenderer sr;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    public Sprite up;

    private void Awake() {
        relaxedState = new Relaxed(this);
        chaseState = new Chase(this);
        returnState = new Return(this);
        alertState = new Alert(this);
        emitter = GetComponent<StudioEventEmitter>();
    }

    protected override BaseState GetInitialState() {
        player = GameObject.Find("Player").transform;
        initialPos = rigidBody.transform.position;
        return relaxedState;
    }
}
