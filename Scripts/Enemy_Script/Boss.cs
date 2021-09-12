using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Transform[] boltSpawnPoints;
    public GameObject bolt;

    public Transform player;
    public float speed;

    public float behaviourTimer;

    public int bossHealth;
    public GameObject bossHealthBar;
    public Vector3 theScale;

    public Animator animator;
    public Transform target;
    public Sprite spitterSprite;

    public int damage = 1;
    public bool attacking;
    public bool inRange;
    public bool floor;
    public bool spawn;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        behaviourTimer += Time.deltaTime;
        if (bossHealth > 0)
        {
            if (behaviourTimer < 3 && behaviourTimer > 1)
            {
                if (spawn == false)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Transform randompoint = boltSpawnPoints[Random.Range(0, boltSpawnPoints.Length)]; //chooses random point of the 5 possible spawns.  
                        randompoint.GetComponent<SpriteRenderer>().sprite = spitterSprite;
                        Instantiate(bolt, randompoint.position, Quaternion.identity);

                    }
                    spawn = true;
                }
            }

            if (behaviourTimer > 4)
            {
                for (int i = 0; i < boltSpawnPoints.Length; i++)
                {
                    boltSpawnPoints[i].GetComponent<SpriteRenderer>().sprite = null;
                }
                spawn = false;
                BehaviourTwo();
            }

            if (behaviourTimer > 0 && behaviourTimer < 1)
            {
                animator.SetBool("Protect", false);
            }

            if (behaviourTimer > 7)
            {
                behaviourTimer = 0f;
            }
        }

        if (bossHealth <= 0)
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
            bossHealth -= 1;
            UpdateHealthBar();
        }

        if(collision.gameObject.name == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }

    public void UpdateHealthBar()
    {
        theScale = bossHealthBar.transform.localScale; //reasigns scaling 
        theScale.x -= 0.1f; //scale of the player, changes the scale on x axis to a negative to flip it
        bossHealthBar.transform.localScale = theScale; //executes
    }
}
