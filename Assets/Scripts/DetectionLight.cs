using UnityEngine;

public class DetectionLight : MonoBehaviour
{
    public float speed;
    public float rotationAmount;

    Vector3 pointA;
    Vector3 pointB;
    AudioSource _audioSource;

    void Start()
    {
        pointA = transform.eulerAngles + new Vector3(0f, 0f, -rotationAmount);
        pointB = transform.eulerAngles + new Vector3(0f, 0f, rotationAmount);
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            _audioSource.Play();
            GameManager.GameFailedEvent.Invoke();
        }
        else if (collision.gameObject.GetComponent<CarMovement>() && collision.gameObject.GetComponent<CarMovement>().PlayerInsideCar == true)
        {
            _audioSource.Play();
            GameManager.GameFailedEvent.Invoke();
        }
    }
}