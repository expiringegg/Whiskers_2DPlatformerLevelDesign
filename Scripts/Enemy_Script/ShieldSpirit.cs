using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpirit : MonoBehaviour
{
    public Animator animator;
    public bool inRange;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inRange = true;
            animator.SetBool("inrange", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
        animator.SetBool("inrange", false);
    }

}

