using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    public Vector3 savedTransform;
    public int spiritPoints;
    public int potions;

    public bool rangeUnlocked;
    public bool dashUnlocked;
    public bool doubleJumpUnlocked;

    public int currentHealth;

    public bool healLearned;
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

