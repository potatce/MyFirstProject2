using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = -1;
   
    public LayerMask whatIsWall;
    public Transform wallCheck;
    public Transform fallCheck;
    
    private Rigidbody2D _rigidBody2D;

    private void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (DetectedWallOrFall())
        {
            moveSpeed *= -1;
            transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        _rigidBody2D.linearVelocityX = moveSpeed;
    }

    private bool DetectedWallOrFall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.1f, whatIsWall)
               || !Physics2D.OverlapCircle(fallCheck.position, 0.1f);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(wallCheck.position, 0.1f);
        Gizmos.DrawWireSphere(fallCheck.position, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Death"))
        {
            moveSpeed *= -1;
            transform.localScale  = new Vector2(transform.localScale.x * -1f, 1f);
        }
    }
}