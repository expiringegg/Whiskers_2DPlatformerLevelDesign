using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHome : MonoBehaviour
{
    public List<string> defeatedenemies = new List<string>();
    public GameObject chest;
 
    void Update()
    {
        if(defeatedenemies.Count == 18)
        {
            chest.SetActive(true);
        }
        
    }
}
