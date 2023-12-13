using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInteractable : Interactable
{
    TMP_Text inventory; 
    public GameObject player;

    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<TMP_Text>();
        player = GameObject.Find("Player3");
    }

    protected override void Interact()
    {
        float itensInBag = player.GetComponent<PlayerController>().itensCollected;
        itensInBag++;
        inventory.text = " God Challenger Bag \n fucking random square (" + itensInBag + "x) -";
        player.GetComponent<PlayerController>().itensCollected = itensInBag;
        Destroy(this.gameObject);
    }
}