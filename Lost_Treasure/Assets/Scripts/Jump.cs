using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float maxJumpForce = 5;
    public float minJumpForce = 3;
    public float jumpTime = 1;

    private Fall fall;
    private float elapsedJumpTime;
    private float currentJumpForce;

    public bool IsJumping { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        fall = GetComponent<Fall>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsJumping && Input.GetKeyDown(KeyCode.Space) && fall.IsOnFloor )
        {
            IsJumping = true;
            fall.Active = false;
            currentJumpForce = maxJumpForce;
        }
        if (IsJumping)
        {
            elapsedJumpTime += Time.deltaTime;
            if (elapsedJumpTime >= jumpTime)
            {
                ResetJump();
            } 
            else
            {
                RiseUp();
                currentJumpForce = Mathf.Lerp(maxJumpForce, minJumpForce, elapsedJumpTime / jumpTime);
            }
        }
    }

    private void RiseUp()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += currentJumpForce * Time.deltaTime;
        transform.position = newPosition;
    }

    public void ResetJump()
    {
        IsJumping = false;
        elapsedJumpTime = 0;
        fall.Active = true;
    }
}
