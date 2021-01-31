using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IFallingObject
{
    public Vector3 respawnPoint;

    [SerializeField, Tooltip("The player character's main collider.")]
    private Collider objCollider;

    [SerializeField, Tooltip("Player movement speed.")]
    private float moveSpeed = 1;

    [SerializeField, Tooltip("Player rotation speed.")]
    private float rotationSpeed = 1;

    [SerializeField, Tooltip("Player's maximum health.")]
    private int maxHealth = 100;

    [SerializeField, Tooltip("The lowest y-position under which the player dies.")]
    private float minGameWorldY = -50;

    [SerializeField, Tooltip("Time after death until respawn.")]
    private int deathTime = 1;

    private UIManager ui;
    private Renderer[] renderers;
    private Animator animator;
    private Fall fall;
    private Jump jump;

    private int health;
    private bool moving;
    private float elapsedDeathTime;

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

    public bool IsDead { get { return Health == 0; } }

    public bool JustDied { get; private set; }

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
        renderers = GetComponentsInChildren<Renderer>();
        animator = GetComponent<Animator>();
        fall = GetComponent<Fall>();
        jump = GetComponent<Jump>();
        respawnPoint = transform.position;
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
        {
            UpdateDeath();
        }
        else
        {
#if DEBUG
            UpdateDebugInput();
#endif
            UpdateMove();
            UpdateRotation();

            if (transform.position.y < minGameWorldY)
            {
                Die();
            }
        }
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
            ResetPlayer();
            GoToRespawnPoint(false);
        }
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
        //if (Health == 0)
        //{
        //    ShowOrHide(false);
        //}

        Health = 0;
        jump.ResetJump();
        Moving = false;
        animator.Play("Death");
        JustDied = true;
        //transform.position = respawnPoint;
    }

    private void UpdateDeath()
    {
        elapsedDeathTime += Time.deltaTime;
        if (JustDied && elapsedDeathTime >= 0.75f * deathTime)
        {
            GoToRespawnPoint(true);
            JustDied = false;
        }
        else if (!JustDied && elapsedDeathTime >= deathTime)
        {
            ResetPlayer();
        }
    }

    private void ResetPlayer()
    {
        //if (!JustDied)
        //{
        //    GoToRespawnPoint();
        //}

        //GoToRespawnPoint();
        jump.ResetJump();
        Health = maxHealth;
        JustDied = false;
        elapsedDeathTime = 0;
    }

    private void GoToRespawnPoint(bool playAnimation)
    {
        transform.position = respawnPoint;

        if (playAnimation)
            animator.Play("Respawn");
    }

    private void ShowOrHide(bool show)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = show;
        }
    }

    private void PlayWalkSound()
    {
        if (fall.IsOnFloor && !jump.IsJumping && !IsDead)
        {
            SFXPlayer.Instance.Play(Sound.Walk, volumeFactor: 0.4f, pitch: Random.Range(0.85f, 1.15f));
        }
    }
}
