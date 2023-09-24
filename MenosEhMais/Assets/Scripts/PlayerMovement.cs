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
    public SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
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
            input = new Vector3(1,0,1);//Up
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteUp;
        } 
        else if (input == new Vector3(1,0,1)) 
        {
            input = new Vector3(1,0,0);//UpRight
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(1,0,0)) 
        {
            input = new Vector3(1,0,-1);//Right
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(-1,0,1)) 
        {
            input = new Vector3(0,0,1);//UpLeft
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteLeft;
        } 
        else if (input == new Vector3(1,0,-1)) 
        {
            input = new Vector3(0,0,-1);//DownRight
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(0,0,-1)) 
        {
            input = new Vector3(-1,0,-1);//Down
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteDown;
        } 
        else if (input == new Vector3(-1,0,-1)) 
        {
            input = new Vector3(-1,0,0);//DownLeft
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteLeft;
        } 
        else if (input == new Vector3(-1,0,0)) 
        {
            input = new Vector3(-1,0,1);
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteLeft;
        }
    }   
    
    void MovePlayer()
    {
        //rb.AddForce(input * moveSpeed, ForceMode.Acceleration);
        rb.velocity = input * moveSpeed;
    }
}
