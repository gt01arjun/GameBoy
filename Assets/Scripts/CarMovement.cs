using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField]
    private float _accelerationPower;
    [SerializeField]
    private float _steeringPower;
    [SerializeField]
    private bool _canFillGas;

    private float _steeringAmount;
    private float _speed;
    private float _direction;

    private Rigidbody2D _rb;

    public bool PlayerInsideCar;
    public GameObject Player;

    private Camera _mainCamera;

    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip[] _audioClips;

    [SerializeField]
    private Sprite _crashedCarSprite;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_audioClips[2]);
    }

    private void Update()
    {
        if (GameManager.IsGamePaused || GameManager.IsGameOver)
            return;

        if (_canFillGas)
        {
            TargetCarMovement();
        }
        else
        {
            OtherCarMovement();
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused || GameManager.IsGameOver)
            return;

        _direction = Mathf.Sign(Vector2.Dot(_rb.velocity, _rb.GetRelativeVector(Vector2.up)));
        _rb.rotation += (int)(_steeringAmount * _steeringPower * _rb.velocity.magnitude * _direction);

        _rb.AddRelativeForce(Vector2.up * _speed);

        _rb.AddRelativeForce(-Vector2.right * _rb.velocity.magnitude * _steeringAmount / 2);
    }

    public void OtherCarMovement()
    {
        _steeringAmount = -Input.GetAxis("Horizontal");
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
    }

    public void TargetCarMovement()
    {
        if (GameManager.CurrentGasAmount > 0f)
        {
            _steeringAmount = -Input.GetAxis("Horizontal");
            _speed = Input.GetAxis("Vertical") * _accelerationPower;

            if (Mathf.Abs(_steeringAmount) > 0 || Mathf.Abs(_speed) > 0)
            {
                GameManager.CurrentGasAmount = Mathf.Clamp(GameManager.CurrentGasAmount - Time.deltaTime * 0.1f, 0f, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && PlayerInsideCar)
        {
            _mainCamera.GetComponent<CameraFollow>().TargetToFollow = Player.transform;
            Player.transform.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y);
            Player.SetActive(true);
            PlayerInsideCar = false;
            Player = null;
            gameObject.GetComponent<CarMovement>().enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.F) && _canFillGas && GameManager.GasCanCounter > 0)
        {
            GameManager.CurrentGasAmount = 1;
            GameManager.GasCanCounter--;
            GameManager.TotalOilCans--;
            _audioSource.PlayOneShot(_audioClips[0]);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OilSpill"))
        {
            _rb.drag = 0;
            _audioSource.PlayOneShot(_audioClips[1]);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OilSpill"))
        {
            _rb.drag = 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            return;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = _crashedCarSprite;

        if (_mainCamera != null || Player != null)
        {
            _mainCamera.GetComponent<CameraFollow>().TargetToFollow = Player.transform;
            Player.transform.position = new Vector2(gameObject.transform.position.x + 2f, gameObject.transform.position.y);
            Player.SetActive(true);
            PlayerInsideCar = false;
            Player = null;
            Destroy(gameObject.GetComponent<CarMovement>());
        }
    }
}