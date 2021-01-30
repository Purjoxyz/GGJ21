using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float gravity = 1;
    public float timeToMaxGravity = 1;
    public float riseSpeed = 1;
    public float fallRiseDeadZone = 0.1f;

    private IFallingObject fallingObj;
    private float rayCastDistance;
    private float currentGravity;
    private float elapsedFallTime;

    public bool IsOnFloor
    {
        get; private set;
    }

    public bool Active
    {
        get; set;
    }
    // Start is called before the first frame update

    void Start()
    {
        fallingObj = GetComponent<IFallingObject>();
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
        RaycastHit[] hits = new RaycastHit[fallingObj == null ? 1 : 5];

        for (int i = 0; i < hits.Length; i++)
        {
            Vector3 origin = transform.position + Vector3.up * rayCastDistance;
            switch (i)
            {
                // case 0 excluded because it's already OK
                case 1:
                    origin += new Vector3(fallingObj.Extents.x, 0, fallingObj.Extents.z);
                    break;
                case 2:
                    origin += new Vector3(fallingObj.Extents.x, 0, -1 * fallingObj.Extents.z);
                    break;
                case 3:
                    origin += new Vector3(-1 * fallingObj.Extents.x, 0,  fallingObj.Extents.z);
                    break;
                case 4:
                    origin += new Vector3(-1 * fallingObj.Extents.x, 0, -1 * fallingObj.Extents.z);
                    break;
            }

            Physics.Raycast(origin, Vector3.down, out hits[i], rayCastDistance + fallRiseDeadZone, LayerMask.GetMask("Floor"));

            if (hits[i].collider != null)
            {
                //Debug.Log("Osui!");
                //Debug.Log(hitInfo.distance);

                return hits[i].distance;
            }
        }

        // No raycast hits
        return -1;
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
