using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInstances : MonoBehaviour
{
    //when game ends or/and player goes back in the main menu i dont want those object to be there with the saved values,
    //so this will reset the whole game when player leaves the main game
    void Start()
    {
        Destroy(GameObject.Find("CheckPoint Handle"));
        Destroy(GameObject.Find("UIManager"));
    }
}