using UnityEngine;

public class Player : MonoBehaviour
{
    public float _baseSpeed = 5f; // Base speed of the player
    public float _speedIncrease = 0.5f; // Speed multiplier when pressing 'T' key
    public float _speedDecrease = 0.5f; // Speed decrease when pressing 'G' key
    public float _rotationSpeed = 25f; // Rotation speed of the player
    public float _pitchSpeed = 25f; // Pitch speed of the player
    

    [SerializeField] private float _currentSpeed; // Current speed of the player
    [SerializeField] private GameObject _exhaustFX;
    [SerializeField] private Camera _externalCam;
    void Start()
    {
        _currentSpeed = _baseSpeed;
    }

    void Update()
    {
        // Move the player forward
        transform.Translate(Vector3.forward * _currentSpeed * Time.deltaTime);
        VelocityControls();
        ShipMovementControls();
        CheckExhaust();

        if (Input.GetKeyDown(KeyCode.Y))
        {
            //_externalCam.GetComponent<Camera>
        }
    }

    private void ShipMovementControls()
    {
        // Rotate the player when 'Q' key is pressed
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        }

        // Rotate the player when 'E' key is pressed
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * _rotationSpeed * Time.deltaTime);
        }


        // Pitch the player when 'W' key is pressed (Pitch up)
        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(Vector3.left * _pitchSpeed * Time.deltaTime);
        }

        // Pitch the player when 'S' key is pressed (Pitch down)
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(Vector3.right * _pitchSpeed * Time.deltaTime);
        }

        // Strafe the player when 'A' key is pressed (Strafe left)
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
        }

        // Strafe the player when 'D' key is pressed (Strafe right)
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
        }
    }

    private void VelocityControls()
    {
        // Increase speed when 'T' key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            _currentSpeed += _speedIncrease;
        }

        // Decrease speed when 'G' key is pressed
        if (Input.GetKeyDown(KeyCode.G))
        {
            _currentSpeed -= _speedDecrease;
            if (_currentSpeed < 0)
            {
                _currentSpeed = 0;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.L))
        {
            AllStop();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            AllAhead();
        }
    }

    private void AllStop()
    {
        _currentSpeed = 0;
    }

    private void AllAhead()
    {
        _currentSpeed = _baseSpeed;
    }

    private void CheckExhaust()
    {
        if (_currentSpeed == 0)
        {
            _exhaustFX.SetActive(false);
        }
        else if (_currentSpeed > 0)
        {
            _exhaustFX.SetActive(true);
        }
    }
}