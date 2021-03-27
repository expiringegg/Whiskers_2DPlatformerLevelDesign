using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Transform[] boltspawnpoints;
    public GameObject bolt;

    public Transform player;
    public float speed;

    public float behaviourtimer;

    public int bosshealth;
    public GameObject bosshealthbar;
    public Vector3 theScale;

    public Animator animator;
    public Transform target;
    public Sprite spittersprite;

    public int damage = 1;
    public bool attacking;
    public bool inrange;

    public bool floor;
    public bool spawn;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        behaviourtimer += Time.deltaTime;
        if (bosshealth > 0)
        {
            if (behaviourtimer < 3 && behaviourtimer > 1)
            {
                if (spawn == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Transform randompoint = boltspawnpoints[Random.Range(0, boltspawnpoints.Length)]; //chooses random point of the 5 possible spawns.  
                        randompoint.GetComponent<SpriteRenderer>().sprite = spittersprite;
                        Instantiate(bolt, randompoint.position, Quaternion.identity);

                    }
                    spawn = true;
                }
            }
            if (behaviourtimer > 4)
            {
                for (int i = 0; i < boltspawnpoints.Length; i++)
                {
                    boltspawnpoints[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                spawn = false;
                BehaviourTwo();
            }
            if (behaviourtimer > 0 && behaviourtimer < 1)
            {
                animator.SetBool("Protect", false);
            }
            if (behaviourtimer > 7)
            {
                behaviourtimer = 0f;
            }
        }
        if (bosshealth <= 0)
        {
            animator.SetTrigger("Death");
            StartCoroutine(EndofGame());
        }
    }
    IEnumerator EndofGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("End");
    }
    public void BehaviourTwo()
    {
        animator.SetBool("Protect", true);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "AttackBolt")
        {
            bosshealth -= 1;
            UpdateHealthBar();
        }
        if(collision.gameObject.name == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
    public void UpdateHealthBar()
    {
        theScale = bosshealthbar.transform.localScale; //reasigns scaling 
        theScale.x -= 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        bosshealthbar.transform.localScale = theScale; //executes
    }
}
