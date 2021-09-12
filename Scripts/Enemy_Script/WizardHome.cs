using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHome : MonoBehaviour
{
    public List<string> defeatedEnemies = new List<string>();
    public GameObject chest;
 
    void Update()
    {
        if(defeatedEnemies.Count == 18)
        {
            chest.SetActive(true);
        }
    }
}
