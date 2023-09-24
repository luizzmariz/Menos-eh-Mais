using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance {get; private set; }

    private void Awake(){
        if (instance != null){
            Debug.LogError("Found more than one Fmod Event in the scene.");
        }
        instance = this;
    }
}