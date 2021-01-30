using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float gravity = 1;

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position + Vector3.up, Vector3.down,
            out RaycastHit hitInfo, 1.1f, LayerMask.GetMask("Floor"));
        if (hitInfo.collider != null)
        {
            //Debug.Log("Osui!");
            Debug.Log(hitInfo.distance);
        }
    }
}
