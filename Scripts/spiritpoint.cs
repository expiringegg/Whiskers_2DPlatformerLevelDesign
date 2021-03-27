using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritpoint : MonoBehaviour
{
    public GameObject UImanager;
    public Transform target;
    public int speed = 5; 

    public float timer;
    public AudioSource gainpoint;
    void Start()
    {
        UImanager = GameObject.Find("UIManager");
        target = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > 3f)
        {
          transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); //the enemy will move towards the connected objec
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            UImanager.GetComponent<UIManager>().totalspiritpoints += 1;
            UImanager.GetComponent<UIManager>().UpdatePoints();
            gainpoint.Play();
            StartCoroutine(DestorythePoint());
        }
    }
    IEnumerator DestorythePoint()
    {
        yield return new WaitForSeconds(0.03f);
        Destroy(gameObject);
    }
}
