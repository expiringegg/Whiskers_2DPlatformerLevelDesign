using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSpirit : MonoBehaviour
{
    public Transform target;

    public float speed;
    public int health;
    public Animator animator;

    Vector2 moveDirection;
    Vector2 movePerSecond;

    public int damage = 1;
    public bool attacking;
    public bool inRange;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 1f;

    public bool floor;
    private void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>(); //finds gameobject with the name and searches for the component attached to the object
        calcuateNewMovementVector();
    }
    void calcuateNewMovementVector()
    {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        moveDirection = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        movePerSecond = moveDirection * speed;
    }
    private void FixedUpdate()
    {
        if (health == 1)
        {
            if (inRange == false)
            {
                transform.position = new Vector2(transform.position.x + (movePerSecond.x * Time.deltaTime), transform.position.y + (movePerSecond.y * Time.deltaTime));
                if (Time.time - latestDirectionChangeTime > directionChangeTime)
                {
                    latestDirectionChangeTime = Time.time;
                    calcuateNewMovementVector();
                }
            }

            if (inRange == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); //the enemy will move towards the connected object
                moveDirection = (target.transform.position - transform.position); //sets the direction the enemy is moving
            }
        }

        if (health <= 0)
        {
            StartCoroutine(Setoff());
            StartCoroutine(Despawn()); //I delayed the destruction so the animation can finish playing before it's destoryed  
        }
    }

    IEnumerator Setoff()
    {
        yield return new WaitForSeconds(0.2f);
        animator.Play("enemydeath");
    }

    public IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject); //enemy dies
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Walls" || collision.gameObject.tag == "Floor")
        {
            //this is so that the enemy doesnt fly outside of the walls.
            calcuateNewMovementVector();

        }

        if(collision.gameObject.name == "Player")
        {
            target.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
