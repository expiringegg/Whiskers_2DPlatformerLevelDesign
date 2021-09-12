using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spit_Spirit : MonoBehaviour
{
    public GameObject enemyBolt;
    public float waitToAttack;
    public Player player;
    public int damage = 1;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        waitToAttack += Time.deltaTime;
    }

    void Update()
    {
        if (waitToAttack > 2f)
        {
            Instantiate(enemyBolt, gameObject.transform.position + new Vector3(0f, 0.3f, 0f), Quaternion.identity); //enemy bolt spawns slightly above the enemy
            waitToAttack = 0f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            player.TakeDamage(damage);
        }
    }
}
