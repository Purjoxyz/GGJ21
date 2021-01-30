using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFallingObject
{
    public Vector3 respawnPoint;

    [SerializeField, Tooltip("The player character's main collider")]
    private Collider objCollider;

    [SerializeField, Tooltip("Player movement speed")]
    private float moveSpeed = 1;

    [SerializeField, Tooltip("Player rotation speed")]
    private float rotationSpeed = 1;

    private Jump jump;

    public Vector3 Extents
    {
        get
        {
            return objCollider.bounds.extents;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        jump = GetComponent<Jump>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDebugInput();
        UpdateMove();
        UpdateRotation();
    }

    private void UpdateMove()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -1 * transform.forward;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += -1 * transform.right;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += transform.right;
        }

        moveDirection.Normalize();
        Move(moveDirection);
    }

    private void UpdateRotation()
    {
        Vector3 rotationDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationDirection += -1 * Vector3.up;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationDirection += Vector3.up;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationDirection * rotationSpeed * Time.deltaTime);
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void UpdateDebugInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            transform.position = respawnPoint;
            jump.ResetJump();
        }
    }
}
