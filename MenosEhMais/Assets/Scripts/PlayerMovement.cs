using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody rb;
    private Vector3 input;
    public float moveSpeed; 

    //[Header("Sprites")]
    /*public SpriteRenderer sr;
    public Sprite down;
    public Sprite right;
    public Sprite left;
    public Sprite up;*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();
        MovePlayer();
    }

    void GetInputs()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (input == new Vector3(0,0,1)) 
        {
            input = new Vector3(1,0,1);
        } 
        else if (input == new Vector3(1,0,1)) 
        {
            input = new Vector3(1,0,0);
        } 
        else if (input == new Vector3(1,0,0)) 
        {
            input = new Vector3(1,0,-1);
        } 
        else if (input == new Vector3(-1,0,1)) 
        {
            input = new Vector3(0,0,1);
        } 
        else if (input == new Vector3(1,0,-1)) 
        {
            input = new Vector3(0,0,-1);
        } 
        else if (input == new Vector3(0,0,-1)) 
        {
            input = new Vector3(-1,0,-1);
        } 
        else if (input == new Vector3(-1,0,-1)) 
        {
            input = new Vector3(-1,0,0);
        } 
        else if (input == new Vector3(-1,0,0)) 
        {
            input = new Vector3(-1,0,1);
        }
    }   
    
    void MovePlayer()
    {
        //rb.AddForce(input * moveSpeed, ForceMode.Acceleration);
        rb.velocity = input * moveSpeed;
    }
}
