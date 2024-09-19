using UnityEngine;

public class AppleController : MonoBehaviour
{
    private AppleCountController _appleCountController;
    
    [Header("Audio")]
    public AudioClip appleSound;
    
    [Header("Components")]
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _appleCountController = GameObject.Find("AppleUserInterFace").GetComponent<AppleCountController>();
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        _appleCountController.appleCount++;
        _audioSource.PlayOneShot(appleSound);
        Destroy(gameObject);
    }
}
