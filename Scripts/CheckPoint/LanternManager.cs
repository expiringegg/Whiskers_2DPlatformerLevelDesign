using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LanternManager : MonoBehaviour
{
    public GameObject[] lantern;
    void Start()
    {
        lantern = GameObject.FindGameObjectsWithTag("Checkpoint");
    }
    void Update()
        //added this so that the lanterns dont spawn in the WizardHome level, but they needed to be connected to a dontdestroyonload object to work correctly
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            for (int i = 0; i < lantern.Length; i++)
            {
                lantern[i].SetActive(true);
            }
        }
        else if (SceneManager.GetActiveScene().name != "Start")
        {
            for (int i = 0; i < lantern.Length; i++)
            {
                lantern[i].SetActive(false);

            }
        }
    }
}
