using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Physics")]
    private Rigidbody rb;
    private Vector3 input;
    public float moveSpeed;
    public KeyCode sprintKey;
    private bool isRunning;
    public float maxSlopeAngle;
    public bool incontrolable;
    public float fallSpeed;
    [SerializeField] LayerMask mask;

    [Header("Sprites")]
    public SpriteRenderer sr;

    //TO COLOCANDO ESSA VARIAVEL AQUI PQ FAZ PARTE, FEITO DE ULTIMA HORA NÉ HEHEHE, brincadeira eu me garanti mas to com preguiça de criar um script e logica mt boa pra esse role de inventario
    public float itensCollected; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        sr = GetComponent<SpriteRenderer>();
        isRunning = false;
        incontrolable = true;
        itensCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(incontrolable)
        {
            rb.velocity = new Vector3(0, -1, 0) * fallSpeed;
        }
        GetInputs();
        //CheckSlope();
        MovePlayer();
    }

    void OnTriggerEnter(Collider other) {
        int layer = LayerMask.NameToLayer("TerrainTest");
        if(other.gameObject.layer == layer)
        {
            incontrolable = false;
        }
    }

    void GetInputs()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetKey(sprintKey))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (input == new Vector3(0,0,1)) //up
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteUp;
        } 
        else if (input == new Vector3(1,0,1)) //upright
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(1,0,0)) //right
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(-1,0,1)) //upleft
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteLeft;
        } 
        else if (input == new Vector3(1,0,-1)) //DownRight
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteRight;
        } 
        else if (input == new Vector3(0,0,-1)) //down
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteDown;
        } 
        else if (input == new Vector3(-1,0,-1)) //downleft
        {
            sr.sprite = this.GetComponent<PlayerSkins>().currentSpriteLeft;
        } 
        else if (input == new Vector3(-1,0,0)) //left
        {
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
        if(!incontrolable)
        {
            if(isRunning)
            {
                rb.velocity = input * moveSpeed * 1.5f;
            }
            else
            {
                rb.velocity = input * moveSpeed;
            }
        }
    }
}
