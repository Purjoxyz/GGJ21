using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public List<Vector3> waypoints;
    public float travelSpeed = 1;
    public float tolerance=1;
    private int currentPoint;
    private int previousPoint;
    

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[0];
        currentPoint = 1;
        previousPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, waypoints[currentPoint]) < tolerance)
        {
            previousPoint = currentPoint;
            currentPoint++;
            if (currentPoint >= waypoints.Count)
            {
                currentPoint = 0;
            }
        }
        Vector3 newPosition = transform.position;
        newPosition += (waypoints[currentPoint] - transform.position).normalized * travelSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
