using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float gravity = 1;
    public float timeToMaxGravity = 1;
    public float riseSpeed = 1;
    public float fallRiseDeadZone = 0.1f;

    private float rayCastDistance;
    private float currentGravity;
    private float elapsedFallTime;

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
            RiseOrFall(rise: false);
            IsOnFloor = false;
        }
        else if (distanceToFloor < rayCastDistance - 0.5f * fallRiseDeadZone)
        {
            RiseOrFall(rise: true);
            IsOnFloor = true;
        }
        else if (!IsOnFloor)
        {
            IsOnFloor = true;
            elapsedFallTime = 0;
        }
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

    private void RiseOrFall(bool rise)
    {
        Vector3 newPosition = transform.position;
        newPosition.y += (rise ? riseSpeed : -1 * currentGravity) * Time.deltaTime;
        transform.position = newPosition;

        if (!rise && elapsedFallTime < timeToMaxGravity)
        {
            elapsedFallTime += Time.deltaTime;
            currentGravity = Mathf.Lerp(0, gravity, elapsedFallTime / timeToMaxGravity);
        }
    }
}
