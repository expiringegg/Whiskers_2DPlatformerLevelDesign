using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public Transform player;
    public bool goleft;

    public Transform attackPos;
    public LayerMask whatisEnemies;
    public float attackRange;
    public int damage = 1;

    public AudioSource hit;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        if (player.GetComponent<Player>().facingright)
        {
            goleft = false;
        }
        if (player.GetComponent<Player>().facingright == false)
        {
            goleft = true;
        }
        //based on players movement, it'll spawn moving either direction
    }
    void Update()
    {
        Collider2D[] enemiestodamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisEnemies);
        for (int i = 0; i < enemiestodamage.Length; i++)
        {
            enemiestodamage[i].GetComponent<EnemyDamage>().TakeDamage(damage); //the amount of damage caused
            hit.Play();

            StartCoroutine(FasterDespawn());
        }

        if (goleft == true)
        {
            gameObject.transform.position -= new Vector3(0.1f, 0f, 0f);
            StartCoroutine(Despawn());

        }
        if (goleft == false)
        {
            gameObject.transform.position += new Vector3(0.1f, 0f, 0f);
            StartCoroutine(Despawn());
        }
    }
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
    IEnumerator FasterDespawn()
    {
        yield return new WaitForSeconds(0.02f);
        Destroy(gameObject);
    }
}
