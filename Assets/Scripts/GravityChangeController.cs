using Unity.VisualScripting;
using UnityEngine;

public class GravityChangeController : MonoBehaviour
{
    
    private PlayerController _playerController;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerController = other.GetComponent<PlayerController>();
            other.GetComponent<Rigidbody2D>().gravityScale *= -1;
            other.transform.localScale  = new Vector2(other.transform.localScale.x * -1f, other.transform.localScale.y * -1f);
            _playerController.upsideDown = !_playerController.upsideDown; //Returns the opposite boolean value
            _playerController.jumpSpeed *= -1;
        }
        
    }   
}
