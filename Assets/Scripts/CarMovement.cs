using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float _accelerationPower;
    [SerializeField]
    private float _steeringPower;

    private float steeringAmount;
    private float _speed;
    private float _direction;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        steeringAmount = -Input.GetAxis("Horizontal");
        _speed = Input.GetAxis("Vertical") * _accelerationPower;
        _direction = Mathf.Sign(Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.up)));
        _rb.rotation += (int)(steeringAmount * _steeringPower * _rb.velocity.magnitude * _direction);

        _rb.AddRelativeForce(Vector2.up * _speed);

        _rb.AddRelativeForce(-Vector2.right * _rb.velocity.magnitude * steeringAmount / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OilSpill"))
        {
            _rb.drag = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OilSpill"))
        {
            _rb.drag = 5;
        }
    }
}