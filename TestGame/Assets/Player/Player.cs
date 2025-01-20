using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Rigidbody _rb;

    private void FixedUpdate()
    {
       _rb.AddForce(_joystick.Horizontal * -_moveSpeed, _rb.velocity.y, _joystick.Vertical * -_moveSpeed);
    }   
}
