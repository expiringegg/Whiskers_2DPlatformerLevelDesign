using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
        
    }
}

