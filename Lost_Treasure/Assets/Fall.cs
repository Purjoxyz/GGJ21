using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float gravity = 1;
    public float riseSpeed = 1;
    public float fallRiseDeadZone = 0.1f;

    private float rayCastDistance;
    public bool IsOnFloor
    {
        get;private set;
    }

    public bool Active
    {
        get; set;
    }
    // Start is called before the first frame update

    void Start()
    {
        rayCastDistance = 1f;
        Active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active)
            return;

        float distanceToFloor = CalculateDistanceToFloor();
        if (distanceToFloor < 0 || distanceToFloor > rayCastDistance + 0.5f * fallRiseDeadZone)
        {
            FallDown();
            IsOnFloor = false;
        }
        else if (distanceToFloor < rayCastDistance - 0.5f * fallRiseDeadZone)
        {
            RiseUp();
            IsOnFloor = true;
        }
        else 
        { IsOnFloor = true; }
    }

    private float CalculateDistanceToFloor()
    {
        Physics.Raycast(transform.position + Vector3.up * rayCastDistance, Vector3.down,
            out RaycastHit hitInfo, rayCastDistance + fallRiseDeadZone, LayerMask.GetMask("Floor"));
        if (hitInfo.collider != null)
        {
            //Debug.Log("Osui!");
            //Debug.Log(hitInfo.distance);

            return hitInfo.distance;
        }
        else
        {
            return -1;
        }
    }

    private void FallDown()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += -1 * gravity * Time.deltaTime;
        transform.position = newPosition;
    }

    private void RiseUp()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += riseSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
