using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    [SerializeField] private Joystick _joy;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _cameraSensitivity;
    [SerializeField] private Transform _player;

    private Vector3 _moveVector;
    private CharacterController _ch_controller;
    private int _rightFingerId;
    private float _halfScreenWidth;
    private Vector2 _lookInput;
    private float _cameraPitch;

    private void Start()
    {
        _ch_controller = GetComponent<CharacterController>();

        _rightFingerId = -1;

        _halfScreenWidth = Screen.width / 2;
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if (_rightFingerId != -1)
        {
            LookAround();
        }
    }

    private void Update()
    {
        GetTouchInput();
    }

    private void GetTouchInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            switch (t.phase)
            {
                case TouchPhase.Began:
                    if (t.position.x > _halfScreenWidth && _rightFingerId == -1)
                    {
                        _rightFingerId = t.fingerId;
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (t.fingerId == _rightFingerId)
                    {
                        _rightFingerId = -1;
                    }
                    break;
                case TouchPhase.Moved:
                    if (t.fingerId == _rightFingerId)
                    {
                        _lookInput = t.deltaPosition * _cameraSensitivity * Time.deltaTime;
                    }
                    break;
                case TouchPhase.Stationary:
                    if (t.fingerId == _rightFingerId)
                    {
                        _lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    private void LookAround()
    {
        _cameraPitch = Mathf.Clamp(_cameraPitch - _lookInput.y, -90f, 90f);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0, 0);

        transform.Rotate(transform.up, _lookInput.x);
    }

    private void MovePlayer()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joy.Horizontal;
        _moveVector.z = _joy.Vertical;

        _moveVector = transform.right * -_moveVector.x + transform.forward * -_moveVector.z + transform.up * _moveVector.y;

        _ch_controller.Move(_moveVector * _speedMove * Time.deltaTime);
    }
}

