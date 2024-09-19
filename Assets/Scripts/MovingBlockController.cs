using UnityEngine;

public class MovingBlockController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D _rigidbody2D;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            moveSpeed *= -1f;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = moveSpeed;
    }
}
