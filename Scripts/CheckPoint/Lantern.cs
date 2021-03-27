using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    public GameObject checkpoint;
    public UIManager UImanager;
    public Player player;
    public Shop shop;
    public Animator animator;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        UImanager = GameObject.Find("UIManager").GetComponent<UIManager>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            checkpoint.GetComponent<CheckPoint>().savedtransform = gameObject.transform.position;
            checkpoint.GetComponent<CheckPoint>().spiritpoints = UImanager.totalspiritpoints;
            checkpoint.GetComponent<CheckPoint>().potions = UImanager.potiontotal;
            checkpoint.GetComponent<CheckPoint>().currenthealth = player.currenthealth;

            //on collsion it saves all current values to the checkpoint so on death the player doesn't lose progress
        }
    }
    private void Update()
    {
        if(checkpoint.GetComponent<CheckPoint>().savedtransform == gameObject.transform.position)
        {
            animator.SetBool("Lit",true);
        }
        if (checkpoint.GetComponent<CheckPoint>().savedtransform != gameObject.transform.position)
        {
            animator.SetBool("Lit", false);
        }
        //lights up if that current lantern is set off
    }
}
