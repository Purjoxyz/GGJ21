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

    [SerializeField, Tooltip("Player's maximum health")]
    private int maxHealth = 100;

    private UIManager ui;
    private Animator animator;
    private Fall fall;
    private Jump jump;

    private int health;
    private bool moving;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;

            if (ui != null)
            {
                ui.UpdateHealth();
            }
        }
    }

    public bool Moving
    {
        get
        {
            return moving;
        }
        set
        {
            moving = value;
            if (value)
                animator.Play("Walk");
            else
                animator.Play("Idle");
                //animator.SetBool("GoToIdle", true);
        }
    }

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
        ui = FindObjectOfType<UIManager>();
        animator = GetComponent<Animator>();
        fall = GetComponent<Fall>();
        jump = GetComponent<Jump>();
        respawnPoint = transform.position;
        Health = maxHealth;
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

        if (moveDirection != Vector3.zero)
        {
            Move(moveDirection);
        }
        else
        {
            Moving = false;
        }
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

        if (!Moving)
        {
            Moving = true;
        }
    }

    private void UpdateDebugInput()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        transform.position = respawnPoint;
        jump.ResetJump();
        Health = maxHealth;
    }

    public void HealToFull()
    {
        Health = maxHealth;
    }

    public void HealToFull(Vector3 newRespawnPoint)
    {
        HealToFull();
        respawnPoint = newRespawnPoint;
    }

    public bool TakeDamage(int damage)
    {
        if (Health > 0)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
                return true;
            }
        }

        return false;
    }

    private void Die()
    {
        Respawn();
    }

    private void PlayWalkSound()
    {
        if (fall.IsOnFloor && !jump.IsJumping)
        {
            SFXPlayer.Instance.Play(Sound.Walk);
        }
    }
}
