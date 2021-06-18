using UnityEngine;

public class CarCrash : MonoBehaviour
{
    [SerializeField]
    private AudioClip _audioClip;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            return;
        }

        _audioSource.PlayOneShot(_audioClip);
        GameManager.GameFailedEvent.Invoke();
    }
}