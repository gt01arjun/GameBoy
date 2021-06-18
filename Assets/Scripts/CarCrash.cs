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

        if (gameObject.CompareTag("TargetCar") == true)
        {
            _audioSource.PlayOneShot(_audioClip);
            GameManager.GameFailedEvent.Invoke();
        }
        else if (gameObject.CompareTag("TargetCar") == false)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }
}