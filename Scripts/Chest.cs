using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Player player;
    public bool interacted;
    public GameObject spiritPoint;
    public Animator animator;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && player.interactInput == 1)
        {
            Debug.Log("chest");
            if (!interacted)
            {
                interacted = true;
                Debug.Log("inteact ionhappening");
                animator.SetTrigger("Open");
                for (int i = 0; i < 5; i++) //stops from spawning hundreds of points and spawns only 5
                {
                    Instantiate(spiritPoint, gameObject.transform.position, Quaternion.identity);
                }
            }
        }
    }
}
