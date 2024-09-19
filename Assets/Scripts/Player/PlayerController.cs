using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("GroundCheck")]
    public bool playerIsGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpSpeed = 20f;
    public bool upsideDown;
    private float Speed;
    
    [Header("Health")]
    public int playerHealth = 3;
    public float damageCooldown;
    public float _damageCooldownTimer;

    [Header("Audio")]
    public AudioClip music;
    public AudioClip[] deathSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] takeDamageSounds;
    
    [Header("Components")]
    private Rigidbody2D _rigidBody2D;
    private InputActions _inputActions;
    private Animator _animator;
    public Rigidbody2D parentRigidbody2D;
    private AudioSource _audioSource;

    private void Awake()
    {
        
    }

    void Start()
  {
      _inputActions = GetComponent<InputActions>();
      _rigidBody2D = GetComponent<Rigidbody2D>();
      _animator = GetComponent<Animator>();
      _audioSource = GetComponent<AudioSource>();
      
  }

    // Update is called once per frame
    void Update()
    {
        
        playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        
        if (_inputActions.Jump && playerIsGrounded)
        {
            if (_inputActions.Sprint)
            {
                _rigidBody2D.linearVelocityY = jumpSpeed * 1.3f;
            }
            else
            {
                _rigidBody2D.linearVelocityY = jumpSpeed;
            }
            
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(jumpSounds[Random.Range(0, jumpSounds.Length)]);
            
        }

        if (_inputActions.Horizontal != 0)
        {
            if (_inputActions.Horizontal < 0 && !upsideDown)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
 

        if (upsideDown)
        {
            //GetComponent<SpriteRenderer>().flipX = ! GetComponent<SpriteRenderer>().flipX;
        }
        
        Attack();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (parentRigidbody2D != null)
        {
            Speed = (_inputActions.Horizontal * moveSpeed) + (parentRigidbody2D.linearVelocityX);
        }
        else
        {
            if (_inputActions.Sprint)
            {
                Speed = (_inputActions.Horizontal * moveSpeed) * 1.3f;
            }
            else
            {
                Speed = _inputActions.Horizontal * moveSpeed;
            }
        }
        

        _rigidBody2D.linearVelocityX = Speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            ReloadScene();
        }
        
        if (other.transform.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (other.gameObject.CompareTag("MovingBlock"))
        {
            transform.parent = other.transform;
            parentRigidbody2D = other.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingBlock"))
        {
            transform.parent = null;
            parentRigidbody2D = null;
        }

    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }

    private void Attack()
    {
        if (!Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy")))
            return;
        var enemyColliders = Physics2D.OverlapCircleAll(groundCheck.position, 0.2f, LayerMask.GetMask("Enemy"));

        foreach (var enemy in enemyColliders)
        {
            Destroy(enemy.gameObject);
        }
        
        _rigidBody2D.linearVelocityY = jumpSpeed/1.3f;
    }

    private void TakeDamage()
    {
        
        if (Time.time > _damageCooldownTimer)
        {
            playerHealth--;
            _damageCooldownTimer = Time.time + damageCooldown;
        }

        if (playerHealth <= 0)
        {
            ReloadScene();
        }
        
        _audioSource.PlayOneShot(takeDamageSounds[Random.Range(0, takeDamageSounds.Length)]);
        
        //transform.position = new Vector3((transform.position.x - 0.5), transform.position.y, transform.position.z); //Resets the players position
    }

    private void ReloadScene()
    {
        _audioSource.PlayOneShot(deathSounds[0]);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateAnimation()
    {
        if (playerIsGrounded)
        {
            if (_inputActions.Horizontal != 0)
            {
                _animator.Play("PlayerWalk");
            }
            else
            {
                _animator.Play("PlayerIdle");
            }
        }
        else
        {
            if (_rigidBody2D.linearVelocityY > 0)
            {
                _animator.Play("PlayerJump");
            }
            else
            {
                _animator.Play("PlayerFall");
            }
        }
    }
}
