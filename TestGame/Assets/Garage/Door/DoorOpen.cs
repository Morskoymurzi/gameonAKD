using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _speed;

    private bool _isOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        _isOpen = true;
    }

    private void Update()
    {
        if (_isOpen)
        transform.position = Vector3.Lerp(transform.position, _targetPosition, _speed * Time.deltaTime);
    }
}
