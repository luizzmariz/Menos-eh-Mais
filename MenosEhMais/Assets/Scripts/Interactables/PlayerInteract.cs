using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public float interactionCooldown;
    public float timer;
    bool canTeleport;
    //[SerializeField] private float radius = 1f;

    void Start()
    {
        canTeleport = true;
        timer = 0f;
    }

    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canTeleport = true;
            timer = 0;
        }

    }

        void OnTriggerEnter(Collider other) {
            if(other.GetComponent<PortalInteractable>() != null)
            {
                if(canTeleport)
                {
                    timer = interactionCooldown;
                    canTeleport = false;
                    other.GetComponent<Interactable>().BaseInteract();
                }        
            }
            else if(other.GetComponent<Interactable>() != null)
            {
                other.GetComponent<Interactable>().BaseInteract();
            }
        }
        /*void OnTriggerExit(Collider other) {
            if(other.GetComponent<Interactable>() != null)
            {
                other.GetComponent<Interactable>().HideAction();
            }
        }*/
}
