using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    //[SerializeField] private float radius = 1f;

    /*void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<Interactable>() != null)
            {
                hitCollider.GetComponent<Interactable>().BaseInteract();
            }
        }
    }*/

        void OnTriggerEnter(Collider other) {
            if(other.GetComponent<Interactable>() != null)
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
