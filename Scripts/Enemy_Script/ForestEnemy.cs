using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestEnemy : MonoBehaviour
{
    public float speed;
    private bool movingRight = true;
    public float distance;

    public Transform player;
    
    public int health;
    public int damage;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Update()
    {
        health = gameObject.GetComponent<EnemyDamage>().health;

        if (health > 0)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime); //makes enemy move right 
        }  
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //patrolling
        if (collision.gameObject.name == "Edges")
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0); //turns left (180) when hitting an edge
                movingRight = false; //means the enemy is facing left
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0); //turns right if facing left 
                movingRight = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}

