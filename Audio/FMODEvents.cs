using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Menu and game music")] 
    [field: SerializeField] public EventReference music { get; private set; }
    
    [field: Header("Shot SFX")]
    [field: SerializeField] public EventReference shotSound { get; private set; }
    
    [field: Header("Car explosion SFX")]
    [field: SerializeField] public EventReference carExplosionSound { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in the scene.");
        }
        instance = this;
    }
}
