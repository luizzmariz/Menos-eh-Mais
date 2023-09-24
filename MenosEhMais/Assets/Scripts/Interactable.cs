using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Interactable : MonoBehaviour
{
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        
    }
}