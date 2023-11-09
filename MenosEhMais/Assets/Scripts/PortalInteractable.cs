using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PortalInteractable : Interactable
{
    public GameObject player;
    protected override void Interact()
    {
        player = GameObject.Find("Player3");
        player.GetComponent<PlayerSkins>().ChangeSprites();
    }
}