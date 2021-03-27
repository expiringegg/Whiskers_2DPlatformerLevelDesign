using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    public Vector3 savedtransform;
    public int spiritpoints;
    public int potions;

    public bool RangeUnlocked;
    public bool DashUnlocked;
    public bool DoubleJumpUnlocked;

    public int currenthealth;

    public bool heallearned;
    public bool tutorials;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        //allows for values to not be overwritten and only have one of the object on reload of scenes
    }
}

