using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlock : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    public AudioSource breakRock;

    public CheckPoint checkPoint;
    private bool isDead;
    private void Start()
    {
        player = GameObject.Find("Player");
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (player.GetComponent<Player>().dashInput == 1 && collision.gameObject.name == "Player" && checkPoint.dashUnlocked)
        {
            if (!isDead)
            {
                isDead = true;
                animator.SetTrigger("Break");
                breakRock.Play();
                StartCoroutine(DestroytheORock());
            }
        }
    }
    IEnumerator DestroytheORock()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
