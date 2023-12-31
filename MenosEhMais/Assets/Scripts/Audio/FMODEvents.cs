using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{

    [field: Header("Ambience")]

    [field: SerializeField] public EventReference ambience {get; private set; }

    [field: Header("SFX")]

    [field: SerializeField] public EventReference PoliceCall {get; private set; }

    [field: SerializeField] public EventReference PoliceStep {get; private set; }

    public static FMODEvents instance {get; private set; }

    private void Awake(){
        if (instance != null){
            Debug.LogError("Found more than one Fmod Event in the scene.");
        }
        instance = this;
    }
}
