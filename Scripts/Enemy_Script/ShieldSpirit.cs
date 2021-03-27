using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpirit : MonoBehaviour
{
    public Animator myanimator;
    public bool inrange;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inrange = true;
            myanimator.SetBool("inrange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inrange = false;
        myanimator.SetBool("inrange", false);
    }

}

