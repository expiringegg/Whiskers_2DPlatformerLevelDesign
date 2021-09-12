using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpirit : MonoBehaviour
{
    public float speed;
    public GameObject[] wayPoints;
    int nextWayPoint = 1;
    float distanceToPoint;

    void Update()
    {
        Move();
    }

    public void Move()
    {
        distanceToPoint = Vector2.Distance(transform.position, wayPoints[nextWayPoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[nextWayPoint].transform.position, speed * Time.deltaTime);

        if (distanceToPoint < 0.3f)
        {
            TakeTurn();
        }
    }

    public void TakeTurn()
    {
        Vector3 currentrot = transform.eulerAngles;
        currentrot.z += wayPoints[nextWayPoint].transform.eulerAngles.z; //rotates enemey 
        ChooseNextWayPoint();
    }
    void ChooseNextWayPoint()
    {
        nextWayPoint++;
        if (nextWayPoint == wayPoints.Length)
        {
            nextWayPoint = 0;
        }
    }
}
