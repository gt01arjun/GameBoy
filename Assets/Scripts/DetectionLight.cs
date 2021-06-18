using UnityEngine;

public class DetectionLight : MonoBehaviour
{
    public float speed;
    public float rotationAmount;

    Vector3 pointA;
    Vector3 pointB;

    void Start()
    {
        pointA = transform.eulerAngles + new Vector3(0f, 0f, -rotationAmount);
        pointB = transform.eulerAngles + new Vector3(0f, 0f, rotationAmount);
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1);
        transform.eulerAngles = Vector3.Lerp(pointA, pointB, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>() || collision.gameObject.GetComponent<CarMovement>())
        {
            Debug.Log("GameOver");
        }
    }
}