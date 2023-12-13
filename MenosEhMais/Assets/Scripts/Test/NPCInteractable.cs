using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInteractable : Interactable
{
    public GameObject player;
    protected override void Interact()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerSkins>().ChangeSprites();
    }
}