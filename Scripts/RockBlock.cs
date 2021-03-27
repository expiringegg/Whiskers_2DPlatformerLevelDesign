using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBlock : MonoBehaviour
{
    public GameObject player;
    public Animator myanimator;
    public AudioSource breakrock;

    public CheckPoint checkPoint;
    private bool isdead;
    private void Start()
    {
        player = GameObject.Find("Player");
        checkPoint = GameObject.Find("CheckPoint Handle").GetComponent<CheckPoint>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (player.GetComponent<Player>().dashinput == 1 && collision.gameObject.name == "Player" && checkPoint.DashUnlocked)
        {
            if (!isdead)
            {
                isdead = true;
                myanimator.SetTrigger("Break");
                breakrock.Play();
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
