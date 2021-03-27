using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpirit : MonoBehaviour
{
    public float speed;
    public GameObject[] waypoints;
    int nextwaypoint = 1;
    float distopoint;

    void Update()
    {
        Move();
    }
    public void Move()
    {
        distopoint = Vector2.Distance(transform.position, waypoints[nextwaypoint].transform.position);
        transform.position = Vector2.MoveTowards(transform.position, waypoints[nextwaypoint].transform.position, speed * Time.deltaTime);

        if (distopoint < 0.3f)
        {
            TakeTurn();
        }
    }
    public void TakeTurn()
    {
        Vector3 currentrot = transform.eulerAngles;
        currentrot.z += waypoints[nextwaypoint].transform.eulerAngles.z; //rotates enemey 
        ChooseNextWayPoint();

    }
    void ChooseNextWayPoint()
    {
        nextwaypoint++;
        if (nextwaypoint == waypoints.Length)
        {
            nextwaypoint = 0;
        }
    }
}
