using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private Fall fall;
    public float jumpForce=5;
    public float jumpTime = 1;
    private float elapsedJumpTime;
    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        fall = GetComponent<Fall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space) && fall.IsOnFloor )
        {
            isJumping = true;
            fall.Active = false;
        }
        if (isJumping)
        {
            elapsedJumpTime += Time.deltaTime;
            if(elapsedJumpTime >= jumpTime)
            {
                isJumping = false;
                elapsedJumpTime = 0;
                fall.Active = true;
            } 
            else
            {
                RiseUp();
            }
        }
    }
    private void RiseUp()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += jumpForce * Time.deltaTime;
        transform.position = newPosition;
    }
}
