using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugEnemy_DamagePlayer : MonoBehaviour
{
    public int damage = 1;
    public Player player;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            player.TakeDamage(damage);
        }
    }
}
