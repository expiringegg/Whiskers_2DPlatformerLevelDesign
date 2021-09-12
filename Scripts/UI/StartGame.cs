using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 11)
        {
            SceneManager.LoadScene("Start");
        }
    }
}
