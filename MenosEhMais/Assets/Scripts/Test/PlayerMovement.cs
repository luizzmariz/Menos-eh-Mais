using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody rb;
    private Vector3 input;
    public float moveSpeed;
    public float maxSlopeAngle;
    [SerializeField] LayerMask mask;

    [Header("Sprites")]
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
        //CheckSlope();
        MovePlayer();
    }

    void GetInputs()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

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
    
    /*void CheckSlope()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, 1f, mask))
        {
            if(Vector3.Angle(Vector3.up, hitInfo.normal) < maxSlopeAngle && Vector3.Angle(Vector3.up, hitInfo.normal) != 0)
            {
                input = Vector3.ProjectOnPlane(input, hitInfo.normal).normalized;
            }
        } 
    }*/

    void MovePlayer()
    {
        //rb.AddForce(input * moveSpeed, ForceMode.Acceleration);
        //Debug.DrawRay(transform.position, transform.position + input * moveSpeed, Color.red);
        rb.velocity = input * moveSpeed;
    }
}
