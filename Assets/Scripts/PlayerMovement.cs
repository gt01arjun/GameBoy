using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 _movement;

    private bool _canEnterCar;

    private GameObject _currentCar;

    private Camera _mainCamera;

    private AudioSource _audioSource;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _mainCamera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.IsGamePaused || GameManager.IsGameOver)
            return;

        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");

        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", _movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.E) && _canEnterCar)
        {
            _currentCar.GetComponent<CarMovement>().enabled = true;
            _currentCar.GetComponent<CarMovement>().Player = gameObject;
            _currentCar.GetComponent<CarMovement>().PlayerInsideCar = true;
            _mainCamera.GetComponent<CameraFollow>().TargetToFollow = _currentCar.transform;
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.IsGamePaused || GameManager.IsGameOver)
            return;

        _rb.MovePosition(_rb.position + _movement.normalized * _moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CarMovement>())
        {
            _canEnterCar = true;
            _currentCar = collision.gameObject;
        }
    }

    public void PlayWalkAudio()
    {
        _audioSource.Stop();
        _audioSource.Play();
    }
}