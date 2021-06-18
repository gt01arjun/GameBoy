using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float _accelerationPower;
    [SerializeField]
    private float _steeringPower;
    [SerializeField]
    private bool _canFillGas;

    private float steeringAmount;
    private float _speed;
    private float _direction;

    private Rigidbody2D _rb;

    public bool PlayerInsideCar;
    public GameObject Player;

    private Camera _mainCamera;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        steeringAmount = -Input.GetAxis("Horizontal");
        _speed = Input.GetAxis("Vertical") * _accelerationPower;

        if (Input.GetKeyDown(KeyCode.E) && PlayerInsideCar)
        {
            _mainCamera.GetComponent<CameraFollow>().TargetToFollow = Player.transform;
            Player.transform.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y);
            Player.SetActive(true);
            PlayerInsideCar = false;
            Player = null;
            gameObject.GetComponent<CarMovement>().enabled = false;
        }

        if(Input.GetKeyDown(KeyCode.F) && _canFillGas && GameManager.GasCanCounter > 0)
        {
            Debug.Log("Fill");
        }
    }

    private void FixedUpdate()
    {
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